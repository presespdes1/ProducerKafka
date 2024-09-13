using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsumerDate.src
{
    public static class DependencyExtension
    {
        public static IServiceCollection AddConsumer(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IConsumerDate>(provider =>
            {
                var logger = provider.GetService<ILogger<ConsumerDate>>();

                return new ConsumerDate(configuration, logger);
            });

            return services;
        }
    }
}
