using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using CurrentStock.Models;
using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CurrentStock.DataAccess.File
{
    public class FileStockTypeRepository : IStockTypeRepository
    {
        /// <summary>
        /// The cache service;
        /// </summary>
        private IMemoryCache _memoryCacheService;

        /// <summary>
        /// A key used to store/retrieve the stock type data.
        /// </summary>
        private const string cacheKey = "stock_type_cache";

        public FileStockTypeRepository(IMemoryCache memoryCacheService) => this._memoryCacheService = memoryCacheService;

        public async Task<StockItemType> Get(string typeName)
        {
            if (!this._memoryCacheService.TryGetValue<List<StockItemType>>(FileStockTypeRepository.cacheKey, out var stockTypes))
            {
                var fileLocation = "../Data/StockTypes.json";

                using (var reader = new StreamReader(fileLocation))
                {
                    stockTypes = JsonConvert.DeserializeObject<List<StockItemType>>(await reader.ReadToEndAsync(), new StockItemTypeJsonConverter());
                }

                this._memoryCacheService.Set(FileStockTypeRepository.cacheKey, stockTypes);
            }

            return stockTypes.FirstOrDefault(stockType => stockType.TypeName == typeName);
        }
    }
}
