using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JCoin.Client
{

    // see http://www.paulrohde.com/building-a-really-simple-wcf-p2p-application/
    class Peer
    {
        public Guid Id { get; private set; }

        public INetwork Channel;
        public INetwork Host;

        public Peer(Guid id)
        {
            Id = id;
        }
    }
}
