

using SmartTech.Marketing.Core.Messages;

namespace SmartTech.Marketing.WebApi.EndPoints.UploadDownloadAttach
{
    public class UploadAttachmentEndPointResponse : BaseResponse
    {
        public UploadAttachmentEndPointResponse() { }
        public UploadAttachmentEndPointResponse(Guid correlationId) : base(correlationId) { }
        public string Message { get; set; }

    }
}
