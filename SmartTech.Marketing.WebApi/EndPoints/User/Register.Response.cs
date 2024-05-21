﻿

using SmartTech.Marketing.Core.Messages;

namespace SmartTech.Marketing.WebApi.EndPoints.User
{
    public class RegisterEndPointResponse : BaseResponse
    {
        public RegisterEndPointResponse() { }
        public RegisterEndPointResponse(Guid correlationId) : base(correlationId) { }
        public string Token { get; set; }

    }
}
