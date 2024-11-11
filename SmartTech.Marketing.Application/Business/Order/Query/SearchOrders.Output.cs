using NetTopologySuite.Geometries;
using SmartTech.Marketing.Core.Messages;

namespace SmartTech.Marketing.Application.Business.Order.Query
{
    public class SearchOrdersHandlerOutput : BaseResponse
    {
        public SearchOrdersHandlerOutput() { }
        public SearchOrdersHandlerOutput(Guid correlationId) : base(correlationId) { }
        public List<OrderData> Orders { get; set; }

    }
    
}
