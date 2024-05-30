using SmartTech.Marketing.Core.Messages;

namespace SmartTech.Marketing.WebApi.EndPoints.DataBaseSeed
{
    public class SeedDatabaseEndPointRequest : BaseRequest
    {
        public const string Route = "/api/user/v{version:apiVersion}/DataBaseSeed/";
    }
}
