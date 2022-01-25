using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Domain.Core;

namespace VendingMachine.ConsoleApp.Commands.Handlers
{
    public class HelpCommandHandler : ICommandHandler
    {
        private readonly IServiceProvider _serviceProvider;

        public HelpCommandHandler(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }
        public string CommandKey => "HELP";

        public string CommandDescription => @"{0} Shows help text";

        public IResultTemplate Handle(string[] args)
        {
            var validationResult = ValidateParameters(args);
            if (!validationResult.Succeeded) return validationResult;
            var commandHandlers = _serviceProvider.GetRequiredService<IEnumerable<ICommandHandler>>();
            var result = ResultTemplate.SucceededResult();
            foreach (var cmdHandler in commandHandlers)
            {
                result.AppendMessageLine(new(cmdHandler.CommandDescription, cmdHandler.CommandKey)).
                    AppendMessageLine(new("===========================================") { SkipLocalize = true});
            }
            return result;
        }

        public IResultTemplate ValidateParameters(string[] args)
        {
            if ((args?.Length ?? 0) > 0) return ResultTemplate.FailedResult(CommandDescription, CommandKey);
            return ResultTemplate.SucceededResult();
        }
    }
}
