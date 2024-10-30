using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SmartTech.Marketing.Application.Contract;
using SmartTech.Marketing.Core.Exceptions;

namespace SmartTech.Marketing.Application.Business.Order.Command
{
    public class UpdateOrderHandler : IRequestHandler<UpdateOrderHandlerInput, UpdateOrderHandlerOutput>
    {
        private readonly IDataBaseService _databaseService;
        private readonly ILogger<UpdateOrderHandler> _logger;
        public UpdateOrderHandler(ILogger<UpdateOrderHandler> logger, IDataBaseService databaseService)
        {
            _logger = logger;
            _databaseService = databaseService;
        }
        public async Task<UpdateOrderHandlerOutput> Handle(UpdateOrderHandlerInput request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling UpdateOrder business logic");
            UpdateOrderHandlerOutput output = new UpdateOrderHandlerOutput(request.CorrelationId());
            var order =await _databaseService.SmsOrder.FirstOrDefaultAsync(o => o.Id == request.OrderId);
            if (order != null)
            {
                if(request.CompeletedPercentage!=null)
                    order.CompeletedPercentage = (double)request.CompeletedPercentage;
                if (request.OrderStatusId != null)
                    order.OrderStatusId = (int)request.OrderStatusId;
                 _databaseService.SmsOrder.Update(order);
                await _databaseService.DBSaveChangesAsync();
                output.Message = "Order Status Updated successfully";
            }
            else
            {
                throw new WebApiException(WebApiExceptionSource.FromTranslation,"Please Enter Valid Order Id");
            }
            return output;
        }
    }
}
