using Microsoft.ServiceFabric.Services.Remoting;
using System.ServiceModel;


namespace Common.Interfaces
{
    [ServiceContract]
    public interface IBank: IService
    {
        [OperationContract]
        void ListClients();
        [OperationContract]
        void EnlistMoneyTransfer(string userID, double amount);
    }
}
