using MediatR;
using SmartTech.Marketing.Core.Messages;

namespace SmartTech.Marketing.Application.Business.Clients.Query
{
    public class GetAddClientDataHandlerInput : BaseRequest, IRequest<GetAddClientDataHandlerOutput>
    {
        public GetAddClientDataHandlerInput() { }
        public GetAddClientDataHandlerInput(Guid correlationId) : base(correlationId) { }
    }
}
