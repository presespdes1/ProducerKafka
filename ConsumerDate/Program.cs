// See https://aka.ms/new-console-template for more information
using ConsumerDate.src;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.ComponentModel;

public class Program
{
    private static async Task Main(string[] args)
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







