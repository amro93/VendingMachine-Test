using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Application.Repositories;
using VendingMachine.Application.Services;
using VendingMachine.Domain.Core;
using VendingMachine.Domain.Entities;
using VendingMachine.Shared.Coins;

namespace VendingMachine.Infrastructure.Coins
{
    public class CoinFamilyService : ICoinFamilyService
    {
        private readonly ICoinFamilyRepository _coinFamilyRepository;

        public CoinFamilyService(ICoinFamilyRepository coinFamilyRepository)
        {
            _coinFamilyRepository = coinFamilyRepository;
        }
        public GetExactChangeResult GetExactChange(decimal value)
        {
            var coinFamilies = _coinFamilyRepository.GetQuerryable().ToList();
            return GetExactChange(coinFamilies, value);
        }

        public IResultTemplate RemoveCoin(CoinFamilyRemoveDto dto)
        {
            if (dto.RemovedQuantity <= 0) return ResultTemplate.SucceededResult();
            var coinFamily = _coinFamilyRepository.GetQuerryable().FirstOrDefault(t => t.Id == dto.CoinFamilyId);
            if (coinFamily == null) throw new ArgumentNullException(nameof(coinFamily));
            coinFamily.Quantity -= dto.RemovedQuantity;
            if (coinFamily.Quantity < 0) throw new ArgumentOutOfRangeException(nameof(coinFamily.Quantity), "Removed quantity more than existing balance");
            _coinFamilyRepository.SaveChanges();
            return ResultTemplate.SucceededResult();
        }

        public IResultTemplate AddCoin(decimal amount)
        {
            var dbCoinFamily = _coinFamilyRepository.GetQuerryable().FirstOrDefault(t => t.Value == amount);
            if (dbCoinFamily == null) throw new ArgumentNullException(nameof(dbCoinFamily));
            dbCoinFamily.Quantity++;
            _coinFamilyRepository.SaveChanges();
            return ResultTemplate.SucceededResult();
        }

        public GetExactChangeResult GetExactChange(IEnumerable<CoinFamily> coinFamilies, decimal value)
        {
            if (coinFamilies is null)
            {
                throw new ArgumentNullException(nameof(coinFamilies));
            }

            var coinsFamiliesList = coinFamilies.Where(t => t.Quantity > 0 && t.Value > 0).OrderByDescending(t => t.Value).ToList();
            var result = new GetExactChangeResult();
            var coins = result.Coins;

            foreach (var cf in coinsFamiliesList)
            {
                if (value <= 0) break;

                var familyCount = (int)Math.Floor(value / cf.Value);
                if (familyCount < 1) continue;

                if (cf.Quantity < familyCount) familyCount = cf.Quantity;
                coins.Add(new()
                {
                    Name = cf.Name,
                    CoinFamilyId = cf.Id,
                    Value = cf.Value,
                    Count = familyCount,
                });

                value = value - familyCount * cf.Value;
            }

            return result;
        }
    }
}
