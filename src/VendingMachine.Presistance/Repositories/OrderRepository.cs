using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Application.Repositories;
using VendingMachine.Domain.Entities;
using VendingMachine.Prisistence.DbContexts;

namespace VendingMachine.Persistence.Repositories
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        public OrderRepository(AppDbContext dbContext) : base(dbContext)
        {
        }

        public IQueryable<Order> GetOpenedOrdersQuery()
        {
            return GetQuerryable().Where(t => t.State == Domain.Enums.OrderState.Opened).OrderByDescending(t => t.Id);
        }
    }
}
