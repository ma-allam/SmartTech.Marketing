using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SmartTech.Marketing.Application.Contract;
using SmartTech.Marketing.Core.AppSetting;
using SmartTech.Marketing.Core.Exceptions;
using SmartTech.Marketing.Domain.Entities;
using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace SmartTech.Marketing.Application.Business.ContractManagement.Command
{
    public class UpdateContractHandler : IRequestHandler<UpdateContractHandlerInput, UpdateContractHandlerOutput>
    {
        private readonly IDataBaseService _databaseService;
        private readonly ILogger<UpdateContractHandler> _logger;
        private readonly IWebHostEnvironment _environment;
        private readonly IHttpContextAccessor _contextAccessor;

        public UpdateContractHandler(ILogger<UpdateContractHandler> logger, IDataBaseService databaseService, IWebHostEnvironment environment, IHttpContextAccessor contextAccessor)
        {
            _logger = logger;
            _databaseService = databaseService;
            _environment = environment;
            _contextAccessor = contextAccessor;
        }

        public async Task<UpdateContractHandlerOutput> Handle(UpdateContractHandlerInput request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling UpdateContract business logic");
            UpdateContractHandlerOutput output = new UpdateContractHandlerOutput(request.CorrelationId());

            using (var transaction = await _databaseService.BeginTransactionAsync(cancellationToken))
            {
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
                        .FirstOrDefaultAsync(c => c.Id == request.ContractId, cancellationToken);

                    if (contract == null)
                    {
                        throw new WebApiException(WebApiExceptionSource.DynamicMessage, "Contract not found");
                    }

                    #region Update Contract
                    contract.ClientId = request.SelectedClientId;
                    contract.ContractNumber = request.ContractNumber;
                    contract.StartDate = request.StartDate;
                    contract.EndDate = request.EndDate;
                    contract.TotalContractCost = request.TotalContractCost;
                    contract.TotalCredit = request.TotalCredit;
                    contract.CurrencyId = request.SelectedCurrencyId;
                    contract.Notes = request.Notes;
                    contract.ContractPaymentTypeId = request.SelectedPaymentTypeId;
                    contract.Enabled = request.Enabled;
                    contract.AcceptableCloudPerc = request.AcceptableCloudPerc;
                    contract.MinSquareArea = request.MinSquareArea;
                    #endregion

                    #region Update Related Data
                    // Update Contract Due Dates
                    _databaseService.ContractDueDates.RemoveRange(contract.ContractDueDates);
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

                    // Update Contract Image Modes
                    _databaseService.ContractImageModes.RemoveRange(contract.ContractImageModes);
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

                    // Update Contract Image Resolutions
                    _databaseService.ContractImageResolution.RemoveRange(contract.ContractImageResolution);
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

                    // Update Contract Order Priorities
                    _databaseService.ContractOrderPriority.RemoveRange(contract.ContractOrderPriority);
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

                    // Update Contract Payment Information
                    _databaseService.ContractPaymentInformation.RemoveRange(contract.ContractPaymentInformation);
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

                    // Update Contract Periods
                    _databaseService.ContractPeriods.RemoveRange(contract.ContractPeriods);
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

                    // Update Contract Services
                    _databaseService.ContractServices.RemoveRange(contract.ContractServices);
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
                    output.Message = "Contract updated successfully with all related data.";
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync(cancellationToken);
                    _logger.LogError(ex, "Error occurred during contract update");
                    throw new WebApiException(WebApiExceptionSource.DynamicMessage, ex.Message);
                }
            }

            return output;
        }
    }
}
