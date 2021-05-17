using CurrentStock.Models;

namespace CurrentStock.Service
{
    public class IncreasingDeprecationRuleService : IDeprecationRuleService<IncreasingDeprecationRule>
    {
        public bool RunRule(IncreasingDeprecationRule rule, StockItem stockItem)
        {
            if(rule.LastApplicableDay > stockItem.SellIn)
            {
                return false;
            }

            stockItem.SellIn--;
            stockItem.Quality += rule.Amount;

            return true;
        }
    }
}
