using SmartTech.Marketing.Core.Messages;

namespace SmartTech.Marketing.WebApi.EndPoints.Order.Query
{
    public class GetOrdersByContractIdEndPointRequest : BaseRequest
    {
        public const string Route = "/api/Order/v{version:apiVersion}/GetOrdersByContractId/";
        public decimal ContractId { get; set; }

    }
}
