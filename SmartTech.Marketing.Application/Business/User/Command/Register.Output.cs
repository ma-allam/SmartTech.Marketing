using SmartTech.Marketing.Core.Messages;

namespace SmartTech.Marketing.Application.Business.User.Command
{
    public class RegisterHandlerOutput : BaseResponse
    {
        public RegisterHandlerOutput() { }
        public RegisterHandlerOutput(Guid correlationId) : base(correlationId) { }
        public string Token { get; set; }

    }
}
