using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Application.Localization;
using VendingMachine.Application.Logging;
using VendingMachine.Application.Repositories;
using VendingMachine.Application.Services;
using VendingMachine.Domain.Core;
using VendingMachine.Shared.Coins;

namespace VendingMachine.Infrastructure.Coins
{
    public class CoinService : ICoinService
    {
        private readonly ICurrentCurrency _currentCurrency;
        private readonly ICurrentOrder _currentOrder;
        private readonly IOrderRepository _orderRepository;
        private readonly ICoinFamilyRepository _coinFamilyRepository;
        private readonly ICoinFamilyService _coinFamilyService;

        public CoinService(
            ICurrentCurrency currentCurrency,
            ICurrentOrder currentOrder,
            IOrderRepository orderRepository,
            ICoinFamilyRepository coinFamilyRepository,
            ICoinFamilyService coinFamilyService)
        {
            _currentCurrency = currentCurrency;
            _currentOrder = currentOrder;
            _orderRepository = orderRepository;
            _coinFamilyRepository = coinFamilyRepository;
            _coinFamilyService = coinFamilyService;
        }
        public IResultTemplate AddCoin(decimal amount)
        {
            var amountValidationResult = ValidateAmount(amount);
            if (!amountValidationResult.Succeeded) return amountValidationResult;
            var currentOrderId = _currentOrder.CurrentOrderId;
            if (!currentOrderId.HasValue)
            {
                throw new ArgumentNullException(nameof(currentOrderId), "Current order id can't be null");
            }
            var currentOrder = _orderRepository.GetQuerryable().First(t => t.Id == currentOrderId);
            currentOrder.Balance += amount;
            var savedCols = _orderRepository.SaveChanges();
            var addCoinResult = _coinFamilyService.AddCoin(amount);
            if (!addCoinResult.Succeeded) return addCoinResult;
            var result = new ResultTemplate
            {
                Succeeded = savedCols > 0,
            };
            result.AppendMessageLine(new("Amount Entered: {0}{1}", amount.ToString("0.00"), _currentCurrency.Unit))
                .AppendMessageLine(new("Total balance: {0}{1}", currentOrder.Balance.ToString("0.00"), _currentCurrency.Unit));
            return result;
        }

        private IResultTemplate ValidateAmount(decimal amount)
        {
            if (amount == 0) return ResultTemplate.FailedResult("INSERT COIN");
            else if (amount <= 0 || amount > 2m)
                return ResultTemplate.FailedResult("Coin value must be larger than 0 and less than 2");
            var currencyUnit = _currentCurrency.Unit;

            if (amount % 0.05m > 0) return ResultTemplate.FailedResult("Coin value must be 0.05{0} and it's multiples", currencyUnit);

            var allCoinFamilies = _coinFamilyRepository.GetQuerryable().ToList();
            var amountMatchesFamily = allCoinFamilies.Any(cf => cf.Value == amount);
            if (!amountMatchesFamily)
            {
                var result = ResultTemplate.FailedResult("Coin family is not accepted")
                    .AppendMessageLine(new("Accepted Coins:"));
                foreach (var cf in allCoinFamilies)
                {
                    result.AppendMessageLine(new("{0}", cf.Name));
                }
                return result;
            }
            return ResultTemplate.SucceededResult();
        }
    }
}
