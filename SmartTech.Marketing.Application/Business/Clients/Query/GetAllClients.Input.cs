using MediatR;
using SmartTech.Marketing.Core.Messages;

namespace SmartTech.Marketing.Application.Business.Clients.Query
{
    public class GetAllClientsHandlerInput : BaseRequest, IRequest<GetAllClientsHandlerOutput>
    {
        public GetAllClientsHandlerInput() { }
        public GetAllClientsHandlerInput(Guid correlationId) : base(correlationId) { }
    }
}
