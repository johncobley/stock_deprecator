using System;
using System.Collections.Generic;
using System.Text;
using CurrentStock.Models;

namespace CurrentStock.Service
{
    public class DecreasingDeprecationRuleService : IDeprecationRuleService<DecreasingDeprecationRule>
    {
        public bool RunRule(DecreasingDeprecationRule rule, StockItem stockItem)
        {
            if (rule.FirstApplicableDay < stockItem.SellIn)
            {
                return false;
            }

            var amount = rule.Amount;

            if (stockItem.SellIn < 0)
            {
                amount *= 2;
            }

            stockItem.Quality -= amount;

            return true;
        }
    }
}
