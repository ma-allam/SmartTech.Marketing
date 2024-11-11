using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SmartTech.Marketing.Application.Business.Clients.Query;
using SmartTech.Marketing.Application.Contract;

namespace SmartTech.Marketing.Application.Business.Order.Query
{
    public class SearchOrdersHandler : IRequestHandler<SearchOrdersHandlerInput, SearchOrdersHandlerOutput>
    {
        private readonly IDataBaseService _databaseService;
        private readonly ILogger<SearchOrdersHandler> _logger;
        public SearchOrdersHandler(ILogger<SearchOrdersHandler> logger, IDataBaseService databaseService)
        {
            _logger = logger;
            _databaseService = databaseService;
        }
        public async Task<SearchOrdersHandlerOutput> Handle(SearchOrdersHandlerInput request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling SearchOrders business logic");
            SearchOrdersHandlerOutput output = new SearchOrdersHandlerOutput(request.CorrelationId());
            var query = _databaseService.SmsOrder.AsQueryable();

            // Filter by OrderStatus if specified
            if (request.OrderStatus > 0)
            {
                query = query.Where(o => o.OrderStatusId == request.OrderStatus);
            }

            // Filter by FromDate if specified
            if (request.FromDate != default)
            {
                query = query.Where(o => o.OrderDate >= request.FromDate);
            }

            // Filter by ClientId if specified
            if (request.ClientId.HasValue)
            {
                query = query.Where(o => o.ClientId == request.ClientId.Value);
            }

            // Filter by ContractId if specified
            if (request.ContractId.HasValue)
            {
                query = query.Where(o => o.ContractId == request.ContractId.Value);
            }

            // Filter by ContractStatus (Enabled) if specified
            if (request.ContractEnabled)
            {
                query = query.Where(o => o.Contract.Enabled == true);
            }

            // Select the filtered records and project to the output format
            output.Orders = await query.Select(o => new OrderData
            {
                Id = o.Id,
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
            }).ToListAsync(cancellationToken);

            return output;
        }
    }
}
