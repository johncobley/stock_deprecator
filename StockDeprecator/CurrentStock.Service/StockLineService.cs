using System.Linq;
using System.Threading.Tasks;
using CurrentStock.DataAccess;
using CurrentStock.Models;

namespace CurrentStock.Service
{
    public class StockLineService : IStockLineService
    {
        public StockItem ParseLine(string line)
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
                SellIn = sellIn,
                Quality = quality
            };
            
        }
    }
}
