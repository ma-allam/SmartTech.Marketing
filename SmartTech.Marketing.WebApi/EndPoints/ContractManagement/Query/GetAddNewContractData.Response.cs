

using SmartTech.Marketing.Application.Business.ContractManagement.Query;
using SmartTech.Marketing.Core.Messages;

namespace SmartTech.Marketing.WebApi.EndPoints.ContractManagement.Query
{
    public class GetAddNewContractDataEndPointResponse : BaseResponse
    {
        public GetAddNewContractDataEndPointResponse() { }
        public GetAddNewContractDataEndPointResponse(Guid correlationId) : base(correlationId) { }
        public List<ContractData> Currency { get; set; }
        public List<ContractData> ImageType { get; set; }
        public List<ContractData> PaymentMethod { get; set; }
    }
}
