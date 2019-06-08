using System;
using System.IO;
using System.Linq;
using System.Reflection;
using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NorthWeird.Application.Interfaces;
using NorthWeird.Application.Mapping;
using NorthWeird.Application.Services;
using NorthWeird.Application.Validation;
using NorthWeird.Infrastructure.Mailing;
using NorthWeird.Persistence;
using NorthWeird.WebUI.Middleware;
using NorthWeird.WebUI.Validation;

namespace NorthWeird.WebUI
{
    public class Startup
    {
        private readonly IConfiguration _configuration;
        private readonly ILogger _logger;
        private readonly IHostingEnvironment _environment;

        public Startup(IConfiguration configuration, ILogger<Startup> logger, IHostingEnvironment env)
        {
            _configuration = configuration;
            _logger = logger;
            _environment = env;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            var migrationAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            //services
            //    .AddDbContext<IdentityDbContext>(
            //        options => options.UseSqlServer(_configuration.GetConnectionString("Identity"),
            //            sql => sql.MigrationsAssembly(migrationAssembly))

            services.AddDbContext<NorthWeirdDbContext>(
                options => options.UseSqlServer(_configuration.GetConnectionString("Northwind"), 
                    sql => sql.MigrationsAssembly(migrationAssembly)));

            services.AddScoped<IProductData, SqlProductData>();
            services.AddScoped<ICategoryData, SqlCategoryData>();
            services.AddScoped<ISupplierData, SqlSupplierData>();
            services.AddTransient<IEmailSender, EmailSender>();

            //services.AddScoped<LoggingActionFilterAttribute>();

            _logger.LogInformation(string.Join(Environment.NewLine, _configuration.AsEnumerable().Select((k, v) => $"{k.Key} - {k.Value}")));

            services
                .AddMvc()
                .AddFluentValidation(fv=>
                {
                    fv.RegisterValidatorsFromAssemblyContaining<ProductValidator>();
                    fv.RegisterValidatorsFromAssemblyContaining<RegisterViewModelValidator>();
                });

            if (!_environment.IsDevelopment())
            {
                //services.Configure<MvcOptions>(o => o.Filters.Add(new RequireHttpsAttribute()));
            }

            services.AddAutoMapper(typeof(ProductMappingProfile));

            //Identity

            //var migrationAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            services
                .AddDbContext<IdentityDbContext>(
                    options => options.UseSqlServer(_configuration.GetConnectionString("Identity"),
                    sql=>sql.MigrationsAssembly(migrationAssembly))
                );

            services
                .AddIdentity<IdentityUser, IdentityRole>(options => options.SignIn.RequireConfirmedEmail = true)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<IdentityDbContext>()
                .AddDefaultTokenProviders();

            services.Configure<DataProtectionTokenProviderOptions>(options =>
                options.TokenLifespan = TimeSpan.FromHours(3));

            services.ConfigureApplicationCookie(options =>
            {
                options.LoginPath = "/auth/login";
                options.AccessDeniedPath = "/administrator/AccessDenied";
            });

            services.AddAuthentication().AddAzureAd(options => _configuration.Bind("AzureAd", options));

        }

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //if (env.IsDevelopment())
            //{
            //    app.UseExceptionHandler("/Error");
            //}

            _logger.LogInformation(env.EnvironmentName);

            app.UseAuthentication();

            app.UseStaticFiles();

            //app.UseNodeModules(env.ContentRootPath);

            app.UseImageCaching(new ImageCachingMiddlewareOptions
            {
                ContentFolder = env.WebRootPath,
                //ContentFolder = "D:\\test",
                MaxCount = 10,
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
