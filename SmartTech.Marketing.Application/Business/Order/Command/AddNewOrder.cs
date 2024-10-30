using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.VisualBasic;
using SmartTech.Marketing.Application.Contract;
using SmartTech.Marketing.Domain.Entities;
using System.Diagnostics.Contracts;

namespace SmartTech.Marketing.Application.Business.Order.Command
{
    public class AddNewOrderHandler : IRequestHandler<AddNewOrderHandlerInput, AddNewOrderHandlerOutput>
    {
        private readonly IDataBaseService _databaseService;
        private readonly ILogger<AddNewOrderHandler> _logger;
        public AddNewOrderHandler(ILogger<AddNewOrderHandler> logger, IDataBaseService databaseService)
        {
            _logger = logger;
            _databaseService = databaseService;
        }
        public async Task<AddNewOrderHandlerOutput> Handle(AddNewOrderHandlerInput request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling AddNewOrder business logic");
            AddNewOrderHandlerOutput output = new AddNewOrderHandlerOutput(request.CorrelationId());

            var Order = new SmsOrder
            {
                ClientId = request.ClientId,
                ContractId = request.ContractId,
                OrderDate= DateOnly.FromDateTime(DateTime.Now),
                ContractImageResolutionId = request.ContractImageResolutionId,
                ContractImageModeId = request.ContractImageModeId,
                ContractOrderPirorityId = request.ContractOrderPirorityId,
                ShootingAngle = request.ShootingAngle,
                PredictedConsumedCredit = request.PredictedConsumedCredit,
                ActualConsumedCredit = request.ActualConsumedCredit,
                Discount = request.Discount,
                Notes = request.Notes,
                CompeletedPercentage = request.CompeletedPercentage,
                TotalOrderAreaInKm = request.TotalOrderAreaInKm,
                OrderGeometry = request.OrderGeometry,
                DueDate = request.DueDate,
                OrderStatusId = request.OrderStatusId

            };

            await _databaseService.SmsOrder.AddAsync(Order, cancellationToken);
            await _databaseService.DBSaveChangesAsync(cancellationToken);
            output.Message = "Order Added successfully";
            return output;
        }
    }
}
