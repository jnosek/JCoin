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
    public class BlockPrettyWriter
    {
        static readonly JsonSerializer serializer = new JsonSerializer();

        TextWriter writer;

        static BlockPrettyWriter()
        {
            serializer.Converters.Add(new TransactionConverter());
            serializer.Converters.Add(new UlongConverter());
            serializer.Formatting = Formatting.Indented;
        }

        public BlockPrettyWriter(TextWriter writer)
        {
            this.writer = writer;
        }

        public void Write(Block b)
        {
            serializer.Serialize(writer, b);
            writer.Flush();
        }
    }
}
