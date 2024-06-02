using SmartTech.Marketing.Core.Messages;

namespace SmartTech.Marketing.Application.Business.ContractManagement.Command
{
    public class UpdateContractHandlerOutput : BaseResponse
    {
        public UpdateContractHandlerOutput() { }
        public UpdateContractHandlerOutput(Guid correlationId) : base(correlationId) { }
        public string Message { get; set; }

    }
}
