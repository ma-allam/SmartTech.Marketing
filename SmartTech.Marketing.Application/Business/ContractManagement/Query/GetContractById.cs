using MediatR;
using Microsoft.Extensions.Logging;
using SmartTech.Marketing.Application.Contract;
using SmartTech.Marketing.Core.Exceptions;
using SmartTech.Marketing.Domain.Entities;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SmartTech.Marketing.Application.Business.ContractManagement.Query
{
    public class GetContractByIdHandler : IRequestHandler<GetContractByIdHandlerInput, GetContractByIdHandlerOutput>
    {
        private readonly IDataBaseService _databaseService;
        private readonly ILogger<GetContractByIdHandler> _logger;

        public GetContractByIdHandler(ILogger<GetContractByIdHandler> logger, IDataBaseService databaseService)
        {
            _logger = logger;
            _databaseService = databaseService;
        }

        public async Task<GetContractByIdHandlerOutput> Handle(GetContractByIdHandlerInput request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling GetContractById business logic");
            GetContractByIdHandlerOutput output = new GetContractByIdHandlerOutput(request.CorrelationId());

            try
            {
                var contract = await _databaseService.Contracts
                    .Include(c => c.ContractDueDates)
                    .Include(c => c.ContractImageModes)
                    .Include(c => c.ContractImageResolution)
                    .Include(c => c.ContractOrderPriority)
                    .Include(c => c.ContractPaymentInformation)
                    .Include(c => c.ContractPeriods)
                    .Include(c => c.ContractServices)
                    .Include(c => c.ContractAttachments)
                    .FirstOrDefaultAsync(c => c.Id == request.ContractId, cancellationToken);

                if (contract == null)
                {
                    throw new WebApiException(WebApiExceptionSource.DynamicMessage, "Contract not found");
                }

                var contractDto = new ContractDto
                {
                    Id = contract.Id,
                    ContractNumber = contract.ContractNumber,
                    StartDate = contract.StartDate,
                    EndDate = contract.EndDate,
                    TotalContractCost = contract.TotalContractCost,
                    TotalCredit = contract.TotalCredit,
                    CurrencyId = contract.CurrencyId,
                    Notes = contract.Notes,
                    ClientId = contract.ClientId,
                    ContractPaymentTypeId = contract.ContractPaymentTypeId,
                    Enabled = contract.Enabled,
                    AcceptableCloudPerc = contract.AcceptableCloudPerc,
                    MinSquareArea = contract.MinSquareArea,
                    ContractDueDates = contract.ContractDueDates.Select(d => new ContractDueDateDto
                    {
                        Id = d.Id,
                        DueDate = d.DueDate,
                        Notes = d.Notes
                    }).ToList(),
                    ContractImageModes = contract.ContractImageModes.Select(im => new ContractImageModeDto
                    {
                        Id = im.Id,
                        Name = im.Name,
                        CreditFactor = im.CreditFactor
                    }).ToList(),
                    ContractImageResolutions = contract.ContractImageResolution.Select(ir => new ContractImageResolutionDto
                    {
                        Id = ir.Id,
                        ResolutionInCm = ir.ResolutionInCm,
                        MinOrderAreaSize = ir.MinOrderAreaSize,
                        CreditFactor = ir.CreditFactor,
                        ContractImageTypeId = ir.ContractImageTypeId
                    }).ToList(),
                    ContractOrderPriorities = contract.ContractOrderPriority.Select(op => new ContractOrderPriorityDto
                    {
                        Id = op.Id,
                        Name = op.Name,
                        MaxAllowedDays = op.MaxAllowedDays,
                        CreditFactor = op.CreditFactor
                    }).ToList(),
                    ContractPaymentInformation = contract.ContractPaymentInformation.Select(pi => new ContractPaymentInformationDto
                    {
                        Id = pi.Id,
                        BankName = pi.BankName,
                        BankBranch = pi.BankBranch,
                        BankAddress = pi.BankAddress,
                        Iban = pi.Iban,
                        ClientNameInBank = pi.ClientNameInBank,
                        Notes = pi.Notes
                    }).ToList(),
                    ContractPeriods = contract.ContractPeriods.Select(cp => new ContractPeriodDto
                    {
                        Id = cp.Id,
                        StartDate = cp.StartDate,
                        EndDate = cp.EndDate,
                        AvailableCredit = cp.AvailableCredit,
                        RemainingCredit = cp.RemainingCredit
                    }).ToList(),
                    ContractServices = contract.ContractServices.Select(cs => new ContractServiceDto
                    {
                        Id = cs.Id,
                        ServiceName = cs.ServiceName,
                        ServiceCost = cs.ServiceCost,
                        Notes = cs.Notes
                    }).ToList(),
                    ContractAttachments = contract.ContractAttachments.Select(ca => new ContractAttachmentDto
                    {
                        Id = ca.Id,
                        Tags = ca.Tags,
                        Name = ca.Name,
                        AttachmentId = ca.AttachmentId,
                        FileExtension = ca.FileExtension,
                        Notes = ca.Notes,
                        FileUrl = ca.FileUrl,
                        UploadDate = ca.UploadDate
                    }).ToList()
                };

                output.Contract = contractDto;
                output.Message = "Contract retrieved successfully.";
            }
            catch(WebApiException)
            {
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error occurred while retrieving the contract");
                throw new WebApiException(WebApiExceptionSource.DynamicMessage, "Error occurred while retrieving the contract", ex);
            }

            return output;
        }
    }
}
