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
        private readonly IDBSeedService _dBSeedService;

        public VendingMachineBootstrapper(
            IAppLogger<VendingMachineBootstrapper> logger,
            IOrderService orderService,
            ICoinService coinService,
            IDBSeedService dBSeedService
            )
        {
            _logger = logger;
            _orderService = orderService;
            _coinService = coinService;
            _dBSeedService = dBSeedService;
        }
        public void Setup()
        {
            _logger.LogTranslatedInformation("Preparing application data");
            _logger.LogTranslatedInformation("Closing old orders");
            _orderService.CloseAllOrders();

            _logger.LogTranslatedInformation("Creating new order");
            _orderService.CreateNewOrder();
            _dBSeedService.Seed();
        }
    }
}
