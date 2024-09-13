using ProducerDate.src;

namespace Exercise.src
{
    public static class DependencyExtension
    {
        public static IServiceCollection AddProducer(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped<IHash, Compute256sha>();
            services.AddSingleton<IProducerDate> (provider => 
            {
                var logger = provider.GetService<ILogger<ProducerDate>>();

                var hash = provider.GetService<IHash>();

                return new ProducerDate(config, logger, hash);
            });
            
            return services;
        }
    }
}
