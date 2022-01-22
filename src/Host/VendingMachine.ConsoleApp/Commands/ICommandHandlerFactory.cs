using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.ConsoleApp.Commands.Handlers;

namespace VendingMachine.ConsoleApp.Commands
{
    public interface ICommandHandlerFactory
    {
        ICommandHandler GetCommandHandler(string command);
    }
}
