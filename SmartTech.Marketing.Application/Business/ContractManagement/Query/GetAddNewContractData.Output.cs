using SmartTech.Marketing.Core.Messages;

namespace SmartTech.Marketing.Application.Business.ContractManagement.Query
{
    public class GetAddNewContractDataHandlerOutput : BaseResponse
    {
        public GetAddNewContractDataHandlerOutput() { }
        public GetAddNewContractDataHandlerOutput(Guid correlationId) : base(correlationId) { }
        public List<ContractData> Currency { get; set; }
        public List<ContractData> ImageType { get; set; }
        public List<ContractData> PaymentMethod { get; set; }


    }
    public class ContractData
    {
        public int Id { get; set; }
        public string Descr { get; set; }
    }
}
