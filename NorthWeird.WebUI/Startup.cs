using System;
using System.Linq;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NorthWeird.Application.Interfaces;
using NorthWeird.Application.Services;
using NorthWeird.Persistence;
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

            _logger.LogInformation(string.Join(Environment.NewLine, _configuration.AsEnumerable().Select((k, v) => $"{k.Key} - {k.Value}")));

            services.AddMvc();
        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseNodeModules(env.ContentRootPath);

            app.UseMvc(ConfigureRoutes);
        }

        private void ConfigureRoutes(IRouteBuilder routeBuilder)
        {
            routeBuilder.MapRoute("Default", "{controller=Home}/{action=Index}/{id?}");
        }
    }
}
