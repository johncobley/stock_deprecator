using System;

namespace CurrentStock.Models
{
    /// <summary>
    /// Used to filter the results from a stock search.
    /// </summary>
    public class StockSearchFilter
    {
        /// <summary>
        /// The Id of the stock item required.
        /// </summary>
        public Guid? StockId { get; set; }

        /// <summary>
        /// A name of the type of stock to find. 
        /// </summary>
        public string? StockType { get; set; }

        // In a full application there would be additional filters.
    }
}
