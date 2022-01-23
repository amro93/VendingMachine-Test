using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using VendingMachine.Application.Localization;
using VendingMachine.Application.Logging;
using VendingMachine.Application.Services;
using VendingMachine.ConsoleApp.Commands;
using VendingMachine.Domain.Core;
using VendingMachine.Shared.Configurations;

namespace VendingMachine.ConsoleApp.Application
{
    internal class VendingMachineApplicaiton : IConsoleApp
    {
        private readonly VendingMachineConfiguration _configurationOption;
        private readonly IAppLogger<VendingMachineApplicaiton> _logger;
        private readonly IApplicationBootstrapper _applicationBootstrapper;
        private readonly ILocalizationService _localizationService;
        private readonly ICommandHandlerFactory _commandHandlerFactory;
        private readonly IResultProcessorService _resultProcessorService;

        public VendingMachineApplicaiton(
            IOptions<VendingMachineConfiguration> configurationOption,
            IAppLogger<VendingMachineApplicaiton> logger,
            IApplicationBootstrapper applicationBootstrapper,
            ILocalizationService localizationService,
            ICommandHandlerFactory commandHandlerFactory,
            IResultProcessorService resultProcessorService
            )
        {
            _configurationOption = configurationOption.Value;
            _logger = logger;
            _applicationBootstrapper = applicationBootstrapper;
            _localizationService = localizationService;
            _commandHandlerFactory = commandHandlerFactory;
            _resultProcessorService = resultProcessorService;
        }

        public void Dispose()
        {
            _logger.LogTranslatedInformation("Machine is shutting down");

        }

        public void Run(string[] args)
        {
            _applicationBootstrapper.Setup();
            while (true)
            {
                ApplicationLoop();
            }
        }
        private void ApplicationLoop()
        {
            _logger.LogTranslatedInformation("Enter a Command: ");
            var command = Console.ReadLine();
            IResultTemplate result = null;
            var (cmdKey, cmdParams) = ProcessCommand(command);
            var cmdHandlerResult = _commandHandlerFactory.GetCommandHandler(cmdKey);
            if(cmdHandlerResult?.Succeeded == true) result = cmdHandlerResult?.Data?.Handle(cmdParams);
            else
            {
                cmdHandlerResult = _commandHandlerFactory.GetCommandHandler(command);
                if (cmdHandlerResult == null || !cmdHandlerResult.Succeeded) result = cmdHandlerResult;
                else result = cmdHandlerResult?.Data?.Handle(Array.Empty<string>());
            }
            _resultProcessorService.PrintTranslatedMessage(result);
        }

        private (string key, string[] parameters) ProcessCommand(string command)
        {
            var commandsArray = command?.Split(" ")?.Where(str => !string.IsNullOrEmpty(str)) ?? Array.Empty<string>();
            var cmdKey = commandsArray.FirstOrDefault();
            var cmdParams = commandsArray.Skip(1).ToArray();
            return (cmdKey, cmdParams);
        }
    }
}
