

using SmartTech.Marketing.Application.Business.Clients.Query;
using SmartTech.Marketing.Core.Messages;

namespace SmartTech.Marketing.WebApi.EndPoints.Client.Query
{
    public class GetAllClientsEndPointResponse : BaseResponse
    {
        public GetAllClientsEndPointResponse() { }
        public GetAllClientsEndPointResponse(Guid correlationId) : base(correlationId) { }
        public List<ClientData> Clients { get; set; }

    }
}
