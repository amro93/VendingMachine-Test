using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Application.Repositories;
using VendingMachine.Domain.Entities;
using VendingMachine.Prisistence.DbContexts;

namespace VendingMachine.Presistence.Repositories
{
    public abstract class RepositoryBase<T> : IRepository<T> where T : class, IBaseEntity
    {
        private readonly AppDbContext _dbContext;

        public RepositoryBase(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public void Create(T entity)
        {
            _dbContext.Add(entity);
        }

        public void Delete(T entity)
        {
            _dbContext.Remove(entity);
        }

        public IQueryable<T> GetQuerryable()
        {
            return _dbContext.Set<T>();
        }

        public void Update(T entity)
        {
            _dbContext.Update(entity);
        }

        public int SaveChanges()
        {
            return _dbContext.SaveChanges();
        }
    }
}
