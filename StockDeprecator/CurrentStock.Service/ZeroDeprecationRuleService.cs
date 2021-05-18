using System;
using System.Collections.Generic;
using System.Text;
using CurrentStock.Models;

namespace CurrentStock.Service
{
    public class ZeroDeprecationRuleService : IDeprecationRuleService<ZeroDeprecationRule>
    {
        public bool RunRule(ZeroDeprecationRule rule, StockItem stockItem)
        {
            if(rule.FirstApplicableDay < stockItem.SellIn)
            {
                return false;
            }

            stockItem.Quality = 0;

            return true;
        }
    }
}
