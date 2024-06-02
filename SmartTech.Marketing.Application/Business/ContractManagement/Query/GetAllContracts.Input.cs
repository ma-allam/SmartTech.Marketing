using MediatR;
using SmartTech.Marketing.Core.Messages;

namespace SmartTech.Marketing.Application.Business.ContractManagement.Query
{
    public class GetAllContractsHandlerInput : BaseRequest, IRequest<GetAllContractsHandlerOutput>
    {
        public GetAllContractsHandlerInput() { }
        public GetAllContractsHandlerInput(Guid correlationId) : base(correlationId) { }

    }
}
