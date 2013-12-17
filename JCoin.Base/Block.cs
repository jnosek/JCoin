using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCoin.Base
{
    public class Block
    {
        static readonly JsonSerializer serializer = new JsonSerializer();

        public ulong PreviousBlockId { get; set; }
        public DateTime Timestamp { get; set; }
        public uint Nonce { get; set; }

        public ICollection<Transaction> Transactions { get; set; }

        public Block()
        {
            Timestamp = DateTime.UtcNow;
        }

        [JsonIgnore]
        public ulong Id
        {
            get
            {
                return HashFunction.Compute(serializer, this);
            }
        }

        public byte[] GetBytes()
        {
            using (var stream = new MemoryStream())
            using (var writer = new StreamWriter(stream))
            {
                serializer.Serialize(writer, this);
                writer.Flush();

                return stream.GetBuffer();
            }
        }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented, new UlongConverter());
        }

        class UlongConverter : JsonConverter
        {
            public override bool CanConvert(Type objectType)
            {
                return objectType == typeof(ulong);
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                throw new NotImplementedException();
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                writer.WriteValue(string.Format("{0:X}", value));
            }
        }
    }
}
