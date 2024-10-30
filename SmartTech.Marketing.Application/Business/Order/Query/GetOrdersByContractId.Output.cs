using NetTopologySuite.Geometries;
using SmartTech.Marketing.Core.Messages;

namespace SmartTech.Marketing.Application.Business.Order.Query
{
    public class GetOrdersByContractIdHandlerOutput : BaseResponse
    {
        public GetOrdersByContractIdHandlerOutput() { }
        public GetOrdersByContractIdHandlerOutput(Guid correlationId) : base(correlationId) { }
        public List<OrderData> Orders { get; set; }
    }
    public class OrderData
    {
        public decimal Id { get; set; }
        public int ClientId { get; set; }

        public int ContractId { get; set; }

        public DateOnly OrderDate { get; set; }

        public int ContractImageResolutionId { get; set; }

        public int ContractImageModeId { get; set; }

        public int ContractOrderPirorityId { get; set; }

        public double ShootingAngle { get; set; }

        public double PredictedConsumedCredit { get; set; }

        public double ActualConsumedCredit { get; set; }

        public double Discount { get; set; }

        public string? Notes { get; set; }

        public double CompeletedPercentage { get; set; }

        public double TotalOrderAreaInKm { get; set; }

        public Geometry? OrderGeometry { get; set; } = null!;

        public DateOnly DueDate { get; set; }

        public int OrderStatusId { get; set; }
    }
}
