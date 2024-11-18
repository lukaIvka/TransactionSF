using Microsoft.ServiceFabric.Services.Remoting;
using System.ServiceModel;


namespace Common.Interfaces
{
    [ServiceContract]
    public interface IValidate : IService
    {
        [OperationContract]
        Task<string> Validate(string book, uint quantity);
    }
}
