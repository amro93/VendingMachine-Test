using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Domain.Entities;

namespace VendingMachine.Application.Repositories
{
    public interface IOrderRepository : IRepository<Order>
    {
        IQueryable<Order> GetOpenedOrdersQuery();
    }
}
