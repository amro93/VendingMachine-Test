using System;
using System.Globalization;
using VendingMachine.Application.Services;
using VendingMachine.Domain.Core;

namespace VendingMachine.ConsoleApp.Commands.Handlers
{
    public class SelectCommandHandler : ICommandHandler
    {
        private readonly ICurrentOrder _currentOrder;

        public string CommandKey => "SELECT";

        public string CommandDescription => @"{0} [NUMBER] Select product by number";

        public SelectCommandHandler(ICurrentOrder currentOrder)
        {
            _currentOrder = currentOrder;
        }

        public IResultTemplate Handle(string[] args)
        {
            var parseResult = ParseParameters(args);
            if (!parseResult.Succeeded) return parseResult;
            return _currentOrder.SelectProduct(parseResult.Data);
        }

        private IResultTemplate<int> ParseParameters(string[] args)
        {
            if ((args?.Length ?? 0) != 1) return ResultTemplate<int>.FailedResult(CommandDescription, CommandKey);
            var param = args[0];
            var canParse = int.TryParse(param, System.Globalization.NumberStyles.Any, CultureInfo.CreateSpecificCulture("en-US"), out int value);
            if (!canParse) return ResultTemplate<int>.FailedResult("the value {0} is not a valid integer", param);
             return ResultTemplate<int>.SucceededResult(value);
        }
    }
}
