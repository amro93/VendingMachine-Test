using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.Shared.Coins
{
    public class GetExactChangeResult
    {
        public GetExactChangeResult()
        {
            Coins = new List<CoinExactChangeDto>();
        }
        public List<CoinExactChangeDto> Coins { get; set; }
        public decimal ExactChange => Coins.Sum(c => c.Value * c.Count);
    }
}
