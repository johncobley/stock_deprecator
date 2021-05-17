using System;
using System.Collections.Generic;
using System.Text;
using CurrentStock.Models;

namespace CurrentStock.Service
{
    public interface IDeprecationRuleService<TRuleType> where TRuleType : IDeprecationRule
    {
        /// <summary>
        /// Runs the rule against the provided stock item.
        /// </summary>
        /// <param name="rule">The rule to run.</param>
        /// <param name="stockItem">The stock item to run the rule against.</param>
        /// <returns>True if the rule was run successfully.</returns>
        bool RunRule(TRuleType rule, StockItem stockItem);
    }
}
