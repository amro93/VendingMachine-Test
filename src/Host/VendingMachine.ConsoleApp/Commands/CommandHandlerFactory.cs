using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Application.Logging;
using VendingMachine.ConsoleApp.Commands.Handlers;
using VendingMachine.Domain.Core;

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

        public IResultTemplate<ICommandHandler> GetCommandHandler(string commandKey)
        {
            var cmdHandler = _commandHandlers.FirstOrDefault(t => t.CommandKey.ToUpper() == commandKey?.ToUpper());
            if (cmdHandler == null)
            {
                return ResultTemplate<ICommandHandler>.FailedResult("Command {0} not found, enter {1} to list all available commands", commandKey, "HELP");
            }

            return ResultTemplate<ICommandHandler>.SucceededResult(cmdHandler);
        }
    }
}
