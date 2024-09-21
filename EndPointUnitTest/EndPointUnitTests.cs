using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FakeItEasy;
using FluentAssertions;
using FluentAssertions.Execution;
using System.IO;
using System.Text.Json.Nodes;
using Newtonsoft.Json;
using System.Security.Cryptography.X509Certificates;
using Microsoft.VisualBasic.FileIO;
using Microsoft.Extensions.Configuration;
using Endpoint.src.Contracts;
using Endpoint.src.Services;


namespace EndPointUnitTest
{

    public class EndPointUnitTests
    {
        private const string ResponseHeaderMessage = "Respuesta del Servidor: ";
        private string option;
        private readonly IConsoleService _console;
        private readonly IRequestService _request;
        private IConfiguration _config;
        private IConfigurationSection _configSubValue;
        private IConfigurationSection _configSub;
        private ProgramService _program;
        public EndPointUnitTests()
        {    
            _console = A.Fake<IConsoleService>();
            _request = A.Fake<IRequestService>();
            option = "2";
            //Section                    
            _configSubValue = A.Fake<IConfigurationSection>();
            A.CallTo(() => _configSubValue.Value).Returns(option);

            _configSub = A.Fake<IConfigurationSection>();
            A.CallTo(() => _configSub.GetSection("Option")).Returns(_configSubValue);

            _config = A.Fake<IConfiguration>();
            A.CallTo(() => _config.GetSection("EndPoint")).Returns(_configSub);

            _program = new ProgramService(_console, _request, _config);
        }


        [Fact]
        public async Task ProgramService_Run_WhenValidInput()
        {
            string number = "5";
            string requestResponse = "Server Response";
            option = _config.GetSection("EndPoint")["Option"];
            var InitialMessage = $"Escribe un número entre 1-{option} (Enter para terminar): ";

            A.CallTo(() => _console.ReadLine()).Returns(number);
            A.CallTo(() => _request.GetRequest(number)).Returns(requestResponse);

            await _program.Run();
            

            //Asserts
            A.CallTo(() => _console.WriteLine("")).MustHaveHappenedOnceExactly();
            A.CallTo(() => _console.Write(InitialMessage)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _console.Clear()).MustHaveHappenedOnceExactly();
            A.CallTo(() => _console.WriteLine(ResponseHeaderMessage)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _console.WriteLine(requestResponse)).MustHaveHappenedOnceExactly();            
        }

        [Fact]
        public async Task ProgramService_Run_WhenInvalidInput()
        {
            string number = "-5";           
            string requestResponse = $"El valor {number} no es número entero entre 1 y 2";

            A.CallTo(() => _console.ReadLine()).Returns(number);
            A.CallTo(() => _request.GetRequest(number)).Returns(requestResponse);
          
            option = _config.GetSection("EndPoint")["Option"];
            var InitialMessage = $"Escribe un número entre 1-{option} (Enter para terminar): ";

            await _program.Run();

            //Asserts
            A.CallTo(() => _console.WriteLine("")).MustHaveHappenedOnceExactly();
            A.CallTo(() => _console.Write(InitialMessage)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _console.Clear()).MustHaveHappenedOnceExactly();
            A.CallTo(() => _console.WriteLine(ResponseHeaderMessage)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _console.WriteLine(requestResponse)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task ProgramService_Run_WhenInvalidInputIsNotInteger()
        {
            string number = "5as d";
            string requestResponse = $"El valor {number} no es número entero entre 1 y 2";
            A.CallTo(() => _console.ReadLine()).Returns(number);
            A.CallTo(() => _request.GetRequest(number)).Returns(requestResponse);

            option = _config.GetSection("EndPoint")["Option"];
            var InitialMessage = $"Escribe un número entre 1-{option} (Enter para terminar): ";

            await _program.Run();

            //Asserts
            A.CallTo(() => _console.WriteLine("")).MustHaveHappenedOnceExactly();
            A.CallTo(() => _console.Write(InitialMessage)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _console.Clear()).MustHaveHappenedOnceExactly();
            A.CallTo(() => _console.WriteLine(ResponseHeaderMessage)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _console.WriteLine(requestResponse)).MustHaveHappenedOnceExactly();
        }

        [Fact]
        public async Task ProgramService_Run_WhenInputIsNullOrEmpty()
        {
            string number = "";
            string requestResponse = $"El valor {number} no es número entero entre 1 y 2";
            A.CallTo(() => _console.ReadLine()).Returns(number);

            option = _config.GetSection("EndPoint")["Option"];
            var InitialMessage = $"Escribe un número entre 1-{option} (Enter para terminar): ";

            await _program.Run();

            //Asserts
            A.CallTo(() => _console.WriteLine("")).MustHaveHappenedOnceExactly();
            A.CallTo(() => _console.Write(InitialMessage)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _console.Clear()).MustHaveHappenedOnceExactly();          
        }

        [Fact]
        public async Task ProgramService_Run_WhenValidInputButNoRequest()
        {            
            string number = "5";
            var requestResponse = "Status: Reason: ";
            A.CallTo(() => _console.ReadLine()).Returns(number);
            A.CallTo(() => _request.GetRequest(number)).Returns(requestResponse);

            option = _config.GetSection("EndPoint")["Option"];
            var InitialMessage = $"Escribe un número entre 1-{option} (Enter para terminar): ";

            await _program.Run();

            //Asserts
            A.CallTo(() => _console.WriteLine("")).MustHaveHappenedOnceExactly();
            A.CallTo(() => _console.Write(InitialMessage)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _console.Clear()).MustHaveHappenedOnceExactly();
            A.CallTo(() => _console.WriteLine(ResponseHeaderMessage)).MustHaveHappenedOnceExactly();
            A.CallTo(() => _console.WriteLine(requestResponse)).MustHaveHappenedOnceExactly();        
        }
    }
}
