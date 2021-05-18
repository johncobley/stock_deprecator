using System;
using System.Collections.Generic;
using System.Text;
using CurrentStock.Models;
using Microsoft.Extensions.DependencyInjection;

namespace CurrentStock.Service
{
    public static class CurrentStockServiceConfiguration
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IStockLineService, StockLineService>();
            services.AddSingleton<IDeprecationService, DeprecationService>();
            services.AddSingleton<IDeprecationRuleService<DecreasingDeprecationRule>, DecreasingDeprecationRuleService>();
            services.AddSingleton<IDeprecationRuleService<IncreasingDeprecationRule>, IncreasingDeprecationRuleService>();
            services.AddSingleton<IDeprecationRuleService<ZeroDeprecationRule>, ZeroDeprecationRuleService>();
        }
    }
}
