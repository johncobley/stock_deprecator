using System;
using System.Collections.Generic;
using System.Text;

namespace CurrentStock.Models
{
    public class DecreasingDeprecationRule : IDeprecationRule
    {
        public int FirstApplicableDay { get; set; }

        /// <summary>
        /// The amount to increment/decrement the quality by.
        /// </summary>
        public int Amount { get; set; }
    }
}
