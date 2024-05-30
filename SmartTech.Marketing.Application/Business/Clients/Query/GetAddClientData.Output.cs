using SmartTech.Marketing.Core.Messages;

namespace SmartTech.Marketing.Application.Business.Clients.Query
{
    public class GetAddClientDataHandlerOutput : BaseResponse
    {
        public GetAddClientDataHandlerOutput() { }
        public GetAddClientDataHandlerOutput(Guid correlationId) : base(correlationId) { }
        public List<CountryData> Countries { get; set; }
        public List<ClientTypeData> ClientTypes { get; set; }
        public string TempPass { get; set; }
    }
    public class CountryData
    {
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public string CountryPrefix { get; set; }
    }
    public class ClientTypeData
    {
        public int Id { get; set; }
        public string Type { get; set; }
    }
}
