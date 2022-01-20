﻿using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Application.Localization;
using VendingMachine.Application.Logging;
using VendingMachine.Application.Repositories;
using VendingMachine.Application.Services;
using VendingMachine.Domain.Entities;
using VendingMachine.Domain.Enums;

namespace VendingMachine.Infrastructure.Orders
{
    public class OrderService : IOrderService
    {
        private readonly ICurrentOrder _currentOrder;
        private readonly IAppLogger<OrderService> _logger;
        private readonly IOrderRepository _orderRepository;
        private readonly ICurrentCurreny _currentCurreny;

        public OrderService(ICurrentOrder currentOrder,
            IAppLogger<OrderService> logger,
            IOrderRepository orderRepository,
            ICurrentCurreny currentCurreny)
        {
            _currentOrder = currentOrder;
            _logger = logger;
            _orderRepository = orderRepository;
            _currentCurreny = currentCurreny;
        }

        public long? CreateNewOrder()
        {
            if(_currentOrder.CurrentOrderId.HasValue)
            {
                _logger.LogTranslatedError("Current order is not closed please try {0} command", "CLOSE");
                return null;
            }
            var order = new Order
            {
                State = OrderState.Opened,
                Date = DateTime.UtcNow,
                CurrencyUnit = _currentCurreny.Unit,
            };
            _orderRepository.Create(order);
            _orderRepository.SaveChanges();
            return order.Id;
        }

        public void CloseAllOrders()
        {
            var openOrders = _orderRepository.GetOpenedOrdersQuery();
            foreach(var order in openOrders)
            {
                order.State = OrderState.Closed;
            }
            _orderRepository.SaveChanges();
        }
    }
}