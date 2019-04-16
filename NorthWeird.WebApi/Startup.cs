using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NorthWeird.Application.Interfaces;
using NorthWeird.Application.Services;
using NorthWeird.Persistence;

namespace NorthWeird.WebApi
{
    public class Startup
    {
        private IConfiguration _configuration;
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

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
                options.AddPolicy(MyAllowSpecificOrigins,
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:3000");
                    });
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(MyAllowSpecificOrigins);
            app.UseHttpsRedirection();
            app.UseMvc(config =>
            {
                config.MapRoute("NorthWeird", "api/{controller}/{action}");
            });
        }
    }
}
