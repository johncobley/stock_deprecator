using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CurrentStock.Models;

namespace CurrentStock.DataAccess
{
    public interface ICurrentStockRepository
    {
        /// <summary>
        /// Searches through stock for items matching the specified filter.
        /// </summary>
        /// <param name="searchFilter">Used to filter the stock list.</param>
        /// <returns></returns>
        Task<IEnumerable<StockItem>> Search(StockSearchFilter searchFilter);

        /// <summary>
        /// Saves the stock data to the repository.
        /// </summary>
        /// <param name="stockData"></param>
        /// <returns></returns>
        Task Save(string stockData);
    }
}
