using SmartTech.Marketing.Core.Messages;

namespace SmartTech.Marketing.Application.Business.Clients.Query
{
    public class GetClientByIdHandlerOutput : BaseResponse
    {
        public GetClientByIdHandlerOutput() { }
        public GetClientByIdHandlerOutput(Guid correlationId) : base(correlationId) { }
        public ClientData? Client { get; set; }

    }
}
