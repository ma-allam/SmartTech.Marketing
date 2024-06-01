using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SmartTech.Marketing.Application.Contract;
using SmartTech.Marketing.Core.AppSetting;
using SmartTech.Marketing.Core.Exceptions;
using SmartTech.Marketing.Domain.Entities;

namespace SmartTech.Marketing.Application.Business.ContractManagement.Command
{
    public class AddNewContractHandler : IRequestHandler<AddNewContractHandlerInput, AddNewContractHandlerOutput>
    {
        private readonly IDataBaseService _databaseService;
        private readonly ILogger<AddNewContractHandler> _logger;
        private readonly IWebHostEnvironment _environment;
        private readonly IHttpContextAccessor _contextAccessor;

        public AddNewContractHandler(ILogger<AddNewContractHandler> logger, IDataBaseService databaseService, IWebHostEnvironment environment, IHttpContextAccessor contextAccessor)
        {
            _logger = logger;
            _databaseService = databaseService;
            _environment = environment;
            _contextAccessor = contextAccessor;
        }
        public async Task<AddNewContractHandlerOutput> Handle(AddNewContractHandlerInput request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling AddNewContract business logic");
            AddNewContractHandlerOutput output = new AddNewContractHandlerOutput(request.CorrelationId());

            using (var transaction = await _databaseService.BeginTransactionAsync(cancellationToken))
            {
                try
                {
                    #region Add Contact
                    var contract = new Contracts
                    {
                        ClientId = request.SelectedClientId,
                        ContractNumber = request.ContractNumber,
                        StartDate = request.StartDate,
                        EndDate = request.EndDate,
                        TotalContractCost = request.TotalContractCost,
                        TotalCredit = request.TotalCredit,
                        CurrencyId = request.SelectedCurrencyId,
                        Notes = request.Notes,
                        ContractPaymentTypeId = request.SelectedPaymentTypeId,
                        Enabled = true
                    };

                    _databaseService.Contracts.Add(contract);
                    await _databaseService.DBSaveChangesAsync(cancellationToken);
                    #endregion
                    #region Upload Files
                    var httpContext = _contextAccessor.HttpContext;

                    if (request.Attachments != null && request.Attachments.Count > 0)
                    {
                        var uploadsPath = Path.Combine(_environment.WebRootPath, SettingsDependancyInjection.FilesPathSettings.Path!);
                        if (!Directory.Exists(uploadsPath))
                        {
                            Directory.CreateDirectory(uploadsPath);
                        }

                        foreach (var file in request.Attachments)
                        {
                            if (file.File.Length > 5 * 1024 * 1024) // 5MB
                            {
                                throw new WebApiException(WebApiExceptionSource.DynamicMessage, "File size cannot exceed 5MB");
                            }
                            var fileExtension = Path.GetExtension(file.File.FileName);
                            var fileName = Guid.NewGuid().ToString() + fileExtension;
                            var filePath = Path.Combine(uploadsPath, fileName);

                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                await file.File.CopyToAsync(stream);
                            }

                            var fileMetadata = new ContractAttachments
                            {
                                ContractId = contract.Id,
                                Description=file.Description,
                                Name = fileName,
                                Notes=file.Notes,
                                FileUrl = $"{httpContext.Request.Scheme}://{httpContext.Request.Host}/{SettingsDependancyInjection.FilesPathSettings.Path}/{fileName}"

                            };

                            _databaseService.ContractAttachments.Add(fileMetadata);
                        }

                        await _databaseService.DBSaveChangesAsync(cancellationToken);
                    }
                    #endregion

                    
                    await transaction.CommitAsync(cancellationToken);
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync(cancellationToken);
                    _logger.LogError(ex, "Error occurred during contract creation");
                    throw new WebApiException(WebApiExceptionSource.DynamicMessage, ex.Message);
                }
            }

            return output;
        }
        public string Upload(List<IFormFile> formFiles)
        {

            return "";
        }
    }
}
