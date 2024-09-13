using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Endpoint.src
{
    public static class Startup
    {
        public static IServiceProvider Init()
        {
            //Configuracion
            var builderConf = new ConfigurationBuilder();
            builderConf.AddJsonFile("appsettings.json");
            IConfiguration configuration = builderConf.Build();
            //Srvices
            var services = new ServiceCollection();
            services.AddSingleton<IRequestService>(provider =>
            {
                return new RequestService(configuration);
            });
            services.AddScoped<IConsoleService, ConsoleService>();
            services.AddScoped<IProgram>(provider => 
            { 
                var request = provider.GetService<IRequestService>();
                var console = provider.GetService<IConsoleService>();
                return new ProgramService(console, request, configuration);
            });
            return services.BuildServiceProvider();
        }

    }
}
