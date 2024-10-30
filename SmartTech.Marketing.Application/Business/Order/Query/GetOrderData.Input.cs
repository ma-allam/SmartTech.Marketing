using MediatR;
using SmartTech.Marketing.Core.Messages;

namespace SmartTech.Marketing.Application.Business.Order.Query
{
    public class GetOrderDataHandlerInput : BaseRequest, IRequest<GetOrderDataHandlerOutput>
    {
        public GetOrderDataHandlerInput() { }
        public GetOrderDataHandlerInput(Guid correlationId) : base(correlationId) { }
        public decimal ContractId { get; set; }
    }
}
