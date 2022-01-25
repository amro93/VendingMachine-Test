using VendingMachine.Application.Repositories;
using VendingMachine.Domain.Entities;
using VendingMachine.Prisistence.DbContexts;

namespace VendingMachine.Persistence.Repositories
{
    public class OrderProductRepository : RepositoryBase<OrderProduct>, IOrderProductRepository
    {
        public OrderProductRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
