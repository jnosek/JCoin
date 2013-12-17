using JCoin.Base;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCoin.Client
{
    class BlockReader
    {
        static readonly JsonSerializer serializer = new JsonSerializer();

        TextReader reader;

        static BlockReader()
        {
            serializer.Converters.Add(new TransactionConverter());
            serializer.Converters.Add(new UlongConverter());
            serializer.Formatting = Formatting.Indented;
        }

        public BlockReader(TextReader reader)
        {
            this.reader = reader;
        }

        public Block Read()
        {
            using (var jsonReader = new JsonTextReader(reader))
            {
                return serializer.Deserialize<Block>(jsonReader);
            }
        }
    }
}
