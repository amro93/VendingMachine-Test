using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Application.Localization;
using VendingMachine.Application.Logging;
using VendingMachine.Application.Services;
using VendingMachine.Domain.Core;

namespace VendingMachine.ConsoleApp.Commands
{
    public class EnterCommandHandler : ICommandHandler
    {
        private readonly ILocalizationService _localizationService;
        private readonly ICoinService _coinService;
        private readonly ICurrentOrder _currentOrder;
        private readonly IAppLogger<EnterCommandHandler> _logger;

        public EnterCommandHandler(ILocalizationService localizationService,
            ICoinService coinService,
            ICurrentOrder currentOrder,
            IAppLogger<EnterCommandHandler> logger)
        {
            _localizationService = localizationService;
            _coinService = coinService;
            _currentOrder = currentOrder;
            _logger = logger;
        }
        public string CommandKey => "ENTER";
        public string CommandDescription => @"{0} <XXX> 
Enter coin to the vending machine, it will accept valid coins (5cts to 2€) and reject invalid ones (1 and 2 cts).
All coins entered by user are in same currency than Vending machine. Cents are represented by two digits decimals i.e. 0.10 for 10cts."
            ;

        public IResultTemplate Handle(string[] parameters)
        {
            var parseResult = TryParseParameter(parameters);
            if (!parseResult.Succeeded) return parseResult;

            var coinAddResult = _coinService.AddCoin(parseResult.Data);
            return coinAddResult;
        }

        private IResultTemplate<decimal> TryParseParameter(string[] parameters)
        {
            if (parameters.Length != 1) return ResultTemplate<decimal>.FailedResult(CommandDescription, CommandKey);

            var parm = parameters[0];
            var canParse = decimal.TryParse(parm, out var value);
            if (!canParse) return ResultTemplate<decimal>.FailedResult("Entered amount is not a valid number");
            
            return ResultTemplate<decimal>.SucceededResult(value);
        }
    }
}
