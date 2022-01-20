using VendingMachine.Application.Logging;
using VendingMachine.Application.Services;

namespace VendingMachine.ConsoleApp.Application
{
    public class VendingMachineBootstrapper : IApplicationBootstrapper
    {
        private readonly IAppLogger<VendingMachineBootstrapper> _logger;
        private readonly IOrderService _orderService;
        private readonly ICoinService _coinService;

        public VendingMachineBootstrapper(
            IAppLogger<VendingMachineBootstrapper> logger,
            IOrderService orderService,
            ICoinService coinService)
        {
            _logger = logger;
            _orderService = orderService;
            _coinService = coinService;
        }
        public void Setup()
        {
            _logger.LogTranslatedInformation("Preparing application data");
            _logger.LogTranslatedInformation("Closing old orders");
            _orderService.CloseAllOrders();

            _logger.LogTranslatedInformation("Creating new order");
            _orderService.CreateNewOrder();
            _coinService.AddCoin(0.15m);
        }
    }
}
