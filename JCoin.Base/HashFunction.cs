using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace JCoin.Base
{
    static class HashFunction
    {
        public static ulong Compute(JsonSerializer serializer, object o)
        {
            using (var stream = new MemoryStream())
            using (var writer = new StreamWriter(stream))
            using (var hashFunction = MD5.Create())
            {
                serializer.Serialize(writer, o);
                writer.Flush();

                stream.Seek(0, SeekOrigin.Begin);

                var bytes = hashFunction.ComputeHash(stream);

                return BitConverter.ToUInt64(bytes, 0);
            }
        }
    }
}
