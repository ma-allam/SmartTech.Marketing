using MediatR;
using SmartTech.Marketing.Core.Messages;

namespace SmartTech.Marketing.Application.Business.Order.Query
{
    public class GetOrdersByContractIdHandlerInput : BaseRequest, IRequest<GetOrdersByContractIdHandlerOutput>
    {
        public GetOrdersByContractIdHandlerInput() { }
        public GetOrdersByContractIdHandlerInput(Guid correlationId) : base(correlationId) { }
        public decimal ContractId { get; set; }
    }
}
