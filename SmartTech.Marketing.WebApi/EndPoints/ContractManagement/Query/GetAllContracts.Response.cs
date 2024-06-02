

using SmartTech.Marketing.Application.Business.ContractManagement.Query;
using SmartTech.Marketing.Core.Messages;

namespace SmartTech.Marketing.WebApi.EndPoints.ContractManagement.Query
{
    public class GetAllContractsEndPointResponse : BaseResponse
    {
        public GetAllContractsEndPointResponse() { }
        public GetAllContractsEndPointResponse(Guid correlationId) : base(correlationId) { }
        public List<ContractDto> Contracts { get; set; }
        public string Message { get; set; }
    }
}
