﻿using SmartTech.Marketing.Core.Auth.JWT;
using SmartTech.Marketing.Core.Messages;

namespace SmartTech.Marketing.Application.Business.User.Command
{
    public class LoginHandlerOutput : BaseResponse
    {
        public LoginHandlerOutput() { }
        public LoginHandlerOutput(Guid correlationId) : base(correlationId) { }
        public TokenContext Context { get; set; }
    }

}
