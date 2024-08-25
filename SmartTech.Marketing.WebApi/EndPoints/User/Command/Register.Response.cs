using SmartTech.Marketing.Core.Auth.JWT;
using SmartTech.Marketing.Core.Messages;

namespace SmartTech.Marketing.WebApi.EndPoints.User.Command
{
    public class RegisterEndPointResponse : BaseResponse
    {
        public RegisterEndPointResponse() { }
        public RegisterEndPointResponse(Guid correlationId) : base(correlationId) { }
        public TokenContext Context { get; set; }

    }
}
