using SmartTech.Marketing.Application.Business.Clients.Query;
using SmartTech.Marketing.Core.Messages;

namespace SmartTech.Marketing.WebApi.EndPoints.Client.Query
{
    public class GetAddClientDataEndPointResponse : BaseResponse
    {
        public GetAddClientDataEndPointResponse() { }
        public GetAddClientDataEndPointResponse(Guid correlationId) : base(correlationId) { }
        public List<CountryData> Countries { get; set; }
        public List<ClientTypeData> ClientTypes { get; set; }
        public string TempPass { get; set; }
    }
}
