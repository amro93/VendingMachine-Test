using VendingMachine.Domain.Core;

namespace VendingMachine.Application.Services
{
    public interface ICurrentCurrency
    {
        public string Unit { get; }
        //public IResultTemplate SetCurrentUnit(string currencyUnit);
    }
}
