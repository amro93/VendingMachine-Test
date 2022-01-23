using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Domain.Core;

namespace VendingMachine.Application.Services
{
    public interface IOrderService
    {
        IResultTemplate CloseAllOrders();
        IResultTemplate CloseOrder(long orderId);

        /// <summary>
        /// Create new order
        /// </summary>
        /// <returns>Current order id</returns>
        IResultTemplate<long> CreateNewOrder();

    }
}
