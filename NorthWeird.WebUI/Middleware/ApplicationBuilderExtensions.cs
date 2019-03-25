using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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
    }
}
