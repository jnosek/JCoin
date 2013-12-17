using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCoin.Base
{
    public class TransferTransaction : Transaction
    {
        public override TransactionType Type
        {
            get { return TransactionType.Transfer; }
        }
    }
}
