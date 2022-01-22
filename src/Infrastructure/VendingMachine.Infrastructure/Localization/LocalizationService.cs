using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VendingMachine.Application.Localization;
using VendingMachine.Shared.Configurations;
using XLocalizer;
using XLocalizer.Translate;

namespace VendingMachine.Infrastructure.Localization
{
    public class LocalizationService : ILocalizationService
    {
        private readonly VendingMachineConfiguration _vendingMachineConfigurationOption;
        private readonly IConfiguration _configuration;
        private readonly ITranslator _translator;
        private readonly IXResourceProvider _xResourceProvider;

        public LocalizationService(IOptions<VendingMachineConfiguration> vendingMachineConfigurationOption,
            IConfiguration configuration,
            ITranslator translator,
            IXResourceProvider xResourceProvider)
        {
            _vendingMachineConfigurationOption = vendingMachineConfigurationOption.Value;
            _configuration = configuration;
            _translator = translator;
            _xResourceProvider = xResourceProvider;
        }
        public string Translate(string message)
        {
            var defaultCulture = _configuration.GetValue<string>("DefaultCulture");
            
            var cultureName = _vendingMachineConfigurationOption.CurrentCultureName ?? defaultCulture;
            if (cultureName.ToLower() == defaultCulture.ToLower() || string.IsNullOrWhiteSpace(message)) return message;
            var txtMsg = message;

            bool canTranslateFromResource;
            bool canTranslateFromProvider;

            canTranslateFromResource = _xResourceProvider.TryGetValue<LocalizationResources.LocSource>(message, out string translatedMessage);
            if (canTranslateFromResource)
            {
                txtMsg = translatedMessage;
            }
            else
            {
                canTranslateFromProvider = _translator.TryTranslate(defaultCulture, cultureName, message, out translatedMessage);
                if (canTranslateFromProvider)
                {
                    txtMsg = translatedMessage;
                    _xResourceProvider.TrySetValue<LocalizationResources.LocSource>(message, txtMsg, "");
                }
            }
            return txtMsg;
        }
        public void SetCurrentCulture(string cultureName)
        {
            _vendingMachineConfigurationOption.CurrentCultureName = cultureName;
            CultureInfo.CurrentCulture = new CultureInfo(_vendingMachineConfigurationOption.CurrentCultureName);
        }
    }
}
