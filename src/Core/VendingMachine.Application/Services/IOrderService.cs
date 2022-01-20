using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.Application.Services
{
    public interface IOrderService
    {
        void CloseAllOrders();

        /// <summary>
        /// Create new order
        /// </summary>
        /// <returns>Current order id</returns>
        long? CreateNewOrder();
    }
}
