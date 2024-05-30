

using SmartTech.Marketing.Core.Messages;

namespace SmartTech.Marketing.WebApi.EndPoints.User.Command
{
    public class AddRoleEndPointResponse : BaseResponse
    {
        public AddRoleEndPointResponse() { }
        public AddRoleEndPointResponse(Guid correlationId) : base(correlationId) { }
        public string Message { get; set; }

    }
}
