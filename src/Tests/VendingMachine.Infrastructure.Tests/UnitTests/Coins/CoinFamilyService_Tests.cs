using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Application.Services;
using VendingMachine.Domain.Entities;

namespace VendingMachine.Infrastructure.Tests.UnitTests.Coins
{
    [TestClass]
    public class CoinFamilyService_Tests : UnitTestBase
    {
        private readonly ICoinFamilyService _coinFamilyService;
        public CoinFamilyService_Tests()
        {
            _coinFamilyService = ServiceProvider.GetRequiredService<ICoinFamilyService>();
        }

        [TestMethod]
        public void GetExactChange_NoCoins_ReturnsEmptyList()
        {
            List<CoinFamily> coinFamilies = new List<CoinFamily>
            {
                new() { Id = 1, Name = "5 Cts", Value = 0.05m, Quantity = 0},
                new() { Id = 2, Name = "10 Cts", Value = 0.10m, Quantity = 0},
                new() { Id = 3, Name = "20 Cts", Value = 0.20m, Quantity = 0},
                new() { Id = 4, Name = "50 Cts", Value = 0.50m, Quantity = 0},
                new() { Id = 5, Name = "1 EURO", Value = 1m, Quantity = 0},
                new() { Id = 6, Name = "2 EURO", Value = 2m, Quantity = 0}
            };

            var result = _coinFamilyService.GetExactChange(coinFamilies, 20);
            Assert.IsTrue(result.ExactChange == 0);
            Assert.IsTrue(result.Coins.Count() == 0);
        }

        [TestMethod]
        public void GetExactChange_WithAllCoins_ReturnsList()
        {
            List<CoinFamily> coinFamilies = new List<CoinFamily>
            {
                new() { Id = 1, Name = "5 Cts", Value = 0.05m, Quantity = 50},
                new() { Id = 2, Name = "10 Cts", Value = 0.10m, Quantity = 50},
                new() { Id = 3, Name = "20 Cts", Value = 0.20m, Quantity = 50},
                new() { Id = 4, Name = "50 Cts", Value = 0.50m, Quantity = 50},
                new() { Id = 5, Name = "1 EURO", Value = 1m, Quantity = 50},
                new() { Id = 6, Name = "2 EURO", Value = 2m, Quantity = 50}
            };

            var result = _coinFamilyService.GetExactChange(coinFamilies, 3);
            Assert.AreEqual(result.ExactChange, 3);
            Assert.IsTrue(result.Coins.Where(t => t.Value == 2 && t.Count == 1).Any());
            Assert.IsTrue(result.Coins.Where(t => t.Value == 1 && t.Count == 1).Any());

            var result2 = _coinFamilyService.GetExactChange(coinFamilies, 4);
            Assert.AreEqual(result2.ExactChange, 4);
            Assert.IsTrue(result2.Coins.Where(t => t.Value == 2 && t.Count == 2).Any());

            var result3 = _coinFamilyService.GetExactChange(coinFamilies, 0.05m);
            Assert.AreEqual(result3.ExactChange, 0.05m);
            Assert.IsTrue(result3.Coins.Where(t => t.Value == 0.05m && t.Count == 1).Any());

            var result4 = _coinFamilyService.GetExactChange(coinFamilies, 0.65m);
            Assert.AreEqual(result4.ExactChange, 0.65m);
            Assert.IsTrue(result4.Coins.Where(t => t.Value == 0.5m && t.Count == 1).Any());
            Assert.IsTrue(result4.Coins.Where(t => t.Value == 0.1m && t.Count == 1).Any());
            Assert.IsTrue(result4.Coins.Where(t => t.Value == 0.05m && t.Count == 1).Any());

            var result5 = _coinFamilyService.GetExactChange(coinFamilies, 0.065m);
            Assert.AreEqual(result5.ExactChange, 0.05m);
            Assert.AreEqual(result5.Coins.Count(), 1);
            Assert.IsTrue(result5.Coins.Where(t => t.Value == 0.05m && t.Count == 1).Any());
        }

        [TestMethod]
        public void GetExactChange_SomeCoins_ReturnsList()
        {

        }
    }
}
