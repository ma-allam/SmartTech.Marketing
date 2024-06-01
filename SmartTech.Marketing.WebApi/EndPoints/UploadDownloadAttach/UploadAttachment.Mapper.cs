
using AutoMapper;
using SmartTech.Marketing.Application.Business.UploadDownloadAttach;

namespace SmartTech.Marketing.WebApi.EndPoints.UploadDownloadAttach
{
    public class UploadAttachmentMapper : Profile
    {
        public UploadAttachmentMapper()
        {
            CreateMap<UploadAttachmentEndPointRequest, UploadAttachmentHandlerInput>()
                .ConstructUsing(src => new UploadAttachmentHandlerInput(src.CorrelationId()));
            CreateMap<UploadAttachmentHandlerOutput, UploadAttachmentEndPointResponse>()
               .ConstructUsing(src => new UploadAttachmentEndPointResponse(src.CorrelationId()));
        }

    }
}
