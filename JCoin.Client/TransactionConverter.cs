using JCoin.Base;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCoin.Client
{
    public class TransactionConverter : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(Transaction);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var jObject = JObject.Load(reader);

            var type = (TransactionType)jObject["Type"].Value<byte>();

            switch (type)
            {
                case TransactionType.Reward:
                    return serializer.Deserialize<RewardTransaction>(jObject.CreateReader());
                case TransactionType.Transfer:
                    return serializer.Deserialize<TransferTransaction>(jObject.CreateReader());
                default:
                    throw new ArgumentException("Unknown TransactionType");
            }
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(value);
        }
    }
}
