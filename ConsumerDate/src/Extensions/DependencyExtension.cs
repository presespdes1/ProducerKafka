using Confluent.Kafka;
using ConsumerDate.src.Contracts;
using ConsumerDate.src.Consumers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;


namespace ConsumerDate.src.Extensions
{
    public static class DependencyExtension
    {
        public static IServiceCollection AddConsumer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IConsumerDate>(provider =>
            {
                var logger = provider.GetService<ILogger<Consumer>>();

                return new Consumer(configuration, logger);
            });

            return services;
        }
    }
}
