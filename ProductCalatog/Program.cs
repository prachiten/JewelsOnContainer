using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ProductCalatog.Data;
using System.IO;

namespace ProductCalatog
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build(); 
            //first ask startup file ie host the scope of services which is Add Controller and Add DbContext
            using(var scope= host.Services.CreateScope())
         //using statement guarantees that whatever is inside the curly braces of using it will be marked for deletion ie behind the scene it will call IDisposable
            {
                var serviceProviders = scope.ServiceProvider;// it means give me the scope of each service provider
                var context = serviceProviders.GetRequiredService<CatalogContext>();// i am interested only whether sql database is set up or not so only need Catalog Context 
                //This above line will act as await methos and will wait till sql is up and running.
                //Now sql is up and running so we can populate it with data
                CatalogSeed.Seed(context);// context has name of sql database
            }
            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
