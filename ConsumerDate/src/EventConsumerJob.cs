// See https://aka.ms/new-console-template for more information
using ConsumerDate.src;
using Microsoft.Extensions.Hosting;

public class EventConsumerJob : BackgroundService
{
    private readonly IConsumerDate _consumer;
    public EventConsumerJob(IConsumerDate consumer)
    {
        _consumer = consumer;   
    }
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        return _consumer.ConsumerAsync(stoppingToken);
    }
}







