using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Application.Localization;
using VendingMachine.Domain.Core;
using VendingMachine.Shared.Configurations;
using XLocalizer;

namespace VendingMachine.ConsoleApp.Commands.Handlers
{
    public class LanguageCommandHandler : ICommandHandler
    {
        private readonly ILocalizationService _localizationService;

        public LanguageCommandHandler(ILocalizationService localizationService)
        {
            _localizationService = localizationService;
        }
        public string CommandKey => "LANGUAGE";

        public string CommandDescription => @"LANGUAGE <EN|DE|FR>
Change application language";

        public IResultTemplate Handle(string[] args)
        {
            var getCultureResult = GetCultureFromParams(args);
            if (!getCultureResult.Succeeded) return getCultureResult;
            _localizationService.SetCurrentCulture(getCultureResult.Data);
            return ResultTemplate.SucceededResult();
        }

        private IResultTemplate<string> GetCultureFromParams(string[] args)
        {
            if ((args?.Length ?? 0) != 1) return ResultTemplate<string>.FailedResult(CommandDescription, CommandKey);
            return ResultTemplate<string>.SucceededResult().WithData(args[0]);
        }
    }
}
