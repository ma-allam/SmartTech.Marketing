using MediatR;
using SmartTech.Marketing.Core.Messages;

namespace SmartTech.Marketing.Application.Business.ContractManagement.Query
{
    public class GetAddNewContractDataHandlerInput : BaseRequest, IRequest<GetAddNewContractDataHandlerOutput>
    {
        public GetAddNewContractDataHandlerInput() { }
        public GetAddNewContractDataHandlerInput(Guid correlationId) : base(correlationId) { }
    }
}
