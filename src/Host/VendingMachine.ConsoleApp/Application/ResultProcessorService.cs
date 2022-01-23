using VendingMachine.Application.Logging;
using VendingMachine.Domain.Core;

namespace VendingMachine.ConsoleApp.Application
{
    public class ResultProcessorService : IResultProcessorService
    {
        private readonly IAppLogger<ResultProcessorService> _logger;

        public ResultProcessorService(
            IAppLogger<ResultProcessorService> logger )
        {
            _logger = logger;
        }

        public void PrintMessage(IResultTemplate result)
        {
            if (result == null) return;
            _logger.LogResultMessage(result);
        }

        public void PrintTranslatedMessage(IResultTemplate result)
        {
            if (result is null)
            {
                throw new System.ArgumentNullException(nameof(result));
            }

            _logger.LogTranslatedResultMessage(result);
        }
    }
}
