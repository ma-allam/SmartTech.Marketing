using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SmartTech.Marketing.Application.Business.ContractManagement.Query;
using SmartTech.Marketing.Application.Contract;
using SmartTech.Marketing.Core.Auth.Contract;
using SmartTech.Marketing.Domain.Entities;
using System.Diagnostics.Contracts;

namespace SmartTech.Marketing.Application.Business.Clients.Query
{
    public class GetAllClientsHandler : IRequestHandler<GetAllClientsHandlerInput, GetAllClientsHandlerOutput>
    {
        private readonly IDataBaseService _databaseService;
        private readonly ILogger<GetAllClientsHandler> _logger;
        private readonly ICurrentUserService _currentUserService;
        public GetAllClientsHandler(ILogger<GetAllClientsHandler> logger, IDataBaseService databaseService, ICurrentUserService currentUserService = null)
        {
            _logger = logger;
            _databaseService = databaseService;
            _currentUserService = currentUserService;
        }
        public async Task<GetAllClientsHandlerOutput> Handle(GetAllClientsHandlerInput request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling GetAllClients business logic");
            GetAllClientsHandlerOutput output = new GetAllClientsHandlerOutput(request.CorrelationId());
          
            output.Clients = await _databaseService.Client.Select(o => new ClientData { ClientId=o.Id,Name = o.Name, Email = o.Email, Username = o.User.UserName, PhoneNumber = o.PhoneNumber, Country = new Data { Id = o.CountryId, Name = o.Country.CountryName }, ClientType = new Data { Id = o.ClientType, Name = o.ClientTypeNavigation.Type } ,Contracts=o.Contracts.Select(c=> new ContractDto
            {
                Id = c.Id,
                ContractNumber = c.ContractNumber,
                StartDate = c.StartDate,
                EndDate = c.EndDate,
                TotalContractCost = c.TotalContractCost,
                TotalCredit = c.TotalCredit,
                CurrencyId = c.CurrencyId,
                Notes = c.Notes,
                ClientId = c.ClientId,
                ContractPaymentTypeId = c.ContractPaymentTypeId,
                Enabled = c.Enabled,
                AcceptableCloudPerc = c.AcceptableCloudPerc,
                MinSquareArea = c.MinSquareArea,
                ContractDueDates = c.ContractDueDates.Select(d => new ContractDueDateDto
                {
                    Id = d.Id,
                    DueDate = d.DueDate,
                    Notes = d.Notes
                }).ToList(),
                ContractImageModes = c.ContractImageModes.Select(im => new ContractImageModeDto
                {
                    Id = im.Id,
                    Name = im.Name,
                    CreditFactor = im.CreditFactor
                }).ToList(),
                ContractImageResolutions = c.ContractImageResolution.Select(ir => new ContractImageResolutionDto
                {
                    Id = ir.Id,
                    ResolutionInCm = ir.ResolutionInCm,
                    MinOrderAreaSize = ir.MinOrderAreaSize,
                    CreditFactor = ir.CreditFactor,
                    ContractImageTypeId = ir.ContractImageTypeId
                }).ToList(),
                ContractOrderPriorities = c.ContractOrderPriority.Select(op => new ContractOrderPriorityDto
                {
                    Id = op.Id,
                    Name = op.Name,
                    MaxAllowedDays = op.MaxAllowedDays,
                    CreditFactor = op.CreditFactor
                }).ToList(),
                ContractPaymentInformation = c.ContractPaymentInformation.Select(pi => new ContractPaymentInformationDto
                {
                    Id = pi.Id,
                    BankName = pi.BankName,
                    BankBranch = pi.BankBranch,
                    BankAddress = pi.BankAddress,
                    Iban = pi.Iban,
                    ClientNameInBank = pi.ClientNameInBank,
                    Notes = pi.Notes
                }).ToList(),
                ContractPeriods = c.ContractPeriods.Select(cp => new ContractPeriodDto
                {
                    Id = cp.Id,
                    StartDate = cp.StartDate,
                    EndDate = cp.EndDate,
                    AvailableCredit = cp.AvailableCredit,
                    RemainingCredit = cp.RemainingCredit
                }).ToList(),
                ContractServices = c.ContractServices.Select(cs => new ContractServiceDto
                {
                    Id = cs.Id,
                    ServiceName = cs.ServiceName,
                    ServiceCost = cs.ServiceCost,
                    Notes = cs.Notes
                }).ToList(),
                ContractAttachments = c.ContractAttachments.Select(ca => new ContractAttachmentDto
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
           }).ToList()
            }).ToListAsync();
            return output;
        }
    }
}
