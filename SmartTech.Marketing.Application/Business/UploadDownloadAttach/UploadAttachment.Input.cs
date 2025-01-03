﻿using MediatR;
using Microsoft.AspNetCore.Http;
using SmartTech.Marketing.Application.Business.ContractManagement.Command;
using SmartTech.Marketing.Core.Messages;

namespace SmartTech.Marketing.Application.Business.UploadDownloadAttach
{
    public class UploadAttachmentHandlerInput : BaseRequest, IRequest<UploadAttachmentHandlerOutput>
    {
        public UploadAttachmentHandlerInput() { }
        public UploadAttachmentHandlerInput(Guid correlationId) : base(correlationId) { }
        public int ContractId { get; set; }
        //public List<AttachmentInput> Attachments { get; set; }
        public string Tags { get; set; }
        public string? Notes { get; set; }
        public List<IFormFile> Attachments { get; set; }


    }
}
