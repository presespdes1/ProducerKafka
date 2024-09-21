using Confluent.Kafka;
using ConsumerDate.src.Contracts;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConsumerDate.src.Consumers
{
    public class Consumer : IConsumerDate
    {
        private readonly IConfiguration _config;
        private readonly ILogger<Consumer> _logger;

        public Consumer(IConfiguration config, ILogger<Consumer> logger)
        {
            _config = config;
            _logger = logger;
        }
        public Task ConsumerAsync(CancellationToken stopToken)
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = _config["Kafka:Host"],
                GroupId = _config["Kafka:Group"],
                AutoOffsetReset = AutoOffsetReset.Earliest,

            };

            using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();

            consumer.Subscribe(_config["Kafka:Topic"]);

            while (!stopToken.IsCancellationRequested)
            {
                try
                {
                    var consumerResponse = consumer.Consume(TimeSpan.FromSeconds(Convert.ToInt32(_config["Kafka:Consumer-Time"])));

                    if (consumerResponse == null)
                    {
                        continue;
                    }

                    _logger.LogInformation($"Mensaje consumido {consumerResponse.Message.Value} en: {consumerResponse.Offset}");
                }
                catch (OperationCanceledException)
                {
                    _logger.LogInformation("Operación Terminada");
                }
            }

            return Task.CompletedTask;
        }
    }
}
