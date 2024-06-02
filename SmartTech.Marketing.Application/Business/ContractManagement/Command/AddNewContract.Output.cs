using SmartTech.Marketing.Core.Messages;

namespace SmartTech.Marketing.Application.Business.ContractManagement.Command
{
    public class AddNewContractHandlerOutput : BaseResponse
    {
        public AddNewContractHandlerOutput() { }
        public AddNewContractHandlerOutput(Guid correlationId) : base(correlationId) { }

        public string Message { get;  set; }
    }
}
