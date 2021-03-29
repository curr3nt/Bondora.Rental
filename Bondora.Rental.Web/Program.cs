using Bondora.Rental.Domain.Interface;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using NServiceBus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Bondora.Rental.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseNServiceBus(context =>
                {
                    var endpointConfiguration = new EndpointConfiguration("Bondora.Rental.Web");
                    var transport = endpointConfiguration.UseTransport<LearningTransport>();
                    endpointConfiguration.MakeInstanceUniquelyAddressable("Bondora.Rental.Web");
                    endpointConfiguration.EnableCallbacks();
                    transport.Routing().RouteToEndpoint(
                        assembly: typeof(EquipmentOrder).Assembly,
                        destination: "Bondora.Rental.Domain.Interface");

                    return endpointConfiguration;
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
