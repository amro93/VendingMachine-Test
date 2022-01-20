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
        public bool AddCoin(decimal amount)
        {
            if (!ValidateAmount(amount)) return false;
            var currentOrderId = _currentOrder.CurrentOrderId;
            if(!currentOrderId.HasValue)
            {
                _logger.LogTranslatedError("Please create a new order");
                return false;
            }
            var currentOrder = _orderRepository.GetQuerryable().First(t => t.Id == currentOrderId);
            currentOrder.Balance += amount;
            var savedCols = _orderRepository.SaveChanges();
            _logger.LogTranslatedInformation("Current amount = {0}{1}", amount.ToString("0.00"), _currentCurreny.Unit);
            return savedCols > 0;
        }

        private bool ValidateAmount(decimal amount)
        {
            if(amount == 0)
            {
                _logger.LogTranslatedError("You didn't enter any coins");
                return false;
            }
            if(amount % 0.05m > 0)
            {
                var currenyUnit = _currentCurreny.Unit;
                _logger.LogTranslatedError("Coin value must be 0.05{0} and it's multiples", currenyUnit);
                return false;
            }

            return true;
        }
    }
}
