using SmartTech.Marketing.Core.Messages;

namespace SmartTech.Marketing.Application.Business.User
{
    public class LoginHandlerOutput : BaseResponse
    {
        public LoginHandlerOutput() { }
        public LoginHandlerOutput(Guid correlationId) : base(correlationId) { }
        public string Token { get; set; }
    }
    
}
