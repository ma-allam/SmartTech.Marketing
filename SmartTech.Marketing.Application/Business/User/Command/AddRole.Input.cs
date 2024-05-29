using MediatR;
using SmartTech.Marketing.Core.Messages;
using System.ComponentModel.DataAnnotations;

namespace SmartTech.Marketing.Application.Business.User.Command
{
    public class AddRoleHandlerInput : BaseRequest, IRequest<AddRoleHandlerOutput>
    {
        public AddRoleHandlerInput() { }
        public AddRoleHandlerInput(Guid correlationId) : base(correlationId) { }
        [Required]
        public string RoleName { get; set; }
    }
}
