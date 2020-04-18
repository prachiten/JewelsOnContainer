using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ProductCalatog.Data;

namespace ProductCalatog
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            //add configurations from docker-compose.yml
            var server = Configuration["DatabaseServer"];
            var database = Configuration["DatabaseName"];
            var user = Configuration["DatabaseUser"];
            var password = Configuration["DatabasePassword"];
            var connectionString = $"Server={server};Database={database};User Id={user};Password={password}";
            Console.WriteLine(connectionString);
            services.AddDbContext<CatalogContext>(options=>options.UseSqlServer(connectionString));
            // services.AddDbContext<CatalogContext>(options => options.UseSqlServer(Configuration["Connectionstring"]));  // For IISxpress
            //above statement creates a table of type CatalogContext 
            //ie it creates a object of CatalogContext and while creating object we need to pass option in its constructor

            //-------------------ADD SWAGGER--------------------------------------
            services.AddSwaggerGen(options =>
            {
                //options.DescribeAllEnumsAsStrings();
                //v1 is version 1
                options.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "JewelsonContainers - Product Catalog API",//title of documentation
                    Version = "v1",                                   //this is actual version
                    Description = "Product catalog microservice"      //description of document
                });
                //
                options.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());

            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();
            app.UseSwagger()        //swagger will have UI page as well where all documented information will be available
                //type http://localhost:6803/swagger in browser to see swagger documentation
               .UseSwaggerUI(e =>
               {
                   // give swagger url and name of document
                   e.SwaggerEndpoint($"/swagger/v1/swagger.json", "ProductCatalogAPI V1");
               });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
