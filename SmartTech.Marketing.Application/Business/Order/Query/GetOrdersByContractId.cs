using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SmartTech.Marketing.Application.Contract;

namespace SmartTech.Marketing.Application.Business.Order.Query
{
    public class GetOrdersByContractIdHandler : IRequestHandler<GetOrdersByContractIdHandlerInput, GetOrdersByContractIdHandlerOutput>
    {
        private readonly IDataBaseService _databaseService;
        private readonly ILogger<GetOrdersByContractIdHandler> _logger;
        public GetOrdersByContractIdHandler(ILogger<GetOrdersByContractIdHandler> logger, IDataBaseService databaseService)
        {
            _logger = logger;
            _databaseService = databaseService;
        }
        public async Task<GetOrdersByContractIdHandlerOutput> Handle(GetOrdersByContractIdHandlerInput request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling GetOrdersByContractId business logic");
            GetOrdersByContractIdHandlerOutput output = new GetOrdersByContractIdHandlerOutput(request.CorrelationId());
            output.Orders=await _databaseService.SmsOrder.Where(o=>o.ContractId==request.ContractId).Select(o=>new OrderData
            {
                Id=o.Id,
                ClientId = o.ClientId,
                ContractId = o.ContractId,
                OrderDate = o.OrderDate,
                ContractImageResolutionId = o.ContractImageResolutionId,
                ContractImageModeId = o.ContractImageModeId,
                ContractOrderPirorityId = o.ContractOrderPirorityId,
                ShootingAngle = o.ShootingAngle,
                PredictedConsumedCredit = o.PredictedConsumedCredit,
                ActualConsumedCredit = o.ActualConsumedCredit,
                Discount = o.Discount,
                Notes = o.Notes,
                CompeletedPercentage = o.CompeletedPercentage,
                TotalOrderAreaInKm = o.TotalOrderAreaInKm,
                OrderGeometry = o.OrderGeometry,
                DueDate = o.DueDate,
                OrderStatusId = o.OrderStatusId
            }).ToListAsync();
            return output;
        }
    }
}
