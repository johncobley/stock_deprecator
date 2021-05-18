using System;
using System.Linq;
using System.Threading.Tasks;
using CurrentStock.DataAccess;
using CurrentStock.Models;

namespace CurrentStock.Service
{
    public class StockLineService : IStockLineService
    {
        /// <summary>
        /// A repository used to retrieve the type of the stock when it is parsed.
        /// </summary>
        private readonly IStockTypeRepository _stockTypeRepository;

        /// <summary>
        /// Initialises a new instance.
        /// </summary>
        /// <param name="stockTypeRepository"></param>
        public StockLineService(IStockTypeRepository stockTypeRepository)
        {
            if(stockTypeRepository == null)
            {
                throw new ArgumentException("Parameter should not be null.", nameof(stockTypeRepository));
            }
            this._stockTypeRepository = stockTypeRepository;
        }

        public async Task<StockItem> ParseLine(string line)
        {
            var splitLine = line.Trim().Split(" ").ToList();
            int quality;
            int sellIn;

            if (int.TryParse(splitLine[splitLine.Count() - 1], out quality) && 
                int.TryParse(splitLine[splitLine.Count() - 2], out sellIn))
            {
                splitLine.RemoveAt(splitLine.Count() - 1);
                splitLine.RemoveAt(splitLine.Count() - 1);
            }
            else
            {
                quality = 0;
                sellIn = 0;
            }

            var typeName = string.Join(" ", splitLine);

            return new StockItem
            {
                StockTypeId = typeName,
                StockType = await this._stockTypeRepository.Get(typeName),
                SellIn = sellIn,
                Quality = quality
            };
            
        }
    }
}
