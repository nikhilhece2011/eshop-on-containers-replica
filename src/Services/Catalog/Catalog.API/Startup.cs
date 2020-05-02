using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.API.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Catalog.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; set; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<GlobalSettings>(Configuration);

            services.AddDbContext<CatalogContext>(options =>
            {
                options.UseSqlServer(Configuration["ConnectionString"]);
            });

            services.AddMvc();

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "eShopOnContainersReplica - Product Catalog HTTP API",
                    Version = "v1",
                    Description = "Product Catalog Microservice, Simple CRUD Operation Based API",
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger()
                .UseSwaggerUI(options => options.SwaggerEndpoint($"/swagger/v1/swagger.json", "ProductCatalogAPI V1"));

            app.UseRouting();
            app.UseEndpoints(options => 
                            options.MapDefaultControllerRoute());
        }
    }
}
