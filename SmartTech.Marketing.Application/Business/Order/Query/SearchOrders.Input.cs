using MediatR;
using SmartTech.Marketing.Core.Messages;

namespace SmartTech.Marketing.Application.Business.Order.Query
{
    public class SearchOrdersHandlerInput : BaseRequest, IRequest<SearchOrdersHandlerOutput>
    {
        public SearchOrdersHandlerInput() { }
        public SearchOrdersHandlerInput(Guid correlationId) : base(correlationId) { }
        public decimal? ClientId { get; set; }
        public decimal? ContractId { get; set; }

        public bool ContractEnabled { get; set; }
        public int OrderStatus { get; set; }
        public DateOnly FromDate { get; set; }

    }
}
