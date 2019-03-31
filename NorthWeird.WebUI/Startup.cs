using System;
using System.Linq;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NorthWeird.Application.Interfaces;
using NorthWeird.Application.Services;
using NorthWeird.Application.Validation;
using NorthWeird.Persistence;
using NorthWeird.WebUI.Filters;
using NorthWeird.WebUI.Middleware;

namespace NorthWeird.WebUI
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        public Startup(IConfiguration configuration, ILogger<Startup> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<NorthWeirdDbContext>(options => options.UseSqlServer(_configuration.GetConnectionString("Northwind")));

            services.AddScoped<IProductData, SqlProductData>();
            services.AddScoped<ICategoryData, SqlCategoryData>();
            services.AddScoped<ISupplierData, SqlSupplierData>();

            //services.AddScoped<LoggingActionFilterAttribute>();

            _logger.LogInformation(string.Join(Environment.NewLine, _configuration.AsEnumerable().Select((k, v) => $"{k.Key} - {k.Value}")));

            services.AddMvc().AddFluentValidation(fv=>fv.RegisterValidatorsFromAssemblyContaining<ProductValidator>());
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
            //    app.UseExceptionHandler("/Error");
            //}

            app.UseStaticFiles();

            app.UseNodeModules(env.ContentRootPath);

            app.UseImageCaching(new ImageCachingMiddlewareOptions
            {
                ContentFolder = "D:\\test",
                MaxCount = 30,
                ExpirationTime = TimeSpan.FromMinutes(3)
            });


            app.UseMvc(ConfigureRoutes);
        }

        private void ConfigureRoutes(IRouteBuilder routeBuilder)
        {
            routeBuilder.MapRoute("Default", "{controller=Home}/{action=Index}/{id?}");
            routeBuilder.MapRoute("Image", "image/{id}", new {controller = "Category", action = "GetCategoryImage" });
        }
    }
}
