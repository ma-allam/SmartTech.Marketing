using SmartTech.Marketing.Application.Business.ContractManagement.Command;
using SmartTech.Marketing.Core.Messages;
using SmartTech.Marketing.Domain.Entities;

namespace SmartTech.Marketing.Application.Business.Order.Query
{
    public class GetOrderDataHandlerOutput : BaseResponse
    {
        public GetOrderDataHandlerOutput() { }
        public GetOrderDataHandlerOutput(Guid correlationId) : base(correlationId) { }
        public List<ContractImageModeOutput> ContractImageModes { get; set; }
        public List<ContractImageResolutionOutput> ContractImageResolutions { get; set; }
        public List<ContractOrderPriorityOutput> ContractOrderPriorities { get; set; }
        public List<OrderStatusData> OrderStatus { get; set; }
    }
    public class ContractImageModeOutput
    {
        public decimal Id { get; set; }
        public string Name { get; set; }
        public double CreditFactor { get; set; }
    }

    public class ContractImageResolutionOutput
    {
        public decimal Id { get; set; }

        public int ResolutionInCm { get; set; }
        public double MinOrderAreaSize { get; set; }
        public double CreditFactor { get; set; }
        public int ContractImageTypeId { get; set; }
    }

    public class ContractOrderPriorityOutput
    {
        public decimal Id { get; set; }

        public string Name { get; set; }
        public int MaxAllowedDays { get; set; }
        public double CreditFactor { get; set; }
    }
    public class OrderStatusData
    {
        public decimal Id { get; set; }
        public string Name { get; set; }
    }
}
