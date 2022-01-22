using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Application.Repositories;
using VendingMachine.Application.Services;
using VendingMachine.Domain.Core;
using VendingMachine.Domain.Entities;
using VendingMachine.Domain.Enums;
using VendingMachine.Shared.Orders;

namespace VendingMachine.Infrastructure.Orders
{
    public class CurrentOrderService : ICurrentOrder
    {
        private readonly IOrderRepository _orderRepository;
        private readonly ICurrentCurreny _currentCurreny;
        private readonly IOrderProductRepository _orderProductRepository;
        private readonly IServiceProvider _serviceProvider;

        public CurrentOrderService(IOrderRepository orderRepository,
            ICurrentCurreny currentCurreny,
            IOrderProductRepository orderProductRepository,
            IServiceProvider serviceProvider)
        {
            _orderRepository = orderRepository;
            _currentCurreny = currentCurreny;
            _orderProductRepository = orderProductRepository;
            _serviceProvider = serviceProvider;
        }
        public long? CurrentOrderId => _orderRepository.GetOpenedOrdersQuery().Select(t => (long?)t.Id).FirstOrDefault();

        public IResultTemplate AddProduct(long productId)
        {
            var productService = _serviceProvider.GetRequiredService<IProductService>();
            var selectedProductResult = productService.GetById(productId);
            if (!selectedProductResult.Succeeded) return selectedProductResult;
            var selectedProduct = selectedProductResult.Data;

            if (selectedProduct.Quantity <= 0) return ResultTemplate.FailedResult("SOLD OUT");

            var currentOrderDetails = GetCurrentOrderDetails().Data;
            var hasBalance = (currentOrderDetails.Balance - selectedProduct.Price) >= 0;
            if (!hasBalance) return ResultTemplate.FailedResult(
                 "You don't have enough balance.\n Selected product price = {0}{1}. \n Your current balance = {2}{3}",
                 selectedProduct.Price,
                 _currentCurreny.Unit,
                 currentOrderDetails.Balance,
                 _currentCurreny.Unit
                 );

            _orderProductRepository.Create(new OrderProduct
            {
                OrderId = currentOrderDetails.Id,
                ProductId = selectedProduct.Id,
                IsDisposed = true
            });

            _orderProductRepository.SaveChanges();
            // Query order again
            currentOrderDetails = GetCurrentOrderDetails().Data;

            var result = ResultTemplate.SucceededResult("Product {0}.{1} selected", productId, selectedProduct.Name);
            if (currentOrderDetails.Balance > 0)
            {
                result.AppendMessageLine(new("Please take change: {0}{1}", currentOrderDetails.Balance, _currentCurreny.Unit));
            }
            return result.AppendMessageLine(new("Thank You"));
        }

        public IResultTemplate CancelOrder()
        {
            throw new NotImplementedException();
        }

        public IResultTemplate Close()
        {
            throw new NotImplementedException();
        }

        public IResultTemplate DisposeOrderItems()
        {
            throw new NotImplementedException();
        }

        public IResultTemplate<CurrentOrderDto> GetCurrentOrderDetails()
        {
            var query = _orderRepository.GetOpenedOrdersQuery()
                .Select(t => new CurrentOrderDto
                {
                    Id = t.Id,
                    Balance = t.Balance - t.OrderProducts.Sum(t => t.Product.Price),
                    CurrencyUnit = t.CurrencyUnit,
                    Date = t.Date,
                    State = t.State,
                    OrderProducts = t.OrderProducts.Select(op => new OrderProductDto
                    {
                        OrderProductId = op.Id,
                        IsDisposed = op.IsDisposed,
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
