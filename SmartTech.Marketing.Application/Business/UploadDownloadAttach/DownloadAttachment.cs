using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SmartTech.Marketing.Application.Contract;
using SmartTech.Marketing.Core.Exceptions;

namespace SmartTech.Marketing.Application.Business.UploadDownloadAttach
{
    public class DownloadAttachmentHandler : IRequestHandler<DownloadAttachmentHandlerInput, DownloadAttachmentHandlerOutput>
    {
        private readonly IDataBaseService _databaseService;
        private readonly ILogger<DownloadAttachmentHandler> _logger;
        private readonly IWebHostEnvironment _environment;

        public DownloadAttachmentHandler(ILogger<DownloadAttachmentHandler> logger, IDataBaseService databaseService, IWebHostEnvironment environment)
        {
            _logger = logger;
            _databaseService = databaseService;
            _environment = environment;
        }
        public async Task<DownloadAttachmentHandlerOutput> Handle(DownloadAttachmentHandlerInput request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling DownloadAttachment business logic");
            DownloadAttachmentHandlerOutput output = new DownloadAttachmentHandlerOutput(request.CorrelationId());
            var attachment = await _databaseService.ContractAttachments.Where(o => o.Name.StartsWith(request.AttachmentId.ToString())).FirstOrDefaultAsync(); ;
            if (attachment == null)
            {
                _logger.LogError($"File with ID {request.AttachmentId} not found.");
                throw new WebApiException("File not found.");
            }

            var filePath = Path.Combine(_environment.WebRootPath, "uploads", attachment.Name);
            if (!File.Exists(filePath))
            {
                _logger.LogError($"File not found on disk: {filePath}");
                throw new WebApiException("File not found on disk.");
            }

            output.AttachmentFile = await File.ReadAllBytesAsync(filePath, cancellationToken);
            //var contentType = "application/octet-stream"; // You can set this based on the file type            return output;
            output.FileName = attachment.Name;

            return output;
        }
    }
}
