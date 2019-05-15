using System.IO;
using System.Reflection;
using AutoMapper;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NorthWeird.Application.Interfaces;
using NorthWeird.Application.Mapping;
using NorthWeird.Application.Services;
using NorthWeird.Application.Validation;
using NorthWeird.Persistence;
using Swashbuckle.AspNetCore.Swagger;

namespace NorthWeird.WebApi
{
    public class Startup
    {
        private IConfiguration _configuration;
        readonly string _myAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public Startup(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<NorthWeirdDbContext>(options => options.UseSqlServer(_configuration.GetConnectionString("Northwind")));

            services.AddScoped<IProductData, SqlProductData>();
            services.AddScoped<ICategoryData, SqlCategoryData>();
            services.AddScoped<ISupplierData, SqlSupplierData>();

            services.AddCors(options =>
            {
                options.AddPolicy(_myAllowSpecificOrigins,
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:3000");
                    });
            });

            services.AddMvc().AddFluentValidation(fv => fv.RegisterValidatorsFromAssemblyContaining<ProductValidator>());
                //.SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddAutoMapper(typeof(ProductMappingProfile));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "NorthWeird API", Version = "v1" });
                var filePath = Path.Combine(System.AppContext.BaseDirectory, "NorthWeird.WebApi.xml");
                c.IncludeXmlComments(filePath);
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseSwagger();

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "NorthWeird API V1");
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(_myAllowSpecificOrigins);
            app.UseHttpsRedirection();

            app.UseMvc(config =>
            {
                config.MapRoute("NorthWeird", "api/{controller}/{action}");
            });
        }
    }
}
