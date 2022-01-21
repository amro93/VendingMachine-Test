using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Application.Repositories;
using VendingMachine.Application.Services;
using VendingMachine.Domain.Core;
using VendingMachine.Domain.Enums;
using VendingMachine.Shared.Orders;

namespace VendingMachine.Infrastructure.Orders
{
    public class CurrentOrderService : ICurrentOrder
    {
        private readonly IOrderRepository _orderRepository;

        public CurrentOrderService(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }
        public long? CurrentOrderId => _orderRepository.GetOpenedOrdersQuery().Select(t => (long?)t.Id).FirstOrDefault();

        public IResultTemplate<CurrentOrderDto> GetCurrentOrderDetails()
        {
            var query = _orderRepository.GetOpenedOrdersQuery()
                .Select(t => new CurrentOrderDto
                {
                    Id = t.Id,
                    Balance = t.Balance,
                    CurrencyUnit = t.CurrencyUnit,
                    Date = t.Date,
                    State = t.State,
                    OrderProducts = t.OrderProducts.Select(op => new OrderProductDto
                    {
                        OrderProductId = op.Id,
                        IsDisposed = op.IsDisposed,
                        OrderQuantity = op.Quantity,
                        ProductId = op.ProductId,
                        ProductName = op.Product.Name,
                        ProductPrice = op.Product.Price,
                        ProductQuantity = op.Product.Quantity
                    }).ToList()
                });
            var result = query.FirstOrDefault();
            return new ResultTemplate<CurrentOrderDto>
            {
                Succeeded = true,
                Data = result
            };
        }
    }
}
