using MediatR;
using SmartTech.Marketing.Core.Messages;
using System.ComponentModel.DataAnnotations;

namespace SmartTech.Marketing.Application.Business.User
{
    public class LoginHandlerInput : BaseRequest, IRequest<LoginHandlerOutput>
    {
        public LoginHandlerInput() { }
        public LoginHandlerInput(Guid correlationId) : base(correlationId) { }
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
