using System;
using System.Collections.Generic;
using System.Text;
using CurrentStock.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CurrentStock.DataAccess.File
{
    internal class StockItemTypeJsonConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType) => typeof(IDeprecationRule).IsAssignableFrom(objectType);

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jo = JObject.Load(reader);

            IDeprecationRule deprecationRule = null;

            switch(jo["RuleType"].ToString())
            {
                case var decreasingRuleType when decreasingRuleType ==  nameof(DecreasingDeprecationRule):
                    deprecationRule = new DecreasingDeprecationRule();
                    break;
                case var increasingRuleType when increasingRuleType == nameof(IncreasingDeprecationRule):
                    deprecationRule = new IncreasingDeprecationRule();
                    break;
                case var zeroRuleType when zeroRuleType == nameof(ZeroDeprecationRule):
                    deprecationRule = new ZeroDeprecationRule();
                    break;
            }

            if (deprecationRule != null)
            {
                serializer.Populate(jo.CreateReader(), deprecationRule);
            }

            // If the JSON doesn't contain a value for "FirstApplicableDay" assume it is essentially ongoing.
            if (jo["FirstApplicableDay"] == null)
            {
                deprecationRule.FirstApplicableDay = int.MaxValue;
            }

            return deprecationRule;
        }
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) => throw new NotImplementedException();
    }
}
