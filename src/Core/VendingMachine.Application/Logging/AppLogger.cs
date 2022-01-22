using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Application.Localization;
using VendingMachine.Domain.Core;

namespace VendingMachine.Application.Logging
{
    public class AppLogger<T> : IAppLogger<T>
    {
        private readonly ILogger<T> _logger;
        private readonly ILocalizationService _localizationService;

        public AppLogger(ILogger<T> logger,
            ILocalizationService localizationService)
        {
            _logger = logger;
            _localizationService = localizationService;
        }

        public void LogTranslated(LogLevel logLevel, EventId eventId, Exception exception, string message, params object[] args)
        {
            message = _localizationService.Translate(message);
            _logger.Log(logLevel, eventId, exception, message, args);
        }

        public void LogTranslated(LogLevel logLevel, EventId eventId, string message, params object[] args)
        {
            message = _localizationService.Translate(message);
            _logger.Log(logLevel, eventId, message, args);

        }

        public void LogTranslated(LogLevel logLevel, Exception exception, string message, params object[] args)
        {
            message = _localizationService.Translate(message);
            _logger.Log(logLevel, exception, message, args);
        }

        public void LogTranslated(LogLevel logLevel, string message, params object[] args)
        {
            message = _localizationService.Translate(message);
            _logger.Log(logLevel, message, args);
        }

        public void LogTranslatedCritical(EventId eventId, Exception exception, string message, params object[] args)
        {
            message = _localizationService.Translate(message);
            _logger.LogCritical(eventId, exception, message, args);
        }

        public void LogTranslatedCritical(EventId eventId, string message, params object[] args)
        {
            message = _localizationService.Translate(message);
            _logger.LogCritical(eventId, message, args);
        }

        public void LogTranslatedCritical(Exception exception, string message, params object[] args)
        {
            message = _localizationService.Translate(message);
            _logger.LogCritical(exception, message, args);
        }

        public void LogTranslatedCritical(string message, params object[] args)
        {
            message = _localizationService.Translate(message);
            _logger.LogCritical(message, args);
        }

        public void LogTranslatedDebug(EventId eventId, Exception exception, string message, params object[] args)
        {
            message = _localizationService.Translate(message);
            _logger.LogDebug(eventId, exception, message, args);
        }

        public void LogTranslatedDebug(EventId eventId, string message, params object[] args)
        {
            message = _localizationService.Translate(message);
            _logger.LogDebug(eventId, message, args);
        }

        public void LogTranslatedDebug(Exception exception, string message, params object[] args)
        {
            message = _localizationService.Translate(message);
        }

        public void LogTranslatedDebug(string message, params object[] args)
        {
            message = _localizationService.Translate(message);
            _logger.LogDebug(message, args);
        }

        public void LogTranslatedError(string message, params object[] args)
        {
            message = _localizationService.Translate(message);
            _logger.LogError(message, args);
        }

        public void LogTranslatedError(Exception exception, string message, params object[] args)
        {
            message = _localizationService.Translate(message);
            _logger.LogError(exception, message, args);
        }

        public void LogTranslatedError(EventId eventId, string message, params object[] args)
        {
            message = _localizationService.Translate(message);
            _logger.LogError(eventId, message, args);
        }

        public void LogTranslatedError(EventId eventId, Exception exception, string message, params object[] args)
        {
            message = _localizationService.Translate(message);
            _logger.LogError(eventId, exception, message, args);
        }

        public void LogTranslatedInformation(EventId eventId, Exception exception, string message, params object[] args)
        {
            message = _localizationService.Translate(message);
        }

        public void LogTranslatedInformation(EventId eventId, string message, params object[] args)
        {
            message = _localizationService.Translate(message);
        }

        public void LogTranslatedInformation(Exception exception, string message, params object[] args)
        {
            message = _localizationService.Translate(message);
        }

        public void LogTranslatedInformation(string message, params object[] args)
        {
            message = _localizationService.Translate(message);
            _logger.LogInformation(message, args);
        }

        public void LogTranslatedTrace(EventId eventId, Exception exception, string message, params object[] args)
        {
            message = _localizationService.Translate(message);
        }

        public void LogTranslatedTrace(EventId eventId, string message, params object[] args)
        {
            message = _localizationService.Translate(message);
        }

        public void LogTranslatedTrace(Exception exception, string message, params object[] args)
        {
            message = _localizationService.Translate(message);
        }

        public void LogTranslatedTrace(string message, params object[] args)
        {
            message = _localizationService.Translate(message);
            _logger.LogTrace(message, args);
        }

        public void LogTranslatedWarning(EventId eventId, Exception exception, string message, params object[] args)
        {
            message = _localizationService.Translate(message);
        }

        public void LogTranslatedWarning(EventId eventId, string message, params object[] args)
        {
            message = _localizationService.Translate(message);
        }

        public void LogTranslatedWarning(Exception exception, string message, params object[] args)
        {
            message = _localizationService.Translate(message);
        }

        public void LogTranslatedWarning(string message, params object[] args)
        {
            message = _localizationService.Translate(message);
            _logger.LogWarning(message, args);
        }

        public void LogTranslatedResultMessage(IResultTemplate result)
        {
            if (result == null) return;
            var logLevel = result.Succeeded ? LogLevel.Information : LogLevel.Error;
            var strBuilder = new StringBuilder();
            if(result?.Message != null)
            {
                var translatedMsg = _localizationService.Translate(result.Message);
                strBuilder.AppendFormat(translatedMsg, result.MessageArgs);
            }
            var messageLines = result.GetMessageLines();
            if ((messageLines?.Count ?? 0) > 0)
            {
                if(result.Message != null) strBuilder.AppendLine();
                foreach(var msgLine in messageLines.Where(t => t?.Message != null))
                {
                    var msgLineStr = msgLine.Message;
                    if(!msgLine.SkipLocalize) msgLineStr = _localizationService.Translate(msgLine.Message);
                    strBuilder.AppendFormat(msgLineStr, msgLine.Args);
                    strBuilder.AppendLine();
                }
            }
            var translatedStr = strBuilder.ToString();
            if(!string.IsNullOrWhiteSpace(translatedStr)) _logger.Log(logLevel, translatedStr);
        }

        public void LogResultMessage(IResultTemplate result)
        {
            if (result == null) return;
            var logLevel = result.Succeeded ? LogLevel.Information : LogLevel.Error;
            var strBuilder = new StringBuilder();
            if (result?.Message != null)
            {
                strBuilder.AppendFormat(result.Message, result.MessageArgs);
            }
            var messageLines = result.GetMessageLines();
            if ((messageLines?.Count ?? 0) > 0)
            {
                if (result.Message != null) strBuilder.AppendLine();
                foreach (var msgLine in messageLines.Where(t => t?.Message != null))
                {
                    strBuilder.AppendFormat(msgLine.Message, msgLine.Args);
                    strBuilder.AppendLine();
                }
            }
            var translatedStr = strBuilder.ToString();
            _logger.Log(logLevel, translatedStr);
        }
    }
}
