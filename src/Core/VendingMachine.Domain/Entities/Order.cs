using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Domain.Enums;

namespace VendingMachine.Domain.Entities
{
    public class Order : IBaseEntity<long>
    {
        public long Id { get; set; }
        public DateTime Date { get; set; }
        public decimal Balance { get; set; }
        public string CurrencyUnit { get; set; }
        public OrderState State { get; set; }
        public ICollection<OrderProduct> OrderProducts { get; set; }
    }
}
