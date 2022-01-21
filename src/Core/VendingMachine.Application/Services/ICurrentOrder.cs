using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Domain.Core;
using VendingMachine.Shared.Orders;

namespace VendingMachine.Application.Services
{
    public interface ICurrentOrder
    {
        public long? CurrentOrderId { get; }
        public IResultTemplate<CurrentOrderDto> GetCurrentOrderDetails();
    }
}
