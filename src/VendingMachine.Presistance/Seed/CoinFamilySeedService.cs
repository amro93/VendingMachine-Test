using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Application.Repositories;
using VendingMachine.Application.Services;

namespace VendingMachine.Persistence.Seed
{
    public class CoinFamilySeedService : IDBSeedService
    {
        private readonly ICoinFamilyRepository _repository;

        public CoinFamilySeedService(
            ICoinFamilyRepository repository)
        {
            _repository = repository;
        }
        public void Seed()
        {
            _repository.Create(new()
            {
                Name = "5 Cts",
                Value = 0.05m,
            });

            _repository.Create(new()
            {
                Name = "10 Cts",
                Value = 0.10m,
            });

            _repository.Create(new()
            {
                Name = "20 Cts",
                Value = 0.20m,
            });

            _repository.Create(new()
            {
                Name = "50 Cts",
                Value = 0.50m,
            });

            _repository.Create(new()
            {
                Name = "1 EURO",
                Value = 1m,
            });

            _repository.Create(new()
            {
                Name = "2 EURO",
                Value = 2m,
            });
            _repository.SaveChanges();
        }
    }
}
