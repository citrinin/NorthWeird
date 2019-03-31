using System.IO;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.FileProviders;

namespace NorthWeird.WebUI.Middleware
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder UseNodeModules(this IApplicationBuilder app, string root)
        {
            var path = Path.Combine(root, "node_modules");
            var fileProvider = new PhysicalFileProvider(path);
            var options = new StaticFileOptions();

            options.RequestPath = "/node_modules";
            options.FileProvider = fileProvider;

            app.UseStaticFiles(options);
            return app;
        }

        public static IApplicationBuilder UseImageCaching(this IApplicationBuilder app,
            ImageCachingMiddlewareOptions options)
        {
            app.UseMiddleware<ImageCachingMiddleware>(options);
            return app;
        }
    }
}
