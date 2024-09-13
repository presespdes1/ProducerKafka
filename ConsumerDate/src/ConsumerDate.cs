using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ConsumerDate.src
{
    public class ConsumerDate : IConsumerDate
    {
        private readonly IConfiguration _config;
        private readonly ILogger<ConsumerDate> _logger;

        public ConsumerDate(IConfiguration config, ILogger<ConsumerDate> logger)
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
