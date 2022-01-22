using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XLocalizer.Translate;
using XLocalizer.Translate.MyMemoryTranslate;
using XLocalizer;
using XLocalizer.Xml;
using VendingMachine.Infrastructure.Localization;
using VendingMachine.Application.Localization;
using VendingMachine.Application.Services;
using VendingMachine.Infrastructure.Orders;
using VendingMachine.Infrastructure.Currencies;
using VendingMachine.Infrastructure.Products;

namespace VendingMachine.Infrastructure.DependencyInjection
{
    public static class InfrastructureServices
    {
        public static IServiceCollection AddInfrastructure(this IServiceCollection services)
        {
            // Register translation service
            services.AddHttpClient<ITranslator, MyMemoryTranslateService>();

            // Register XmlResourceProvider
            services.AddSingleton<IXResourceProvider, XmlResourceProvider>();
            services.Configure<XLocalizerOptions>(o =>
            {
                o.AutoAddKeys = true;
                o.AutoTranslate = true;
            });

            services.AddTransient<ILocalizationService, LocalizationService>();
            services.AddTransient<IOrderService, OrderService>();
            services.AddTransient<ICoinService, CoinService>();
            services.AddTransient<ICurrentCurreny, CurrentCurrencyService>();
            services.AddTransient<ICurrentOrder, CurrentOrderService>();
            services.AddTransient<IProductService, ProductService>();
            return services;
        }

    }
}
