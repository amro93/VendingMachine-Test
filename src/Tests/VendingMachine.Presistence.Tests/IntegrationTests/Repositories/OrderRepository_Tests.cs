using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Application.Repositories;
using VendingMachine.Domain.Entities;
using VendingMachine.Domain.Enums;

namespace VendingMachine.Persistence.Tests.IntegrationTests.Repositories
{
    [TestClass]
    public class OrderRepository_Tests : UnitTestBase
    {
        private readonly IOrderRepository _orderRepository;
        public OrderRepository_Tests()
        {
            _orderRepository = ServiceProvider.GetRequiredService<IOrderRepository>();
        }

        private Order InsertOrder(long id)
        {
            var dbOrder = _orderRepository.GetQuerryable().FirstOrDefault(t => t.Id == id);
            if (dbOrder is not null) return dbOrder;
            Order order = new()
            {
                Id = id,
                Balance = 0,
                CurrencyUnit = "EUR",
                Date = DateTime.UtcNow,
                State = Domain.Enums.OrderState.Opened
            };
            _orderRepository.Create(order);
            _orderRepository.SaveChanges();

            return order;
        }
        [TestMethod]
        public void CreateOrder_ReturnNewOrder()
        {
            var order = InsertOrder(1);

            var dbOrder = _orderRepository.GetQuerryable().FirstOrDefault(t => t.Id == 1);
            Assert.IsNotNull(dbOrder);
            Assert.AreEqual(order.Id, dbOrder.Id);
            Assert.AreEqual(order.Balance, dbOrder.Balance);
            Assert.AreEqual(order.CurrencyUnit, dbOrder.CurrencyUnit);
            Assert.AreEqual(order.Date, dbOrder.Date);
            Assert.AreEqual(order.State, dbOrder.State);
            
        }

        [TestMethod]
        public void UpdateOrder_ReturnUpdtedOrder()
        {
            var order = InsertOrder(1);

            var dbOrder = _orderRepository.GetQuerryable().FirstOrDefault(t => t.Id == 1);


            dbOrder.CurrencyUnit = "USD";
            dbOrder.State = OrderState.Closed;
            _orderRepository.SaveChanges();

            var order2 = _orderRepository.GetQuerryable().FirstOrDefault(t => t.Id == 1);

            Assert.IsNotNull(dbOrder);
            Assert.AreEqual(order2.Id, dbOrder.Id);
            Assert.AreEqual(order2.Balance, dbOrder.Balance);
            Assert.AreEqual(order2.CurrencyUnit, dbOrder.CurrencyUnit);
            Assert.AreEqual(order2.Date, dbOrder.Date);
            Assert.AreEqual(order2.State, dbOrder.State);
        }

        [TestMethod]
        public void DeleteOrder_ReturnsTrue()
        {
            var order = InsertOrder(1);
            var dbOrder = _orderRepository.GetQuerryable().FirstOrDefault(t => t.Id == 1);
            _orderRepository.Delete(dbOrder);
            var savedChanges = _orderRepository.SaveChanges();
            var dbOrder2 = _orderRepository.GetQuerryable().FirstOrDefault(t => t.Id == 1);

            Assert.IsNotNull(dbOrder);
            Assert.IsTrue(savedChanges > 0);
            Assert.IsNull(dbOrder2);
        }

        [TestMethod]
        public void GetOrderById_ReturnsNotNull()
        {
            var order = InsertOrder(2);
            var dbOrder = _orderRepository.GetQuerryable().FirstOrDefault(t => t.Id == 2);

            Assert.IsNotNull(dbOrder);
        }

        [TestMethod]
        public void NotFoundOrderById_ReturnsNull()
        {
            var order = InsertOrder(2);
            var dbOrder = _orderRepository.GetQuerryable().FirstOrDefault(t => t.Id == 2);

            Assert.IsNotNull(dbOrder);
        }

        [TestMethod]
        public void ListOrders_ReturnsOrdersList()
        {
            var order1 = InsertOrder(1);
            var order2 = InsertOrder(2);
            var dbOrders = _orderRepository.GetQuerryable().OrderBy(t => t.Id).ToList();

            var dbOrder1 = dbOrders.FirstOrDefault(t => t.Id == 1);
            var dbOrder2 = dbOrders.FirstOrDefault(t => t.Id == 2);
            Assert.IsNotNull(dbOrders);
            Assert.IsNotNull(dbOrder1);
            Assert.IsNotNull(dbOrder2);

            Assert.IsTrue(dbOrders.Count > 0);

            Assert.AreEqual(order1.Id, dbOrder1.Id);
            Assert.AreEqual(order1.Balance, dbOrder1.Balance);
            Assert.AreEqual(order1.CurrencyUnit, dbOrder1.CurrencyUnit);
            Assert.AreEqual(order1.Date, dbOrder1.Date);
            Assert.AreEqual(order1.State, dbOrder1.State);

            Assert.AreEqual(order2.Id, dbOrder2.Id);
            Assert.AreEqual(order2.Balance, dbOrder2.Balance);
            Assert.AreEqual(order2.CurrencyUnit, dbOrder2.CurrencyUnit);
            Assert.AreEqual(order2.Date, dbOrder2.Date);
            Assert.AreEqual(order2.State, dbOrder2.State);
        }
    }
}
