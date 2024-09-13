using System.Reflection.Metadata.Ecma335;

namespace Exercise.src
{
    public interface IProducerDate
    {
        public Task ProducerAsync(DateTime dateTime, CancellationToken cancelToken);
    }
}
