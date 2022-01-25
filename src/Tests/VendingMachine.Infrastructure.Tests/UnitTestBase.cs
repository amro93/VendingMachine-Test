using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Infrastructure.DependencyInjection;

namespace VendingMachine.Infrastructure.Tests
{
    [TestClass]
    public abstract class UnitTestBase
    {
        protected readonly IServiceProvider ServiceProvider;
        public UnitTestBase()
        {
            var services = new ServiceCollection();
            services.AddInfrastructure();
            var configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(new Dictionary<string, string>()
                {

                })
                .Build();
            services.AddSingleton<IConfiguration>(configuration);

            ServiceProvider = services.BuildServiceProvider();
        }
    }
}
