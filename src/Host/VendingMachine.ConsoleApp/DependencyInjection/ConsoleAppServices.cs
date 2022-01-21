using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.ConsoleApp.Application;
using VendingMachine.Infrastructure.DependencyInjection;
using VendingMachine.Shared.Configurations;
using VendingMachine.Application.DependencyInjection;
using VendingMachine.Prisistence.DependencyInjection;
using VendingMachine.ConsoleApp.Commands;

namespace VendingMachine.ConsoleApp.DependencyInjection
{
    public static class ConsoleAppServices
    {
        public static IServiceCollection AddConsoleApp(this IServiceCollection services)
        {
            services.AddApplication();
            services.AddInfrastructure();
            services.AddPresistence();
            services.AddAppLogging();
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();
            services.AddSingleton<IConfiguration>(configuration);
            services.AddScoped<IApplicationBootstrapper, VendingMachineBootstrapper>();
            services.AddScoped<IConsoleApp, VendingMachineApplicaiton>();
            services.AddScoped<ICommandHandler, EnterCommandHandler>();
            services.AddScoped<ICommandHandlerFactory, CommandHandlerFactory>();
            services.AddScoped<IResultProcessorService, ResultProcessorService>();
            services.Configure<VendingMachineConfiguration>(t =>
            {
                
            });
            return services;
        }
    }
}
