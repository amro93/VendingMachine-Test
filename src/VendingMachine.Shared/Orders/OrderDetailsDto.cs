using System;
using System.Collections.Generic;
using VendingMachine.Domain.Enums;

namespace VendingMachine.Shared.Orders
{
    public class OrderDetailsDto
    {
        public long Id { get; set; }
        public DateTime Date { get; set; }
        public decimal Balance { get; set; }
        public string CurrencyUnit { get; set; }
        public OrderState State { get; set; }
        public List<OrderProductDto> OrderProducts { get; set; }
    }
}
