using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.ConsoleApp.Commands.Handlers;
using VendingMachine.Domain.Core;

namespace VendingMachine.ConsoleApp.Commands
{
    public interface ICommandHandlerFactory
    {
        IResultTemplate<ICommandHandler> GetCommandHandler(string command);
    }
}
