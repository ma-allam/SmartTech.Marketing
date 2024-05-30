using SmartTech.Marketing.Core.Messages;

namespace SmartTech.Marketing.Application.Business.Clients.Command
{
    public class AddNewClientHandlerOutput : BaseResponse
    {
        public AddNewClientHandlerOutput() { }
        public AddNewClientHandlerOutput(Guid correlationId) : base(correlationId) { }

    }
}
