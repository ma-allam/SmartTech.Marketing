using SmartTech.Marketing.Core.Messages;

namespace SmartTech.Marketing.WebApi.EndPoints.ContractManagement.Query
{
    public class GetAddNewContractDataEndPointRequest : BaseRequest
    {
        public const string Route = "/api/ContractManagement/v{version:apiVersion}/GetAddNewContractData/";

    }
}
