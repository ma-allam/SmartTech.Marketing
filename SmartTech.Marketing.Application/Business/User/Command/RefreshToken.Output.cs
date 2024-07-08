using SmartTech.Marketing.Core.Auth.JWT;
using SmartTech.Marketing.Core.Messages;

namespace SmartTech.Marketing.Application.Business.User.Command
{
    public class RefreshTokenHandlerOutput : BaseResponse
    {
        public RefreshTokenHandlerOutput() { }
        public RefreshTokenHandlerOutput(Guid correlationId) : base(correlationId) { }
        public TokenContext Context { get; set; }
    }
}
