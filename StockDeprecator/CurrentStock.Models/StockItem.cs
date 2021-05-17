using System;

namespace CurrentStock.Models
{
    public class StockItem : IEquatable<StockItem>
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

        public override bool Equals(object obj) => Equals(obj as StockItem);
        public bool Equals(StockItem other) => other != null && this.StockTypeId == other.StockTypeId && this.SellIn == other.SellIn && this.Quality == other.Quality;
    }
}
