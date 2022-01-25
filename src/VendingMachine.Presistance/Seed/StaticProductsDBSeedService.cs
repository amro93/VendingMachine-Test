using VendingMachine.Application.Services;
using VendingMachine.Shared.Products;

namespace VendingMachine.Persistence.Seed
{
    public class StaticProductsDBSeedService : IDBSeedService
    {
        private readonly IProductService _productService;

        public StaticProductsDBSeedService(
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
}
