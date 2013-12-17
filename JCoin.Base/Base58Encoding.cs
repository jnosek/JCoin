using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace JCoin.Base
{
    public class Base58Encoding
    {
        static string baseAlphabet = "123456789ABCDEFGHJKLMNPQRSTUVWXYZabcdefghijkmnopqrstuvwxyz";

        public static string Encode(byte[] bytes)
        {
            var number = new BigInteger(bytes);
            var result = new char[33];

            // if negative big integer, add 0 at end to make positive
            if(number < 0)
            {
                var newBytes = new byte[bytes.Length + 1];
                Array.Copy(bytes, newBytes, bytes.Length);
                number = new BigInteger(newBytes);
            }

            int i = 0;
            
            while (number >= 0 && result.Length > i)
            {
                var remainder = BigInteger.Remainder(number, (BigInteger)baseAlphabet.Length);
                number = number / (BigInteger)baseAlphabet.Length;
               result[result.Length - 1 - i] = baseAlphabet[(int)remainder] ;
               i++;
            }

            return new string(result);
        }

        //public static long DecodeBase58(String base58StringToExpand)
        //{
        //    long lConverted = 0;
        //    long lTemporaryNumberConverter = 1;

        //    while (base58StringToExpand.Length > 0)
        //    {
        //        String sCurrentCharacter = base58StringToExpand.Substring(base58StringToExpand.Length - 1);
        //        lConverted = lConverted + (lTemporaryNumberConverter * sBase58Alphabet.IndexOf(sCurrentCharacter));
        //        lTemporaryNumberConverter = lTemporaryNumberConverter * sBase58Alphabet.Length;
        //        base58StringToExpand = base58StringToExpand.Substring(0, base58StringToExpand.Length - 1);
        //    }
        //}
    }
}
