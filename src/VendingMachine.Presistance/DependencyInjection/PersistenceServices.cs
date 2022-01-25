using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VendingMachine.Prisistence.DbContexts;
using VendingMachine.Application.Repositories;
using VendingMachine.Persistence.Repositories;

namespace VendingMachine.Prisistence.DependencyInjection
{
    public static class PersistenceServices
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services)
        {
            services.AddTransient<IOrderRepository, OrderRepository>();
            services.AddTransient<IProductRepository, ProductRepository>();
            services.AddTransient<IOrderProductRepository, OrderProductRepository>();

            services.AddDbContext<AppDbContext>(t =>
            {
                t.UseInMemoryDatabase("InMemoryDB");
            });
            return services;
        }
    }
}
