using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using NorthWeird.Infrastructure.Image;

namespace NorthWeird.WebUI.Middleware
{
    public class ImageCachingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ImageCache _imageCache;

        public ImageCachingMiddleware(RequestDelegate next, ImageCachingMiddlewareOptions options)
        {
            _next = next;
            _imageCache = new ImageCache(options.ContentFolder, options.MaxCount, options.ExpirationTime);
        }

        public async Task InvokeAsync(HttpContext context)
        {
            if (_imageCache.IsImageInCache(context.Request.Path))
            {
                var imageBytes = _imageCache.GetImageFromCache(context.Request.Path);
                await context.Response.Body.WriteAsync(imageBytes, 0, imageBytes.Length);
            }
            else
            {
                using (var memStream = new MemoryStream())
                {
                    var originalBody = context.Response.Body;
                    context.Response.Body = memStream;

                    await _next(context);

                    if (_imageCache.IsContentTypeImage(context.Response.ContentType))
                    {
                        _imageCache.PutImageInCache(memStream, context.Request.Path);
                    }
                    memStream.Position = 0;
                    memStream.CopyTo(originalBody);
                }
            }
        }
    }
}
