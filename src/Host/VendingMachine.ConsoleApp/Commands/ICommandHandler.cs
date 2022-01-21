using System.Linq;
using VendingMachine.Domain.Core;

namespace VendingMachine.ConsoleApp.Commands
{
    public interface ICommandHandler
    {
        string CommandKey { get; }
        IResultTemplate Handle(string[] parameters);
        IResultTemplate Handle(string parameters) => Handle(parameters.Split(" ").Where(str => !string.IsNullOrEmpty(str)).ToArray());
        string CommandDescription { get; }
    }
}
