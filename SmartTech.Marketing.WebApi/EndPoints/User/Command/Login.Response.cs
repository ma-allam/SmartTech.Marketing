using SmartTech.Marketing.Core.Messages;

namespace SmartTech.Marketing.WebApi.EndPoints.User.Command
{
    public class LoginEndPointResponse : BaseResponse
    {
        public LoginEndPointResponse() { }
        public LoginEndPointResponse(Guid correlationId) : base(correlationId) { }
        public string Token { get; set; }

    }
}
