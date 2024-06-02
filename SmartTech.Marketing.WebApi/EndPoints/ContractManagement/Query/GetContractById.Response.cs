

using SmartTech.Marketing.Application.Business.ContractManagement.Query;
using SmartTech.Marketing.Core.Messages;

namespace SmartTech.Marketing.WebApi.EndPoints.ContractManagement.Query
{
    public class GetContractByIdEndPointResponse : BaseResponse
    {
        public GetContractByIdEndPointResponse() { }
        public GetContractByIdEndPointResponse(Guid correlationId) : base(correlationId) { }
        public ContractDto Contract { get; set; }
        public string Message { get; set; }
    }
}
