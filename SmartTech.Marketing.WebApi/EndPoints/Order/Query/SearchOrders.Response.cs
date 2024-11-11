

using SmartTech.Marketing.Application.Business.Order.Query;
using SmartTech.Marketing.Core.Messages;

namespace SmartTech.Marketing.WebApi.EndPoints.Order.Query
{
    public class SearchOrdersEndPointResponse : BaseResponse
    {
        public SearchOrdersEndPointResponse() { }
        public SearchOrdersEndPointResponse(Guid correlationId) : base(correlationId) { }
        public List<OrderData> Orders { get; set; }

    }
}
