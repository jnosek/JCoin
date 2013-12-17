using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCoin.Test
{
    static class ExtensionMethods
    {
        public static ulong NextULong(this Random r)
        {
            var bytes = new byte[8];
            r.NextBytes(bytes);

            return BitConverter.ToUInt64(bytes, 0);
        }
    }
}
