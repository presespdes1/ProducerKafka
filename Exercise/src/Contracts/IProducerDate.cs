using System.Reflection.Metadata.Ecma335;

namespace ProducerDate.src.Contracts
{
    public interface IProducerDate
    {
        public Task ProducerAsync(DateTime dateTime, CancellationToken cancelToken);
    }
}
