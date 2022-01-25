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
    public class CoinFamilyRepository : RepositoryBase<CoinFamily>, ICoinFamilyRepository
    {
        public CoinFamilyRepository(AppDbContext dbContext) : base(dbContext)
        {
        }
    }
}
