using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Application.Logging;

namespace VendingMachine.ConsoleApp.Commands
{
    public class CommandHandlerFactory : ICommandHandlerFactory
    {
        private readonly IEnumerable<ICommandHandler> _commandHandlers;
        private readonly IAppLogger<CommandHandlerFactory> _logger;

        public CommandHandlerFactory(IEnumerable<ICommandHandler> commandHandlers,
            IAppLogger<CommandHandlerFactory> logger)
        {
            _commandHandlers = commandHandlers;
            _logger = logger;
        }

        public ICommandHandler GetCommandHandler(string commandKey)
        {
            var cmdHandler = _commandHandlers.FirstOrDefault(t => t.CommandKey.ToUpper() == commandKey.ToUpper());
            if (cmdHandler == null)
            {
                _logger.LogTranslatedError("Command {0} not found, enter {1} to list all available commands", commandKey, "HELP");
            }

            return cmdHandler;
        }
    }
}
