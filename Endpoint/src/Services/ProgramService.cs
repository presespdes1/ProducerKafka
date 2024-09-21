using Endpoint.src.Contracts;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Endpoint.src.Services
{
    public class ProgramService : IProgram
    {
        private readonly IConsoleService _;
        private readonly IRequestService _request;
        private readonly IConfiguration _config;
        public ProgramService(IConsoleService console, IRequestService request, IConfiguration config)
        {
            _ = console;
            _request = request;
            _config = config;
        }
        public async Task Run()
        {
            string option = _config.GetSection("EndPoint")["Option"];
            string? number = "";

            _.Clear();
            _.WriteLine("");
            _.Write($"Escribe un número entre 1-{option} (Enter para terminar): ");
            number = _.ReadLine();

            if (!string.IsNullOrEmpty(number))
            {
                _.WriteLine("Respuesta del Servidor: ");
                _.WriteLine(await _request.GetRequest(number));
            }
        }
    }
}
