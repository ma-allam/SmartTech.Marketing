using SmartTech.Marketing.Core.Messages;

namespace SmartTech.Marketing.WebApi.EndPoints.Client.Query
{
    public class SearchClientsEndPointRequest : BaseRequest
    {
        public const string Route = "/api/client/v{version:apiVersion}/SearchClients/";

        public string? Name { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public int? ClientType { get; set; }
        public int? CountryId { get; set; }
    }
}
