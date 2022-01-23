using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Application.Services;
using VendingMachine.Shared.Products;

namespace VendingMachine.ConsoleApp.Application
{
    public interface IDBSeedService
    {
        void Seed();
    }

    public class StaticDBSeedService : IDBSeedService
    {
        private readonly IProductService _productService;

        public StaticDBSeedService(
            IProductService productService)
        {
            _productService = productService;
        }
        public void Seed()
        {
            _productService.Create(new ProductCreateDto
            {
                Name = "COLA",
                Price = 1,
                Quantity = 8
            });

            _productService.Create(new ProductCreateDto
            {
                Name = "Chips",
                Price = 0.50m,
                Quantity = 12
            });

            _productService.Create(new ProductCreateDto
            {
                Name = "Candy",
                Price = 0.65m,
                Quantity = 0
            });
        }
    }

    public class CsvDBSeedService : IDBSeedService
    {
        public void Seed()
        {

        }
    }
}
