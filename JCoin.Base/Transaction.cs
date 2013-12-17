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
    public abstract class Transaction
    {
        static readonly JsonSerializer serializer = new JsonSerializer();

        public IEnumerable<TransactionInput> Inputs { get; set; }
        public IEnumerable<TransactionOutput> Outputs { get; set; }

        public abstract TransactionType Type { get; }

        /// <summary>
        /// Creates an MD5 Hash of the transaction
        /// </summary>
        /// <returns></returns>
        [JsonIgnore]
        public ulong Id
        {
            get
            {
                return HashFunction.Compute(serializer, this);
            }
        }

        
    }

    public enum TransactionType : byte
    {
        Transfer = 1,
        Reward = 0
    }

    public class TransactionInput
    {
        static readonly JsonSerializer serializer = new JsonSerializer();

        //public ulong Block { get; set; }
        public ulong Transaction { get; set; }
        public uint Index { get; set; }
        public byte[] Signature { get; set; }
        public byte[] PublicKey { get; set; }

        public void Sign(TransactionOutput output, byte[] privateKey)
        {
            var signature = new InputSignature
            {
                Address = output.Address,
                Value = output.Value,
                Transaction = this.Transaction,
                Index = this.Index
            };
           
            using (var key = CngKey.Import(privateKey, CngKeyBlobFormat.EccPrivateBlob))
            using (var encryption = new ECDsaCng(key))
            {
                this.Signature = encryption.SignData(signature.Bytes);
            }
        }

        private class InputSignature
        {
            public string Address { get; set; }
            public decimal Value { get; set; }
            public ulong Transaction { get; set; }
            public uint Index { get; set; }

            [JsonIgnore]
            public byte[] Bytes
            {
                get
                {
                    return serializer.ToBytes(this);
                }
            }
        }
    }

    public class TransactionOutput
    {
        public string Address { get; set; }
        public decimal Value { get; set; }
    }
}
