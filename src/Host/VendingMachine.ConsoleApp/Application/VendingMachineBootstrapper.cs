using VendingMachine.Application.Logging;
using VendingMachine.Application.Services;
using VendingMachine.Shared.Products;

namespace VendingMachine.ConsoleApp.Application
{
    public class VendingMachineBootstrapper : IApplicationBootstrapper
    {
        private readonly IAppLogger<VendingMachineBootstrapper> _logger;
        private readonly IOrderService _orderService;
        private readonly ICoinService _coinService;
        private readonly IProductService _productService;

        public VendingMachineBootstrapper(
            IAppLogger<VendingMachineBootstrapper> logger,
            IOrderService orderService,
            ICoinService coinService,
            IProductService productService)
        {
            _logger = logger;
            _orderService = orderService;
            _coinService = coinService;
            _productService = productService;
        }
        public void Setup()
        {
            _logger.LogTranslatedInformation("Preparing application data");
            _logger.LogTranslatedInformation("Closing old orders");
            _orderService.CloseAllOrders();

            _logger.LogTranslatedInformation("Creating new order");
            _orderService.CreateNewOrder();
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
