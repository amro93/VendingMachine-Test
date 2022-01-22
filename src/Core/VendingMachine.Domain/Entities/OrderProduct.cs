using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Domain.Enums;

namespace VendingMachine.Domain.Entities
{
    public class OrderProduct : IBaseEntity<long>
    {
        public long Id { get; set; }
        public long ProductId { get; set; }
        public Product Product { get; set; }
        public long OrderId { get; set; }
        public Order Order { get; set; }
        public bool IsDisposed { get; set; }
    }
}
