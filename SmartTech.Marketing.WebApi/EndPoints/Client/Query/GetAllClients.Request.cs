using SmartTech.Marketing.Core.Messages;

namespace SmartTech.Marketing.WebApi.EndPoints.Client.Query
{
    public class GetAllClientsEndPointRequest : BaseRequest
    {
        public const string Route = "/api/client/v{version:apiVersion}/GetAllClients/";

    }
}
