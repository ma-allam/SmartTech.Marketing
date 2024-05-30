﻿using SmartTech.Marketing.Core.Messages;

namespace SmartTech.Marketing.Application.Business.User.Command
{
    public class AddRoleHandlerOutput : BaseResponse
    {
        public AddRoleHandlerOutput() { }
        public AddRoleHandlerOutput(Guid correlationId) : base(correlationId) { }

        public string Message { get;  set; }
    }
}
