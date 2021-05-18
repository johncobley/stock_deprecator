using System;

namespace CurrentStock.Models
{
    public class StockItem
    {
        /// <summary>
        /// The identifier of the type of this stock item
        /// </summary>
        public string StockTypeId { get; set; }

        /// <summary>
        /// The amount of days the stock needs to be sold within.
        /// </summary>
        public int SellIn { get; set; }

        /// <summary>
        /// The quality or value of the item.
        /// </summary>
        public int Quality { get; set; }

        /// <summary>
        /// The associated <see cref="StockItemType"/>
        /// </summary>
        public StockItemType StockType { get; set; }
 }
}
