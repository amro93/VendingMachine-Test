using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Application.Services;
using VendingMachine.Domain.Core;
using VendingMachine.Shared.Configurations;

namespace VendingMachine.Infrastructure.Currencies
{
    public class CurrentCurrencyService : ICurrentCurreny
    {
        private readonly VendingMachineConfiguration _vendingMachineConfiguration;

        public CurrentCurrencyService(
            IOptions<VendingMachineConfiguration> options)
        {
            _vendingMachineConfiguration = options.Value;
        }

        public string Unit => _vendingMachineConfiguration.CurrentCurrencyUnit;

        public IResultTemplate SetCurrentUnit(string currencyUnit)
        {
            _vendingMachineConfiguration.CurrentCurrencyUnit = currencyUnit;
            return ResultTemplate.SucceededResult();
        }
    }
}
