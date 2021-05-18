using System;
using System.Collections.Generic;
using System.Text;

namespace CurrentStock.Models
{
    public class IncreasingDeprecationRule : IDeprecationRule
    {
        public int FirstApplicableDay { get; set; }

        /// <summary>
        /// The amount to increase the quality by.
        /// </summary>
        public int Amount { get; set; }
    }
}
