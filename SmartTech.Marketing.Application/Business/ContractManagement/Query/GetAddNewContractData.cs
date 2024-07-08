using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SmartTech.Marketing.Application.Contract;

namespace SmartTech.Marketing.Application.Business.ContractManagement.Query
{
    public class GetAddNewContractDataHandler : IRequestHandler<GetAddNewContractDataHandlerInput, GetAddNewContractDataHandlerOutput>
    {
        private readonly IDataBaseService _databaseService;
        private readonly ILogger<GetAddNewContractDataHandler> _logger;
        public GetAddNewContractDataHandler(ILogger<GetAddNewContractDataHandler> logger, IDataBaseService databaseService)
        {
            _logger = logger;
            _databaseService = databaseService;
        }
        public async Task<GetAddNewContractDataHandlerOutput> Handle(GetAddNewContractDataHandlerInput request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling GetAddNewContractData business logic");
            GetAddNewContractDataHandlerOutput output = new GetAddNewContractDataHandlerOutput(request.CorrelationId());
            output.PaymentMethod= await _databaseService.ContractPaymentType.Select(o=>new ContractData { Id=o.Id,Descr=o.Type}).ToListAsync();
            output.Currency = await _databaseService.Currency.Select(o => new ContractData { Id = o.Id, Descr = o.CurrencyName }).ToListAsync();
            output.ImageType = await _databaseService.ContractImageType.Select(o => new ContractData { Id = o.Id, Descr = o.Name }).ToListAsync();

            return output;
        }
    }
}
