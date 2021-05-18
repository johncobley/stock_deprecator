using CurrentStock.Models;

namespace CurrentStock.Service
{
    public class IncreasingDeprecationRuleService : IDeprecationRuleService<IncreasingDeprecationRule>
    {
        public bool RunRule(IncreasingDeprecationRule rule, StockItem stockItem)
        {
            if(rule.FirstApplicableDay < stockItem.SellIn)
            {
                return false;
            }

            stockItem.Quality += rule.Amount;

            return true;
        }
    }
}
