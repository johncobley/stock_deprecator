using System;
using System.Collections.Generic;
using System.Text;
using CurrentStock.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using StockType.Models;

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
                case var standardRuleType when standardRuleType ==  nameof(DecreasingDeprecationRule):
                    deprecationRule = new DecreasingDeprecationRule();
                    break;
                case var zeroRuleType when zeroRuleType == nameof(ZeroDeprecationRule):
                    deprecationRule = new ZeroDeprecationRule();
                    break;
            }

            if (deprecationRule != null)
            {
                serializer.Populate(reader, deprecationRule);
            }

            return deprecationRule;
        }
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) => throw new NotImplementedException();
    }
}
