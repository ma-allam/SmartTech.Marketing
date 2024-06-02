

using SmartTech.Marketing.Core.Messages;

namespace SmartTech.Marketing.WebApi.EndPoints.ContractManagement.Command
{
    public class AddNewContractEndPointResponse : BaseResponse
    {
        public AddNewContractEndPointResponse() { }
        public AddNewContractEndPointResponse(Guid correlationId) : base(correlationId) { }
        public string Message { get; set; }

    }
}
