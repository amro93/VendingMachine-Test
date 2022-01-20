using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Domain.Entities;

namespace VendingMachine.Application.Repositories
{

    public interface IRepository<TEntity> where TEntity : class, IBaseEntity
    {
        void Create(TEntity entity);
        void Update(TEntity entity);
        void Delete(TEntity entity);
        IQueryable<TEntity> GetQuerryable();
        public int SaveChanges();
    }
}
