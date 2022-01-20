﻿using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VendingMachine.Prisistence.DbContexts;
using VendingMachine.Application.Repositories;
using VendingMachine.Presistence.Repositories;

namespace VendingMachine.Prisistence.DependencyInjection
{
    public static class PresistenceServices
    {
        public static IServiceCollection AddPresistence(this IServiceCollection services)
        {
            services.AddTransient<IOrderRepository, OrderRepository>();

            services.AddDbContext<AppDbContext>(t =>
            {
                t.UseInMemoryDatabase("InMemoryDB");
            });
            return services;
        }
    }
}