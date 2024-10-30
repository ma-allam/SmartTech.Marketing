using MediatR;
using SmartTech.Marketing.Core.Messages;

namespace SmartTech.Marketing.Application.Business.Order.Command
{
    public class UpdateOrderHandlerInput : BaseRequest, IRequest<UpdateOrderHandlerOutput>
    {
        public UpdateOrderHandlerInput() { }
        public UpdateOrderHandlerInput(Guid correlationId) : base(correlationId) { }
        public decimal OrderId { get; set; }
        public double? CompeletedPercentage { get; set; }
        public int? OrderStatusId { get; set; }

    }
}
