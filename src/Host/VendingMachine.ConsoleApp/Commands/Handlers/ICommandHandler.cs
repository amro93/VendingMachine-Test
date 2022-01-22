using System.Linq;
using VendingMachine.Domain.Core;

namespace VendingMachine.ConsoleApp.Commands.Handlers
{
    public interface ICommandHandler
    {
        string CommandKey { get; }
        IResultTemplate Handle(string[] args);
        IResultTemplate Handle(string args) => Handle(args.Split(" ").Where(str => !string.IsNullOrEmpty(str)).ToArray());
        string CommandDescription { get; }
    }
}
