using SmartTech.Marketing.Core.Messages;

namespace SmartTech.Marketing.Application.Business.Clients.Query
{
    public class SearchClientsHandlerOutput : BaseResponse
    {
        public SearchClientsHandlerOutput() { }
        public SearchClientsHandlerOutput(Guid correlationId) : base(correlationId) { }
        public List<ClientData> Clients { get; set; }

    }
}
