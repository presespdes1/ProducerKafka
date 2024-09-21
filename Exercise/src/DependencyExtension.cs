using ProducerDate.src.Contracts;
using ProducerDate.src.Services;
using ProducerDate.src.Producers;

namespace Exercise.src
{
    public static class DependencyExtension
    {
        public static IServiceCollection AddProducer(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<IHash, Compute256sha>();
            services.AddSingleton<IProducerDate> (provider => 
            {
                var logger = provider.GetService<ILogger<Producer>>();

                var hash = provider.GetService<IHash>();

                return new Producer(config, logger, hash);
            });
            
            return services;
        }
    }
}
