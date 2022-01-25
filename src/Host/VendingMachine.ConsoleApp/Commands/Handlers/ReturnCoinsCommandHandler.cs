using System;
using VendingMachine.Domain.Core;

namespace VendingMachine.ConsoleApp.Commands.Handlers
{
    public class ReturnCoinsCommandHandler : ICommandHandler
    {
        public string CommandKey => "RETURN COINS";
        public string CommandDescription => @"{0} Returns coins";

        public IResultTemplate Handle(string[] args)
        {

            return ResultTemplate.SucceededResult();
        }
    }
}
