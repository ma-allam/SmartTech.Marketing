using SmartTech.Marketing.Core.Messages;

namespace SmartTech.Marketing.Application.Business.User.Command
{
    public class AssignRoleHandlerOutput : BaseResponse
    {
        public AssignRoleHandlerOutput() { }
        public AssignRoleHandlerOutput(Guid correlationId) : base(correlationId) { }
        public string Message { get; set; }

    }
}
