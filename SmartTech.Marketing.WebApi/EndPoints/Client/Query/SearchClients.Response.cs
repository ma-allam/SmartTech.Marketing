

using SmartTech.Marketing.Application.Business.Clients.Query;
using SmartTech.Marketing.Core.Messages;

namespace SmartTech.Marketing.WebApi.EndPoints.Client.Query
{
    public class SearchClientsEndPointResponse : BaseResponse
    {
        public SearchClientsEndPointResponse() { }
        public SearchClientsEndPointResponse(Guid correlationId) : base(correlationId) { }
        public List<ClientData> Clients { get; set; }

    }
}
