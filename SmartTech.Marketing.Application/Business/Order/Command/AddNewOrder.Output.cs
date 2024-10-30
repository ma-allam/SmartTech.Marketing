using SmartTech.Marketing.Core.Messages;

namespace SmartTech.Marketing.Application.Business.Order.Command
{
    public class AddNewOrderHandlerOutput : BaseResponse
    {
        public AddNewOrderHandlerOutput() { }
        public AddNewOrderHandlerOutput(Guid correlationId) : base(correlationId) { }
        public string Message { get; set; }
    }
}
