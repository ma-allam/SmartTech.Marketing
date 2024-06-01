using MediatR;
using SmartTech.Marketing.Core.Messages;

namespace SmartTech.Marketing.Application.Business.UploadDownloadAttach
{
    public class DownloadAttachmentHandlerInput : BaseRequest, IRequest<DownloadAttachmentHandlerOutput>
    {
        public DownloadAttachmentHandlerInput() { }
        public DownloadAttachmentHandlerInput(Guid correlationId) : base(correlationId) { }

        public Guid AttachmentId { get;  set; }
    }
}
