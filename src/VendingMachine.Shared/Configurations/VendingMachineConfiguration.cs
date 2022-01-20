using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VendingMachine.Shared.Configurations
{
    public class VendingMachineConfiguration
    {
        public VendingMachineConfiguration()
        {
            CurrentCultureName = "en";
            CurrentCurrencyUnit = "EUR";
        }
        public string CurrentCultureName { get; set; }
        public string CurrentCurrencyUnit { get; set; }
    }
}
