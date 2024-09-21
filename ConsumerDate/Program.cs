// See https://aka.ms/new-console-template for more information
using ConsumerDate.src;
using ConsumerDate.src.Extensions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.ComponentModel;

public class Program
{
    private static async Task Main(string[] args)
    {
        await Startup.Run(args);      
    }
    
}







