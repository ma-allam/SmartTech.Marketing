

using SmartTech.Marketing.Application.Business.Clients.Query;
using SmartTech.Marketing.Core.Messages;

namespace SmartTech.Marketing.WebApi.EndPoints.Client.Query
{
    public class GetClientByIdEndPointResponse : BaseResponse
    {
        public GetClientByIdEndPointResponse() { }
        public GetClientByIdEndPointResponse(Guid correlationId) : base(correlationId) { }
        public ClientData? Client { get; set; }

    }
}
