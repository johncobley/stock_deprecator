using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CurrentStock.Models;
using CurrentStock.Service;
using Microsoft.Extensions.Caching.Memory;

namespace CurrentStock.DataAccess.File
{
    /// <summary>
    /// A repository that will read stock from a file; this wouldn't be used in a developed solution
    /// but I'm using it here for ease of use.
    /// </summary>
    public class FileStockRepository : ICurrentStockRepository
    {
        /// <summary>
        /// The stock service
        /// </summary>
        private readonly IStockLineService _stockService;

        /// <summary>
        /// The cache service;
        /// </summary>
        private readonly IMemoryCache _memoryCacheService;

        /// <summary>
        /// A key used to store/retrieve the stock data.
        /// </summary>
        private const string cacheKey = "stock_cache";

        /// <summary>
        /// Constructs a new <see cref="FileStockRepository"/> instance.
        /// </summary>
        /// <param name="stockService">A <see cref="IStockLineService"/> instance.</param>
        /// <param name="stockTypeRepository">A <see cref="IStockTypeRepository"/> instance.</param>
        /// <param name="memoryCacheService">A <see cref="IMemoryCache"/> instance</param>
        public FileStockRepository(IStockLineService stockService, IMemoryCache memoryCacheService)
        {
            this._stockService = stockService;
            this._memoryCacheService = memoryCacheService;
        }

        public async Task<IEnumerable<StockItem>> Search(StockSearchFilter searchFilter)
        {
            var type = await new FileStockTypeRepository(this._memoryCacheService).Get("Aged Brie");
            if (!this._memoryCacheService.TryGetValue<List<StockItem>>(FileStockRepository.cacheKey, out var stock))
            {
                stock = new List<StockItem>();
                var fileLocation = "../Data/Stock.txt";

                using (var reader = new StreamReader(fileLocation))
                {
                    await this.SaveStock(reader);
                }

                this._memoryCacheService.TryGetValue<List<StockItem>>(FileStockRepository.cacheKey, out stock);
            }

            if (searchFilter?.StockType != null)
            {
                stock = stock.Where(item => item.StockTypeId == searchFilter.StockType).ToList();
            }

            return stock;
        }

        public async Task Save(string stockData)
        {
            using(var reader = new StringReader(stockData))
            {
                await SaveStock(reader);
            }
        }

        private async Task SaveStock(TextReader reader)
        {
            var stock = new List<StockItem>();

            string line;
            while ((line = await reader.ReadLineAsync()) != null)
            {
                stock.Add(await this._stockService.ParseLine(line));
            }

            this.Save(stock);
        }

        public void Save(IEnumerable<StockItem> currentStockItems)
        {
            this._memoryCacheService.Set(FileStockRepository.cacheKey, currentStockItems);
        }
    }
}
