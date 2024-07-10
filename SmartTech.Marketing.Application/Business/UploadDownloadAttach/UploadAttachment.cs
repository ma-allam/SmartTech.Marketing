using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SmartTech.Marketing.Application.Contract;
using SmartTech.Marketing.Core.AppSetting;
using SmartTech.Marketing.Core.Exceptions;
using SmartTech.Marketing.Domain.Entities;
using System.Diagnostics.Contracts;

namespace SmartTech.Marketing.Application.Business.UploadDownloadAttach
{
    public class UploadAttachmentHandler : IRequestHandler<UploadAttachmentHandlerInput, UploadAttachmentHandlerOutput>
    {
        private readonly IDataBaseService _databaseService;
        private readonly ILogger<UploadAttachmentHandler> _logger;
        private readonly IWebHostEnvironment _environment;
        private readonly IHttpContextAccessor _contextAccessor;

        public UploadAttachmentHandler(ILogger<UploadAttachmentHandler> logger, IDataBaseService databaseService, IWebHostEnvironment environment, IHttpContextAccessor contextAccessor)
        {
            _logger = logger;
            _databaseService = databaseService;
            _environment = environment;
            _contextAccessor = contextAccessor;
        }
        public async Task<UploadAttachmentHandlerOutput> Handle(UploadAttachmentHandlerInput request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling UploadAttachment business logic");
            UploadAttachmentHandlerOutput output = new UploadAttachmentHandlerOutput(request.CorrelationId());
            try
            {
                #region Upload Files
                var httpContext = _contextAccessor.HttpContext;

                if (request.Attachments != null && request.Attachments.Count > 0)
                {
                    var uploadsPath = Path.Combine(_environment.WebRootPath, SettingsDependancyInjection.FilesPathSettings.Path!);
                    if (string.IsNullOrEmpty(uploadsPath))
                    {
                        throw new WebApiException(WebApiExceptionSource.DynamicMessage, "Uploads path is not configured.");
                    }

                    if (!Directory.Exists(uploadsPath))
                    {
                        Directory.CreateDirectory(uploadsPath);
                    }

                    foreach (var file in request.Attachments)
                    {
                        try
                        {
                            if (file.Length > 5 * 1024 * 1024) // 5MB
                            {
                                throw new WebApiException(WebApiExceptionSource.DynamicMessage, "File size cannot exceed 5MB");
                            }
                            var FileName = Path.GetFileName(file.FileName);
                            var FileExtension = Path.GetExtension(file.FileName);
                            var FileId= Guid.NewGuid();
                            var SaveFileName = FileId.ToString() + FileExtension;
                            var filePath = Path.Combine(uploadsPath, SaveFileName);

                            using (var stream = new FileStream(filePath, FileMode.Create))
                            {
                                await file.CopyToAsync(stream);
                            }

                            var fileUrl = $"{httpContext.Request.Scheme}://{httpContext.Request.Host}/{SettingsDependancyInjection.FilesPathSettings.Path}/{SaveFileName}";

                            var fileMetadata = new ContractAttachments
                            {
                                ContractId = request.ContractId,
                                Tags = request.Tags,
                                Name = FileName,
                                AttachmentId=FileId,
                                FileExtension = FileExtension,
                                Notes = request.Notes,
                                FileUrl = fileUrl,
                                UploadDate= DateOnly.FromDateTime(DateTime.Now)
                            };

                            _databaseService.ContractAttachments.Add(fileMetadata);
                        }
                        catch (Exception ex)
                        {
                            _logger.LogError(ex, $"Error uploading file: {file.FileName}");
                            throw new WebApiException(WebApiExceptionSource.DynamicMessage, $"Error uploading file: {file.FileName}", ex);
                        }
                    }

                    await _databaseService.DBSaveChangesAsync(cancellationToken);
                }
                #endregion
            }
            catch (WebApiException ex)
            {
                _logger.LogError(ex, "Web API exception occurred.");
                throw; // Re-throw the WebApiException so it can be handled by global exception handler
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred.");
                throw new WebApiException(WebApiExceptionSource.GeneralException, "An unexpected error occurred during file upload.", ex);
            }
            output.Message = "Files Uploaded successfully";
            return output;
        }
    }
}
