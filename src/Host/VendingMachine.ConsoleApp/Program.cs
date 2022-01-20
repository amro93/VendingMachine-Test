using Microsoft.Extensions.DependencyInjection;
using System;
using VendingMachine.ConsoleApp.Application;

namespace VendingMachine.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var serviceCollection = new ServiceCollection();
            new Startup().ConfigureServices(serviceCollection);
            var serviceProvider = serviceCollection.BuildServiceProvider();
            
            using var scope = serviceProvider.CreateScope();
            var consoleApp = scope.ServiceProvider.GetRequiredService<IConsoleApp>();
            consoleApp.Run(args);
        }
    }
}
