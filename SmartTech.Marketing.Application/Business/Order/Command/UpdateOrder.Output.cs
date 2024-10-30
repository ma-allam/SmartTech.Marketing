using SmartTech.Marketing.Core.Messages;

namespace SmartTech.Marketing.Application.Business.Order.Command
{
    public class UpdateOrderHandlerOutput : BaseResponse
    {
        public UpdateOrderHandlerOutput() { }
        public UpdateOrderHandlerOutput(Guid correlationId) : base(correlationId) { }
        public string Message  { get; set; }
    }
}
