using SmartTech.Marketing.Core.Messages;

namespace SmartTech.Marketing.WebApi.EndPoints.Client.Query
{
    public class GetAddClientDataEndPointRequest : BaseRequest
    {
        public const string Route = "/api/client/v{version:apiVersion}/GetAddClientData/";
    }
}
