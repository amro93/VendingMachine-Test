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

namespace VendingMachine.Infrastructure.Orders
{
    public class CoinService : ICoinService
    {
        private readonly IAppLogger<CoinService> _logger;
        private readonly ICurrentCurreny _currentCurreny;
        private readonly ICurrentOrder _currentOrder;
        private readonly IOrderRepository _orderRepository;

        public CoinService(
            IAppLogger<CoinService> logger,
            ICurrentCurreny currentCurreny,
            ICurrentOrder currentOrder,
            IOrderRepository orderRepository
            )
        {
            _logger = logger;
            _currentCurreny = currentCurreny;
            _currentOrder = currentOrder;
            _orderRepository = orderRepository;
        }
        public IResultTemplate AddCoin(decimal amount)
        {
            var amountValidationResult = ValidateAmount(amount);
            if (!amountValidationResult.Succeeded) return amountValidationResult;
            var currentOrderId = _currentOrder.CurrentOrderId;
            if (!currentOrderId.HasValue)
            {
                _logger.LogTranslatedError("Please create a new order");
                throw new ArgumentNullException(nameof(currentOrderId));
            }
            var currentOrder = _orderRepository.GetQuerryable().First(t => t.Id == currentOrderId);
            currentOrder.Balance += amount;
            var savedCols = _orderRepository.SaveChanges();
            var result = new ResultTemplate
            {
                Succeeded = savedCols > 0,
            };
            result.AppendMessageLine(new("Amount Entered: {0}{1}", amount.ToString("0.00"), _currentCurreny.Unit))
                .AppendMessageLine(new())
                .AppendMessageLine(new("Total balance: {0}{1}", currentOrder.Balance.ToString("0.00"), _currentCurreny.Unit));
            return result;
        }

        private IResultTemplate ValidateAmount(decimal amount)
        {
            if (amount <= 0 || amount > 2m) return ResultTemplate.FailedResult("Coin value must be larger than 0 and less than 2");
            var currenyUnit = _currentCurreny.Unit;

            if (amount % 0.05m > 0) return ResultTemplate.FailedResult("Coin value must be 0.05{0} and it's multiples", currenyUnit);

            return ResultTemplate.SucceededResult();
        }
    }
}
