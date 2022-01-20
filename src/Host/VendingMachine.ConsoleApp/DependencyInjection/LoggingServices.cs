using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.ConsoleApp.DependencyInjection
{
    public static class LoggingServices
    {
        public static void AddAppLogging(this IServiceCollection services)
        {
            services.AddLogging(t =>
            {
                t.SetMinimumLevel(LogLevel.Information);
                t.AddConsole();
            });
        }
    }
}
