
using Confluent.Kafka;
using ProducerDate.src.Contracts;

namespace ProducerDate.src.Producers
{
    public class Producer : IProducerDate
    {
        private readonly IConfiguration _config;
        private readonly ILogger<Producer> _logger;
        private readonly IHash _hash;

        public Producer(IConfiguration config, ILogger<Producer> logger, IHash hash)
        {
            _config = config;
            _logger = logger;
            _hash = hash;
        }
        public async Task ProducerAsync(DateTime dateTime, CancellationToken cancelToken)
        {
            var config = new ProducerConfig
            {
                BootstrapServers = _config["Kafka:Host"],
                AllowAutoCreateTopics = true,
                Acks = Acks.All,
                //SslKeystoreLocation = "D:\\Works\\Kafka\\keys\\kafka-broker-keystore.p12",
                //SslKeystorePassword = "export123",
                //SslKeyPassword = "key123",
                //SslEndpointIdentificationAlgorithm = SslEndpointIdentificationAlgorithm.None,
                //SecurityProtocol = SecurityProtocol.Ssl
            };

            using var producer = new ProducerBuilder<Null, string>(config).Build();
            string sha256DateMessage = _hash.Compute(dateTime.ToString());
            try
            {
                var response = await producer.ProduceAsync(topic: _config["Kafka:Topic"],
                    new Message<Null, string> { Value = sha256DateMessage },
                    cancelToken);

                _logger.LogInformation($"Mensaje enviado: {response.Value}, Offset: {response.Offset}");
            }
            catch (ProduceException<Null, string> ex)
            {
                _logger.LogError($"No se pudo enviar el mensaje: {ex.Error.Reason}");
            }

            producer.Flush(cancelToken);
        }
    }
}
