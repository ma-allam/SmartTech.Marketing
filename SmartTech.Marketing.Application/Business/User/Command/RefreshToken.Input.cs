using MediatR;
using SmartTech.Marketing.Core.Messages;

namespace SmartTech.Marketing.Application.Business.User.Command
{
    public class RefreshTokenHandlerInput : BaseRequest, IRequest<RefreshTokenHandlerOutput>
    {
        public RefreshTokenHandlerInput() { }
        public RefreshTokenHandlerInput(Guid correlationId) : base(correlationId) { }
        public string? Token { get; set; }

    }
}
