using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Application.Logging;
using VendingMachine.Domain.Core;

namespace VendingMachine.ConsoleApp.Application
{
    public interface IResultProcessorService
    {
        public void PrintTranslatedMessage(IResultTemplate result);
    }

    public class ResultProcessorService : IResultProcessorService
    {
        private readonly IAppLogger<ResultProcessorService> _logger;

        public ResultProcessorService(
            IAppLogger<ResultProcessorService> logger )
        {
            _logger = logger;
        }
        public void PrintTranslatedMessage(IResultTemplate result)
        {
            if (result == null) return;
            _logger.LogTranslatedResultMessage(result);
        }
    }
}
