using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace CurrentStock.DataAccess.File
{
    public static class CurrentStockDataAccessFileConfiguration
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<ICurrentStockRepository, FileStockRepository>();
            services.AddTransient<IStockTypeRepository, FileStockTypeRepository>();
        }
    }
}
