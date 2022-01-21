using VendingMachine.Domain.Core;

namespace VendingMachine.Application.Services
{
    public interface ICurrentCurreny
    {
        public string Unit { get; }
        public IResultTemplate SetCurrentUnit(string currencyUnit);
    }
}
