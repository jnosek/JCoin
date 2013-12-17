using JCoin.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace JCoin.Client
{
    [ServiceContract(CallbackContract = typeof(INetwork))]
    interface INetwork
    {
        [OperationContract(IsOneWay = true)]
        void AddTransaction(Transaction t);
    }
}
