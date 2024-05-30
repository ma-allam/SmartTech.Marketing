using MediatR;
using SmartTech.Marketing.Core.Messages;
using System.ComponentModel.DataAnnotations;

namespace SmartTech.Marketing.Application.Business.User.Command
{
    public class AssignRoleHandlerInput : BaseRequest, IRequest<AssignRoleHandlerOutput>
    {
        public AssignRoleHandlerInput() { }
        public AssignRoleHandlerInput(Guid correlationId) : base(correlationId) { }
        [Required]
        public string Username { get; set; }

        [Required]
        public string RoleName { get; set; }
    }
}
