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
                    #region Add Contract
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
                        Enabled = true,
                        AcceptableCloudPerc = request.AcceptableCloudPerc,
                        MinSquareArea = request.MinSquareArea
                    };

                    _databaseService.Contracts.Add(contract);
                    await _databaseService.DBSaveChangesAsync(cancellationToken);
                    #endregion

                    #region Add Related Data

                    // Add Contract Due Dates
                    foreach (var dueDate in request.ContractDueDates)
                    {
                        var contractDueDate = new ContractDueDates
                        {
                            ContractId = contract.Id,
                            DueDate = dueDate.DueDate,
                            Notes = dueDate.Notes
                        };
                        _databaseService.ContractDueDates.Add(contractDueDate);
                    }

                    // Add Contract Image Modes
                    foreach (var imageMode in request.ContractImageModes)
                    {
                        var contractImageMode = new ContractImageModes
                        {
                            ContractId = contract.Id,
                            Name = imageMode.Name,
                            CreditFactor = imageMode.CreditFactor
                        };
                        _databaseService.ContractImageModes.Add(contractImageMode);
                    }

                    // Add Contract Image Resolutions
                    foreach (var imageResolution in request.ContractImageResolutions)
                    {
                        var contractImageResolution = new ContractImageResolution
                        {
                            ContractId = contract.Id,
                            ResolutionInCm = imageResolution.ResolutionInCm,
                            MinOrderAreaSize = imageResolution.MinOrderAreaSize,
                            CreditFactor = imageResolution.CreditFactor,
                            ContractImageTypeId = imageResolution.ContractImageTypeId
                        };
                        _databaseService.ContractImageResolution.Add(contractImageResolution);
                    }

                    // Add Contract Order Priorities
                    foreach (var orderPriority in request.ContractOrderPriorities)
                    {
                        var contractOrderPriority = new ContractOrderPriority
                        {
                            ContractId = contract.Id,
                            Name = orderPriority.Name,
                            MaxAllowedDays = orderPriority.MaxAllowedDays,
                            CreditFactor = orderPriority.CreditFactor
                        };
                        _databaseService.ContractOrderPriority.Add(contractOrderPriority);
                    }

                    // Add Contract Payment Information
                    foreach (var paymentInformation in request.ContractPaymentInformation)
                    {
                        var contractPaymentInfo = new ContractPaymentInformation
                        {
                            ContractId = contract.Id,
                            BankName = paymentInformation.BankName,
                            BankBranch = paymentInformation.BankBranch,
                            BankAddress = paymentInformation.BankAddress,
                            Iban = paymentInformation.Iban,
                            ClientNameInBank = paymentInformation.ClientNameInBank,
                            Notes = paymentInformation.Notes
                        };
                        _databaseService.ContractPaymentInformation.Add(contractPaymentInfo);
                    }

                    // Add Contract Periods
                    foreach (var period in request.ContractPeriods)
                    {
                        var contractPeriod = new ContractPeriods
                        {
                            ContractId = contract.Id,
                            StartDate = period.StartDate,
                            EndDate = period.EndDate,
                            AvailableCredit = period.AvailableCredit,
                            RemainingCredit = period.RemainingCredit
                        };
                        _databaseService.ContractPeriods.Add(contractPeriod);
                    }

                    // Add Contract Services
                    foreach (var service in request.ContractServices)
                    {
                        var contractService = new ContractServices
                        {
                            ContractId = contract.Id,
                            ServiceName = service.ServiceName,
                            ServiceCost = service.ServiceCost,
                            Notes = service.Notes
                        };
                        _databaseService.ContractServices.Add(contractService);
                    }

                    await _databaseService.DBSaveChangesAsync(cancellationToken);
                    #endregion

                    //#region Upload Files
                    //var httpContext = _contextAccessor.HttpContext;

                    //if (request.Attachments != null && request.Attachments.Count > 0)
                    //{
                    //    var uploadsPath = Path.Combine(_environment.WebRootPath, SettingsDependancyInjection.FilesPathSettings.Path!);
                    //    if (string.IsNullOrEmpty(uploadsPath))
                    //    {
                    //        throw new WebApiException(WebApiExceptionSource.DynamicMessage, "Uploads path is not configured.");
                    //    }

                    //    if (!Directory.Exists(uploadsPath))
                    //    {
                    //        Directory.CreateDirectory(uploadsPath);
                    //    }

                    //    foreach (var file in request.Attachments)
                    //    {
                    //        try
                    //        {
                    //            if (file.File.Length > 5 * 1024 * 1024) // 5MB
                    //            {
                    //                throw new WebApiException(WebApiExceptionSource.DynamicMessage, "File size cannot exceed 5MB");
                    //            }
                    //            var FileName = Path.GetFileName(file.File.FileName);
                    //            var FileExtension = Path.GetExtension(file.File.FileName);
                    //            var FileId = Guid.NewGuid();
                    //            var SaveFileName = FileId.ToString() + FileExtension;
                    //            var filePath = Path.Combine(uploadsPath, SaveFileName);

                    //            using (var stream = new FileStream(filePath, FileMode.Create))
                    //            {
                    //                await file.File.CopyToAsync(stream);
                    //            }

                    //            var fileUrl = $"{httpContext.Request.Scheme}://{httpContext.Request.Host}/{SettingsDependancyInjection.FilesPathSettings.Path}/{SaveFileName}";

                    //            var fileMetadata = new ContractAttachments
                    //            {
                    //                ContractId = contract.Id,
                    //                Tags = file.Tags,
                    //                Name = FileName,
                    //                AttachmentId = FileId,
                    //                FileExtension = FileExtension,
                    //                Notes = file.Notes,
                    //                FileUrl = fileUrl,
                    //                UploadDate = DateOnly.FromDateTime(DateTime.Now)
                    //            };

                    //            _databaseService.ContractAttachments.Add(fileMetadata);
                    //        }
                    //        catch (Exception ex)
                    //        {
                    //            _logger.LogError(ex, $"Error uploading file: {file.File.FileName}");
                    //            throw new WebApiException(WebApiExceptionSource.DynamicMessage, $"Error uploading file: {file.File.FileName}", ex);
                    //        }
                    //    }

                    //    await _databaseService.DBSaveChangesAsync(cancellationToken);
                    //}
                    //#endregion

                    await transaction.CommitAsync(cancellationToken);
                    output.Message = "Contract created successfully with all related data.";
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
        
    }
}
