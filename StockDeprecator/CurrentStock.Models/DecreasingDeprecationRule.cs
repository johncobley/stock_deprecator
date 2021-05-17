using System;
using System.Collections.Generic;
using System.Text;
using CurrentStock.Models;

namespace StockType.Models
{
    public class DecreasingDeprecationRule : IDeprecationRule
    {
        public int LastApplicableDay { get; set; } = 0;

        /// <summary>
        /// The amount to increment/decrement the quality by.
        /// </summary>
        public int Amount { get; set; }
    }
}
