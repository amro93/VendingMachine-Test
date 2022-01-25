using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Prisistence.DependencyInjection;

namespace VendingMachine.Persistence.Tests
{
    [TestClass]
    public abstract class UnitTestBase
    {
        protected readonly IServiceProvider ServiceProvider;
        public UnitTestBase()
        {
            var services = new ServiceCollection();
            services.AddPersistence();
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
