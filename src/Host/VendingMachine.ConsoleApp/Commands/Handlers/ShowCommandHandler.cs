using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Application.Services;
using VendingMachine.Domain.Core;
using VendingMachine.Shared.Products;

namespace VendingMachine.ConsoleApp.Commands.Handlers
{
    public class ShowCommandHandler : ICommandHandler
    {
        private readonly IProductService _productService;
        private readonly ICurrentCurrency _currentCurrency;

        public ShowCommandHandler(IProductService productService,
            ICurrentCurrency currentCurrency)
        {
            _productService = productService;
            _currentCurrency = currentCurrency;
        }
        public string CommandKey => "SHOW";

        public string CommandDescription => @"{0} Lists all products";

        public IResultTemplate Handle(string[] args)
        {
            var validationResult = ValidateArgs(args);
            if (!(validationResult?.Succeeded ?? false)) return validationResult;
            var productsResult = _productService.List();
            if (!(productsResult?.Succeeded ?? false)) return productsResult;

            foreach (var prod in productsResult?.Data ?? new List<ProductListDto>())
            {
                var currencyUnit = _currentCurrency.Unit;
                if (prod.Quantity > 0)
                {
                    productsResult.AppendMessageLine(new("{0}. {1} {2}{3} - {4} Item Left", prod.Id, prod.Name, prod.Price, currencyUnit, prod.Quantity));
                }
                else
                {
                    productsResult.AppendMessageLine(new("{0}. {1} {2}{3} - SOLD OUT", prod.Id, prod.Name, prod.Price, currencyUnit));
                }
            }

            return productsResult;
        }

        private IResultTemplate ValidateArgs(string[] args)
        {
            if ((args?.Length ?? 0) != 0) return ResultTemplate.FailedResult(CommandDescription, CommandKey);
            return ResultTemplate.SucceededResult();
        }
    }
}
