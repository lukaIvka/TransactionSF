using Microsoft.ServiceFabric.Services.Remoting;
using System.ServiceModel;


namespace Common.Interfaces
{
    [ServiceContract]
    public interface IBank: IService, ITransaction
    {
        [OperationContract]
        Task ListClients();
        [OperationContract]
        Task EnlistMoneyTransfer(string userID, double amount);
    }
}
