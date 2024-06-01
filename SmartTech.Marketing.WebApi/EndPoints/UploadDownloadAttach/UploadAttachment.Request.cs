using SmartTech.Marketing.Application.Business.ContractManagement.Command;
using SmartTech.Marketing.Core.Messages;

namespace SmartTech.Marketing.WebApi.EndPoints.UploadDownloadAttach
{
    public class UploadAttachmentEndPointRequest : BaseRequest
    {
        public const string Route = "/api/UploadDownloadAttach/v{version:apiVersion}/UploadAttachment/";

        public int ContractId { get; set; }
        public List<AttachmentData> Attachments { get; set; }
    }
}
