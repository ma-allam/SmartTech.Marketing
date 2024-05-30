using SmartTech.Marketing.Core.Messages;

namespace SmartTech.Marketing.WebApi.EndPoints.Client.Query
{
    public class GetClientByIdEndPointRequest : BaseRequest
    {
        public const string Route = "/api/client/v{version:apiVersion}/GetClientById/";

        public int ClientId { get; set; }

    }
}
