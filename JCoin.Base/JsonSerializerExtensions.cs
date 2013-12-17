using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCoin.Base
{
    static class JsonSerializerExtensions
    {
        public static byte[] ToBytes(this JsonSerializer serializer, object o)
        {
            using (var stream = new MemoryStream())
            using (var writer = new StreamWriter(stream))
            {
                serializer.Serialize(writer, o);
                writer.Flush();

                stream.Seek(0, SeekOrigin.Begin);

                return stream.GetBuffer();
            }
        }
    }
}
