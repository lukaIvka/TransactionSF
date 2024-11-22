using Microsoft.ServiceFabric.Services.Remoting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace Common.Interfaces
{
    [ServiceContract]
    public interface IBookstore: IService, ITransaction
    {
        [OperationContract]
        Task ListAvailableItems();
        [OperationContract]
        Task EnlistPurchase(string bookId, uint count);
        [OperationContract]
        Task<double> GetItemPrice(string bookId);
    }
}
