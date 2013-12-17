using JCoin.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace JCoin.Test
{
    class Program
    {
        static Random addressGenerator = new Random();

        static void Main(string[] args)
        {
            var cryptoFunction = MD5.Create();

            var block = new Block();
            block.Transactions = new [] 
            { 
                CreateDummy(), 
                CreateDummy(), 
                CreateDummy()
            };

            byte[] hashBytes = new byte[0];
            uint nonce = 0;
            
            var startTime = DateTime.Now;

            Console.WriteLine("Start Time: {0}", startTime);

            do 
            {
                block.Nonce = nonce;
                var blockBytes = block.GetBytes();
                hashBytes = cryptoFunction.ComputeHash(blockBytes);
                
                var hash = BitConverter.ToUInt64(hashBytes, 0);

                Console.WriteLine("{0:X} - {1:X}", nonce, hash);

                // increment nonce for next attempt
                nonce++;

            // difficulty of 8 zeros
            }while((hashBytes[0] & 0xFF) != 0);

            var endTime = DateTime.Now;

            Console.WriteLine("End Time: {0}\nElapsed Time: {1}\nVerified Block:\n\n{2}", endTime, (endTime - startTime), block);

            Console.ReadKey();
        }

        public static string RandomHashedAddress()
        {
            using (var cryptoFunction = RIPEMD160.Create())
            using (var cngKey = CngKey.Create(CngAlgorithm.ECDsaP256))
            {
                var publicKey = cngKey.Export(CngKeyBlobFormat.EccPublicBlob);
                var bytes = cryptoFunction.ComputeHash(publicKey);

                return Base58Encoding.Encode(bytes);
            }
        }

        public static Transaction CreateDummy()
        {
            var transaction = new TransferTransaction();

            transaction.Inputs = new[] { new TransactionInput { Transaction = addressGenerator.NextULong(), Index = 0 } };
            transaction.Outputs = new[] { new TransactionOutput { Address = RandomHashedAddress(), Value = Math.Abs(addressGenerator.Next()) % 100 } };

            foreach(var input in transaction.Inputs)
            {
                using (var cngKey = CngKey.Create(CngAlgorithm.ECDsaP256, null, new CngKeyCreationParameters { ExportPolicy = CngExportPolicies.AllowPlaintextExport }))
                {
                    var publicKey = cngKey.Export(CngKeyBlobFormat.EccPublicBlob);
                    var privateKey = cngKey.Export(CngKeyBlobFormat.EccPrivateBlob);
                    var priorOutput = new TransactionOutput { Address = RandomHashedAddress(), Value = Math.Abs(addressGenerator.Next()) % 100 };

                    input.PublicKey = publicKey;
                    input.Sign(priorOutput, privateKey);
                }
            }

            return transaction;
        }
    }
}
