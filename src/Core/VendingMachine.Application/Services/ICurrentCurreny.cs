namespace VendingMachine.Application.Services
{
    public interface ICurrentCurreny
    {
        public string Unit { get; }
        public void SetCurrentUnit(string currencyUnit);
    }
}
