using Microsoft.ServiceFabric.Services.Remoting;
using System.ServiceModel;

namespace Common.Interfaces
{
    [ServiceContract]
    public interface ITransaction: IService
    {
        [OperationContract]
        Task<bool> Prepare();
        [OperationContract]
        Task Commit();
        [OperationContract]
        Task Rollback();
    }
}
