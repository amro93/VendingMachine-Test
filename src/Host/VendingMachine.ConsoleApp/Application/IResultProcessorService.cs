using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Domain.Core;

namespace VendingMachine.ConsoleApp.Application
{
    public interface IResultProcessorService
    {
        public void PrintTranslatedMessage(IResultTemplate result);
        public void PrintMessage(IResultTemplate result);
    }
}
