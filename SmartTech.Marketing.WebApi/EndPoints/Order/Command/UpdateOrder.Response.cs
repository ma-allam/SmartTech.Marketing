

using SmartTech.Marketing.Core.Messages;

namespace SmartTech.Marketing.WebApi.EndPoints.Order.Command
{
    public class UpdateOrderEndPointResponse : BaseResponse
    {
        public UpdateOrderEndPointResponse() { }
        public UpdateOrderEndPointResponse(Guid correlationId) : base(correlationId) { }
        public string Message { get; set; }

    }
}
