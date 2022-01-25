using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Domain.Core;
using VendingMachine.Domain.Entities;
using VendingMachine.Shared.Coins;

namespace VendingMachine.Application.Services
{
    public interface ICoinFamilyService
    {
        IResultTemplate AddCoin(decimal amount);
        GetExactChangeResult GetExactChange(IEnumerable<CoinFamily> coinFamilies, decimal value);
        GetExactChangeResult GetExactChange(decimal value);
        IResultTemplate RemoveCoin(CoinFamilyRemoveDto dto);
    }
}
