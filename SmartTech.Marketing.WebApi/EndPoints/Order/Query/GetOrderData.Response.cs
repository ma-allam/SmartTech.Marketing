

using SmartTech.Marketing.Application.Business.Order.Query;
using SmartTech.Marketing.Core.Messages;
using SmartTech.Marketing.Domain.Entities;

namespace SmartTech.Marketing.WebApi.EndPoints.Order.Query
{
    public class GetOrderDataEndPointResponse : BaseResponse
    {
        public GetOrderDataEndPointResponse() { }
        public GetOrderDataEndPointResponse(Guid correlationId) : base(correlationId) { }
        public List<ContractImageModeOutput> ContractImageModes { get; set; }
        public List<ContractImageTypeOutput> ImageTypes { get; set; }

        public List<ContractImageResolutionOutput> ContractImageResolutions { get; set; }
        public List<ContractOrderPriorityOutput> ContractOrderPriorities { get; set; }
        public List<ContractServiceOutput> ContractServices { get; set; }

        public List<OrderStatusData> OrderStatus { get; set; }
    }

}
