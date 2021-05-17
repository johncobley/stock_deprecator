using System.Collections.Generic;

namespace CurrentStock.Models
{
    public class StockItemType
    {
        /// <summary>
        /// The name of the type.
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// The deprecation rules for the type.
        /// </summary>
        public IEnumerable<IDeprecationRule> DeprecationRules { get; set; }
    }
}
