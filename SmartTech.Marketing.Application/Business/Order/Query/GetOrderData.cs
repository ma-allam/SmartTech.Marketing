using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SmartTech.Marketing.Application.Business.ContractManagement.Command;
using SmartTech.Marketing.Application.Contract;

namespace SmartTech.Marketing.Application.Business.Order.Query
{
    public class GetOrderDataHandler : IRequestHandler<GetOrderDataHandlerInput, GetOrderDataHandlerOutput>
    {
        private readonly IDataBaseService _databaseService;
        private readonly ILogger<GetOrderDataHandler> _logger;
        public GetOrderDataHandler(ILogger<GetOrderDataHandler> logger, IDataBaseService databaseService)
        {
            _logger = logger;
            _databaseService = databaseService;
        }
        public async Task<GetOrderDataHandlerOutput> Handle(GetOrderDataHandlerInput request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling GetOrderData business logic");
            GetOrderDataHandlerOutput output = new GetOrderDataHandlerOutput(request.CorrelationId());
            var contract=await _databaseService.Contracts.Where(o=>o.Id==request.ContractId)
                .Include(o=>o.ContractImageModes)
                .Include(o=>o.ContractImageResolution)
                .Include(o=>o.ContractOrderPriority).FirstOrDefaultAsync();
            output.ContractOrderPriorities=contract.ContractOrderPriority.Select(o=>new ContractOrderPriorityOutput
            {
                Id=o.Id,
                Name=o.Name,
                CreditFactor=o.CreditFactor,
                MaxAllowedDays=o.MaxAllowedDays
            }).ToList();
            output.ContractImageModes=contract.ContractImageModes.Select(o=>new ContractImageModeOutput
            {
                Id = o.Id,
                Name = o.Name,
                CreditFactor=o.CreditFactor
            }).ToList();
            output.ContractImageResolutions=contract.ContractImageResolution.Select(o=>new ContractImageResolutionOutput
            {
                Id=o.Id,
                ContractImageTypeId=o.ContractImageTypeId,
                CreditFactor=o.CreditFactor,
                MinOrderAreaSize=o.MinOrderAreaSize,
                ResolutionInCm=o.ResolutionInCm
            }).ToList();
            output.OrderStatus=await _databaseService.SmsOrderStatus.Select(o=>new OrderStatusData

            {
                Id=o.Id,
                Name=o.Name
            }).ToListAsync();
            return output;
        }
    }
}
