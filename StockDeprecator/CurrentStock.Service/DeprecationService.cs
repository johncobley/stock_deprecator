using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CurrentStock.Models;

namespace CurrentStock.Service
{
    public class DeprecationService : IDeprecationService
    {
        /// <summary>
        /// The service for running a <see cref="DecreasingDeprecationRule"/>
        /// </summary>
        public IDeprecationRuleService<DecreasingDeprecationRule> _decreasingRuleService;

        /// <summary>
        /// The service for running a <see cref="IncreasingDeprecationRule"/>
        /// </summary>
        public IDeprecationRuleService<IncreasingDeprecationRule> _increasingRuleService;

        /// <summary>
        /// The service for running a <see cref="ZeroDeprecationRule"/>
        /// </summary>
        public IDeprecationRuleService<ZeroDeprecationRule> _zeroRuleService;

        public DeprecationService(
            IDeprecationRuleService<DecreasingDeprecationRule> decreasingRuleService,
            IDeprecationRuleService<IncreasingDeprecationRule> increasingRuleService,
            IDeprecationRuleService<ZeroDeprecationRule> zeroRuleService)
        {
            this._decreasingRuleService = decreasingRuleService;
            this._increasingRuleService = increasingRuleService;
            this._zeroRuleService = zeroRuleService;
        }

        public string DeprecateItem(StockItem stockItem)
        {
            if (stockItem.StockType == null)
            {
                return "NO SUCH ITEM";
            }

            if (stockItem.StockType.DeprecationRules?.Any() ?? false)
            {
                var rule = stockItem.StockType.DeprecationRules
                    .OrderBy(rule => rule.FirstApplicableDay)
                    .FirstOrDefault(rule => rule.FirstApplicableDay >= stockItem.SellIn);

                switch (rule)
                {
                    case DecreasingDeprecationRule decreasingRule:
                        this._decreasingRuleService.RunRule(decreasingRule, stockItem);
                        break;
                    case IncreasingDeprecationRule increasingRule:
                        this._increasingRuleService.RunRule(increasingRule, stockItem);
                        break;
                    case ZeroDeprecationRule zeroRule:
                        this._zeroRuleService.RunRule(zeroRule, stockItem);
                        break;
                }

                stockItem.SellIn--;
            }

            if(stockItem.Quality > 50)
            {
                stockItem.Quality = 50;
            }

            if(stockItem.Quality < 0)
            {
                stockItem.Quality = 0;
            }

            return $"{stockItem.StockTypeId} {stockItem.SellIn} {stockItem.Quality}";
        }
    }
}
