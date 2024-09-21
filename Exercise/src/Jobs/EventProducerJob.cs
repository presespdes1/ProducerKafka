using ProducerDate.src.Contracts;

namespace ProducerDate.src.Jobs
{
    public class EventProducerJob : BackgroundService
    {
        private readonly IProducerDate _producer;
        private readonly TimeSpan _period;

        public EventProducerJob(IProducerDate producer)
        {
            _producer = producer;
            _period = TimeSpan.FromSeconds(1);
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using PeriodicTimer timer = new PeriodicTimer(_period);
            while (!stoppingToken.IsCancellationRequested && await timer.WaitForNextTickAsync(stoppingToken))
            {
                await _producer.ProducerAsync(DateTime.Now, stoppingToken);
            }

        }
    }
}
