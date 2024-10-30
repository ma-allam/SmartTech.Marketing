

using SmartTech.Marketing.Core.Messages;

namespace SmartTech.Marketing.WebApi.EndPoints.Order.Command
{
    public class AddNewOrderEndPointResponse : BaseResponse
    {
        public AddNewOrderEndPointResponse() { }
        public AddNewOrderEndPointResponse(Guid correlationId) : base(correlationId) { }
        public string Message { get; set; }

    }
}
