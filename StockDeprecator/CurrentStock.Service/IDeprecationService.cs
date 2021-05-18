using System;
using System.Collections.Generic;
using System.Text;
using CurrentStock.Models;

namespace CurrentStock.Service
{
    public interface IDeprecationService
    {
        /// <summary>
        /// Deprecates the quality of the stock item based on the rules connected to it's associated stock type
        /// </summary>
        /// <param name="stockItem"></param>
        /// <returns></returns>
        string DeprecateItem(StockItem stockItem);
    }
}
