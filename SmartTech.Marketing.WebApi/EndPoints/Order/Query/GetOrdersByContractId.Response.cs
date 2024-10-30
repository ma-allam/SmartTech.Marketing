

using SmartTech.Marketing.Application.Business.Order.Query;
using SmartTech.Marketing.Core.Messages;

namespace SmartTech.Marketing.WebApi.EndPoints.Order.Query
{
    public class GetOrdersByContractIdEndPointResponse : BaseResponse
    {
        public GetOrdersByContractIdEndPointResponse() { }
        public GetOrdersByContractIdEndPointResponse(Guid correlationId) : base(correlationId) { }
        public List<OrderData> Orders { get; set; }

    }
}
