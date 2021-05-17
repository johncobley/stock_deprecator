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
            services.AddTransient<IStockLineService, StockLineService>();
            services.AddTransient<IDeprecationRuleService<ZeroDeprecationRule>, ZeroDeprecationRuleService>();
        }
    }
}
