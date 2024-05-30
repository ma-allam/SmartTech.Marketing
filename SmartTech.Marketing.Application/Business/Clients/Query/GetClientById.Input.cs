using MediatR;
using SmartTech.Marketing.Core.Messages;

namespace SmartTech.Marketing.Application.Business.Clients.Query
{
    public class GetClientByIdHandlerInput : BaseRequest, IRequest<GetClientByIdHandlerOutput>
    {
        public GetClientByIdHandlerInput() { }
        public GetClientByIdHandlerInput(Guid correlationId) : base(correlationId) { }

        public int ClientId { get;  set; }
    }
}
