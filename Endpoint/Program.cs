// See https://aka.ms/new-console-template for more information
using Endpoint.src;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using static System.Runtime.InteropServices.JavaScript.JSType;

internal class Program
{
    private static async Task Main(string[] args)
    {
        var service = Startup.Init();
        var runProgram = service.GetService<IProgram>();
        await runProgram.Run();
    }
}