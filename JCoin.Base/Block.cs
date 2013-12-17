using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
        static JsonSerializer serializer = new JsonSerializer();

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
            return serializer.ToBytes(this);
        }

        public override string ToString()
        {
            using (var writer = new StringWriter())
            {
                serializer.Serialize(writer, this);
                return writer.ToString();
            }
        }
    }
}
