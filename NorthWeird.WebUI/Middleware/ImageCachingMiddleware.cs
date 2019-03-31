using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace NorthWeird.WebUI.Middleware
{
    public class ImageCachingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly string _path;
        private readonly int _maxCachedItems;
        private readonly TimeSpan _maxLifeTime;
        private DateTime _lastRequestTime;
        private readonly ConcurrentDictionary<string, string> _filePaths;

        public ImageCachingMiddleware(RequestDelegate next, ImageCachingMiddlewareOptions options)
        {
            _next = next;
            _path = options.ContentFolder;
            _maxCachedItems = options.MaxCount;
            _maxLifeTime = options.ExpirationTime;
            _filePaths = new ConcurrentDictionary<string, string>();
            _lastRequestTime = DateTime.Now;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            var originalBody = context.Response.Body;
            var currentTime = DateTime.Now;

            if (currentTime - _lastRequestTime > _maxLifeTime)
            {
                _filePaths.Clear();
                foreach (var file in Directory.EnumerateFiles(_path))
                {
                    File.Delete(file);
                }
            }

            _lastRequestTime = currentTime;
            try
            {
                if (_filePaths.ContainsKey(context.Request.Path) && File.Exists(context.Request.Path))
                {
                    using (var file = new FileStream(_filePaths[context.Request.Path], FileMode.Open, FileAccess.Read))
                    {
                        await file.CopyToAsync(originalBody, (int) file.Length);
                        context.Response.ContentType = "image/jpeg";
                    }

                }
                else
                {
                    if (_filePaths.ContainsKey(context.Request.Path))
                    {
                        _filePaths.Remove(context.Request.Path, out var val);
                    }


                    using (var memStream = new MemoryStream())
                    {
                        context.Response.Body = memStream;

                        await _next(context);


                        memStream.Position = 0;

                        if (string.Equals(context.Response.ContentType, "image/jpeg", StringComparison.OrdinalIgnoreCase))
                        {
                            var filename = Path.Combine(_path, $"{Guid.NewGuid().ToString()}.jpeg");

                            using (var file = new FileStream(filename, FileMode.Create, FileAccess.Write))
                            {
                                await memStream.CopyToAsync(file, (int)memStream.Length);
                            }

                            if (_filePaths.Count >= _maxCachedItems)
                            {
                                var firstItem = _filePaths.First();
                                _filePaths.Remove(firstItem.Key, out var val);
                                File.Delete(firstItem.Value);
                            }

                            _filePaths.TryAdd(context.Request.Path, filename);
                        }

                        memStream.Position = 0;
                        await memStream.CopyToAsync(originalBody);
                    }
                }
            }
            finally
            {
                context.Response.Body = originalBody;
            }
        }
    }
}
