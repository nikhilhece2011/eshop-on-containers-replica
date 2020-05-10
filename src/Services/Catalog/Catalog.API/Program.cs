using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Catalog.API.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace Catalog.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();//.Run();

            using (var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var context = services.GetRequiredService<CatalogContext>();

                    await CatalogSeed.SeedAsync(context);
                }
                catch (Exception ex)
                {
                    throw new Exception("Some error occurred in product catalog seed method");
                }
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    //webBuilder.ConfigureKestrel(serverOptions =>
                    //{
                    //    serverOptions.Listen(IPAddress.Loopback, 5000, listenOptions =>
                    //    {
                    //        listenOptions.UseHttps("mycertificatename.pfx", "Nikhil@123");
                    //    });
                    //});
                    webBuilder.UseStartup<Startup>();
                });
    }
}
