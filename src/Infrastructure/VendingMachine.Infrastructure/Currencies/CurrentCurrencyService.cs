using Microsoft.Extensions.Configuration;
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
    public class CurrentCurrencyService : ICurrentCurrency
    {
        private readonly VendingMachineConfiguration _vendingMachineConfiguration;
        private readonly IConfiguration _configuration;

        public CurrentCurrencyService(
            IOptions<VendingMachineConfiguration> options,
            IConfiguration configuration)
        {
            _vendingMachineConfiguration = options.Value;
            _configuration = configuration;
        }

        public string Unit => _configuration.GetValue<string>("CurrencyUnit", "EUR");

        public IResultTemplate SetCurrentUnit(string currencyUnit)
        {
            _vendingMachineConfiguration.CurrentCurrencyUnit = currencyUnit;
            return ResultTemplate.SucceededResult();
        }
    }
}
