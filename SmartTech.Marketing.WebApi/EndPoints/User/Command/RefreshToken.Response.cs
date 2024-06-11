

using SmartTech.Marketing.Core.Auth.JWT;
using SmartTech.Marketing.Core.Messages;

namespace SmartTech.Marketing.WebApi.EndPoints.User.Command
{
    public class RefreshTokenEndPointResponse : BaseResponse
    {
        public RefreshTokenEndPointResponse() { }
        public RefreshTokenEndPointResponse(Guid correlationId) : base(correlationId) { }
        public TokenContext Context { get; set; }

    }
}
