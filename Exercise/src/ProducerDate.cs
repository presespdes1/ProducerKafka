
using Confluent.Kafka;
using ProducerDate.src;

namespace Exercise.src
{
    public class ProducerDate : IProducerDate
    {
        private readonly IConfiguration _config;
        private readonly ILogger<ProducerDate> _logger;
        private readonly IHash _hash;
 
        public ProducerDate(IConfiguration config, ILogger<ProducerDate> logger, IHash hash) 
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
