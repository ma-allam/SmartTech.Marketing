using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SmartTech.Marketing.Application.Contract;
using SmartTech.Marketing.Domain.Entities;

namespace SmartTech.Marketing.Application.Business.ContractManagement.Command
{
    public class AddNewContractHandler : IRequestHandler<AddNewContractHandlerInput, AddNewContractHandlerOutput>
    {
        private readonly IDataBaseService _databaseService;
        private readonly ILogger<AddNewContractHandler> _logger;
        public AddNewContractHandler(ILogger<AddNewContractHandler> logger, IDataBaseService databaseService)
        {
            _logger = logger;
            _databaseService = databaseService;
        }
        public async Task<AddNewContractHandlerOutput> Handle(AddNewContractHandlerInput request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling AddNewContract business logic");
            AddNewContractHandlerOutput output = new AddNewContractHandlerOutput(request.CorrelationId());

            using (var transaction = await _databaseService.BeginTransactionAsync(cancellationToken))
            {




                Contracts contract =new Contracts() 
                {
                        ClientId=request.SelectedClientId,
                        ContractNumber=request.ContractNumber,
                        StartDate=request.StartDate,
                        EndDate=request.EndDate,
                        ContractPaymentTypeId=request.SelectedPaymentTypeId,
                        CurrencyId=request.SelectedCurrencyId,
                        Enabled=request.Enabled,
                        Notes=request.Notes,
                        TotalContractCost=request.TotalContractCost,
                        TotalCredit=request.TotalCredit
                };
                
                _databaseService.Contracts.Add(contract);
                await _databaseService.DBSaveChangesAsync(cancellationToken);





                    await transaction.CommitAsync(cancellationToken);

                   
            }


            return output;
        }
        public string Upload(List<IFormFile> formFiles)
        {

            return "";
        }
    }
}
