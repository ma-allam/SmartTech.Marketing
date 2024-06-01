using SmartTech.Marketing.Core.Messages;

namespace SmartTech.Marketing.Application.Business.UploadDownloadAttach
{
    public class UploadAttachmentHandlerOutput : BaseResponse
    {
        public UploadAttachmentHandlerOutput() { }
        public UploadAttachmentHandlerOutput(Guid correlationId) : base(correlationId) { }
        public string Message { get; set; }
    }
}
