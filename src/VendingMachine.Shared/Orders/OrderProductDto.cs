namespace VendingMachine.Shared.Orders
{
    public class OrderProductDto
    {
        public long OrderProductId { get; set; }
        public long ProductId { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public int ProductQuantity { get; set; }
        public int OrderQuantity { get; set; }
        public bool IsDisposed { get; set; }
    }
}
