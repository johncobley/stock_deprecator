using System.Threading.Tasks;
using CurrentStock.Models;

namespace CurrentStock.Service
{
    public interface IStockLineService
    {
        /// <summary>
        /// Parses a stock import line into a stock item.
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        StockItem ParseLine(string line);
    }
}