using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using VendingMachine.Application.Localization;
using VendingMachine.Application.Logging;
using VendingMachine.Application.Services;
using VendingMachine.Shared.Configurations;

namespace VendingMachine.ConsoleApp.Application
{
    internal class VendingMachineApplicaiton : IConsoleApp
    {
        private readonly VendingMachineConfiguration _configurationOption;
        private readonly IAppLogger<VendingMachineApplicaiton> _logger;
        private readonly IApplicationBootstrapper _applicationBootstrapper;

        public VendingMachineApplicaiton(
            IOptions<VendingMachineConfiguration> configurationOption,
            IAppLogger<VendingMachineApplicaiton> logger,
            IApplicationBootstrapper applicationBootstrapper
            )
        {
            _configurationOption = configurationOption.Value;
            _logger = logger;
            _applicationBootstrapper = applicationBootstrapper;
        }

        public void Dispose()
        {
            _logger.LogTranslatedInformation("Machine is shutting down");

        }

        public void Run(string[] args)
        {
            _applicationBootstrapper.Setup();
            //while (true)
            //{
            //    ApplicationLoop();
            //}
        }
        private void ApplicationLoop()
        {
            _configurationOption.CurrentCultureName = "fr";
            _logger.LogTranslatedInformation("Test Loop");
        }
    }
}
