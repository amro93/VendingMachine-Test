using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.Application.Services
{
    public interface ICoinService
    {
        /// <summary>
        /// Add Coin to current order
        /// </summary>
        /// <param name="amount">amount of currency must be 0.05 and it's multiples</param>
        /// <returns>true if amount is added</returns>
        bool AddCoin(decimal amount);
    }
}
