using MediatR;
using SmartTech.Marketing.Application.Business.ContractManagement.Command;
using SmartTech.Marketing.Core.Messages;

namespace SmartTech.Marketing.Application.Business.UploadDownloadAttach
{
    public class UploadAttachmentHandlerInput : BaseRequest, IRequest<UploadAttachmentHandlerOutput>
    {
        public UploadAttachmentHandlerInput() { }
        public UploadAttachmentHandlerInput(Guid correlationId) : base(correlationId) { }
        public int ContractId { get; set; }
        public List<AttachmentData> Attachments { get; set; }

    }
}
