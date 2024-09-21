using ConsumerDate.src.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsumerDate.src
{
    public static class Startup
    {
        public static async Task Run(string[] args)
        {
            var builder = new ConfigurationBuilder();
            builder.AddJsonFile("sharedconfig.json");
            IConfigurationRoot configuration = builder.Build();

            var host = Host.CreateDefaultBuilder(args)
                .ConfigureServices(services =>
                {
                    services.AddConsumer(configuration)
                            .AddHostedService<EventConsumerJob>();

                });

            await host.RunConsoleAsync();
        }
    }
}
