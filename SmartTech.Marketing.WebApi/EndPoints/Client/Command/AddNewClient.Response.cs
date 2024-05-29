

using SmartTech.Marketing.Core.Messages;

namespace SmartTech.Marketing.WebApi.EndPoints.Client.Query.Command
{
    public class AddNewClientEndPointResponse : BaseResponse
    {
        public AddNewClientEndPointResponse() { }
        public AddNewClientEndPointResponse(Guid correlationId) : base(correlationId) { }

    }
}
