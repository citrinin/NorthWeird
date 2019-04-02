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

        public ImageCachingMiddleware(RequestDelegate next, ImageCachingMiddlewareOptions options)
        {
            _next = next;
            _path = options.ContentFolder;
            _maxCachedItems = options.MaxCount;
            _maxLifeTime = options.ExpirationTime;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            
            var fileName = Path.Combine(_path, $"{context.Request.Path.ToString().Replace("/","")}.jpeg");

            if (File.Exists(fileName))
            {
                using (var file = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                {
                    await file.CopyToAsync(context.Response.Body, (int) file.Length);
                    context.Response.ContentType = "image/jpeg";
                }
            }
            else
            {
                using (var memStream = new MemoryStream())
                {
                    var originalBody = context.Response.Body;
                    context.Response.Body = memStream;

                    await _next(context);

                    if (string.Equals(context.Response.ContentType, "image/jpeg", StringComparison.OrdinalIgnoreCase))
                    {

                        using (var file = new FileStream(fileName, FileMode.Create, FileAccess.Write))
                        {
                            memStream.Position = 0;
                            await memStream.CopyToAsync(file, (int)memStream.Length);
                        }

                        var filesInDirectory = Directory.EnumerateFiles(_path).OrderBy(File.GetCreationTime).ToList();
                        var filesToDelete = filesInDirectory.Where(f => DateTime.Now - File.GetCreationTime(f) > _maxLifeTime);

                        foreach (var file in filesToDelete)
                        {
                            File.Delete(file);
                        }

                        if (filesInDirectory.Count() >= _maxCachedItems)
                        {
                            var fileToDelete = filesInDirectory.FirstOrDefault();
                            File.Delete(fileToDelete);
                        }
                    }

                    memStream.Position = 0;
                    await memStream.CopyToAsync(originalBody);
                }
            }
 
        }
    }
}
