using System.Collections.Generic;
using System.Threading.Tasks;
using CurrentStock.Models;

namespace CurrentStock.DataAccess
{
    public interface IStockTypeRepository
    {
        /// <summary>
        /// Get stock type by name.
        /// </summary>
        /// <param name="typeName"></param>
        /// <returns></returns>
        Task<StockItemType> Get(string typeName);
    }
}
