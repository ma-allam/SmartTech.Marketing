using SmartTech.Marketing.Core.Messages;

namespace SmartTech.Marketing.Application.Business.Clients.Command
{
    public class UpdateClientHandlerOutput : BaseResponse
    {
        public UpdateClientHandlerOutput() { }
        public UpdateClientHandlerOutput(Guid correlationId) : base(correlationId) { }

        public string Message { get;  set; }
    }
}
