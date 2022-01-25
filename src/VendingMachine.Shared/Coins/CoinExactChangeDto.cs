namespace VendingMachine.Shared.Coins
{
    public class CoinExactChangeDto
    {
        public long CoinFamilyId { get; set; }
        public int Count { get; set; }
        public decimal Value { get; set; }
        public string Name { get; set; }
    }
}
