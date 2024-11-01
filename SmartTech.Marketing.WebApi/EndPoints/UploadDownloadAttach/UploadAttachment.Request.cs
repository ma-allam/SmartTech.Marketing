﻿using SmartTech.Marketing.Application.Business.ContractManagement.Command;
using SmartTech.Marketing.Core.Messages;

namespace SmartTech.Marketing.WebApi.EndPoints.UploadDownloadAttach
{
    public class UploadAttachmentEndPointRequest : BaseRequest
    {
        public const string Route = "/api/UploadDownloadAttach/v{version:apiVersion}/UploadAttachment/";

        public int ContractId { get; set; }
        //public List<AttachmentInput> Attachments { get; set; }
        public string Tags { get; set; }
        public string? Notes { get; set; }
        public List<IFormFile> Attachments { get; set; }

    }
}
