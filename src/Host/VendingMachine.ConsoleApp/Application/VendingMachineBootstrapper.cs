using System.Collections.Generic;
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
        private readonly IEnumerable<IDBSeedService> _dBSeedServices;

        public VendingMachineBootstrapper(
            IAppLogger<VendingMachineBootstrapper> logger,
            IOrderService orderService,
            ICoinService coinService,
            IEnumerable<IDBSeedService> dBSeedServices
            )
        {
            _logger = logger;
            _orderService = orderService;
            _coinService = coinService;
            _dBSeedServices = dBSeedServices;
        }
        public void Setup()
        {
            _logger.LogTranslatedInformation("Preparing application data");
            _logger.LogTranslatedInformation("Closing old orders");
            _orderService.CloseAllOrders();

            _logger.LogTranslatedInformation("Creating new order");
            _orderService.CreateNewOrder();

            _logger.LogTranslatedInformation("Seeding Data");

            foreach (var svc in _dBSeedServices)
            {
                svc.Seed();
            }

            _logger.LogTranslatedInformation("Done Seeding");
        }
    }
}
