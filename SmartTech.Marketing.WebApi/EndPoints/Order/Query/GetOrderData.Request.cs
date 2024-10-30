using SmartTech.Marketing.Core.Messages;

namespace SmartTech.Marketing.WebApi.EndPoints.Order.Query
{
    public class GetOrderDataEndPointRequest : BaseRequest
    {
        public const string Route = "/api/Order/v{version:apiVersion}/GetOrderData/";

        public decimal ContractId { get; set; }

    }
}
