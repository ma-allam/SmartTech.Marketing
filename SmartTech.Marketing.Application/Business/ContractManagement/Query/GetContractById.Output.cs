using SmartTech.Marketing.Core.Messages;

namespace SmartTech.Marketing.Application.Business.ContractManagement.Query
{
    public class GetContractByIdHandlerOutput : BaseResponse
    {
        public GetContractByIdHandlerOutput() { }
        public GetContractByIdHandlerOutput(Guid correlationId) : base(correlationId) { }
        public ContractDto Contract { get; set; }
        public string Message { get; set; }
    }
}
