using SmartTech.Marketing.Core.Messages;

namespace SmartTech.Marketing.Application.Business.UploadDownloadAttach
{
    public class DownloadAttachmentHandlerOutput : BaseResponse
    {
        public DownloadAttachmentHandlerOutput() { }
        public DownloadAttachmentHandlerOutput(Guid correlationId) : base(correlationId) { }
        public byte[] AttachmentFile { get; set; }
        public string FileName { get; set; }
    }
}
