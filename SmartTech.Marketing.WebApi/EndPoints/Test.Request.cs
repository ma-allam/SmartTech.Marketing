using SmartTech.Marketing.Core.Messages;

namespace SmartTech.Marketing.WebApi.EndPoints
{
    public class TestEndPointRequest : BaseRequest
    {
        public const string Route = "/api/user/v{version:apiVersion}/Test/";
    }
}
