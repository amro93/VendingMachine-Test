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
        private string currentCultureName;
        public string CurrentCultureName { get => currentCultureName; set => currentCultureName = value.ToLower(); }
        public string CurrentCurrencyUnit { get; set; }
    }
}
