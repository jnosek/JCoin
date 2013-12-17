using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCoin.Base
{
    public interface IBlockChain
    {
        void Write(Block b);
        Block Read(ulong id);
        Block Head();
    }
}
