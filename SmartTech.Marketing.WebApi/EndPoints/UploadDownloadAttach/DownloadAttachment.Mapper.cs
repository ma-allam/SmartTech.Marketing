
using AutoMapper;
using SmartTech.Marketing.Application.Business.UploadDownloadAttach;

namespace SmartTech.Marketing.WebApi.EndPoints.UploadDownloadAttach
{
    public class DownloadAttachmentMapper : Profile
    {
        public DownloadAttachmentMapper()
        {
            CreateMap<DownloadAttachmentEndPointRequest, DownloadAttachmentHandlerInput>()
                .ConstructUsing(src => new DownloadAttachmentHandlerInput(src.CorrelationId()));
            CreateMap<DownloadAttachmentHandlerOutput, DownloadAttachmentEndPointResponse>()
               .ConstructUsing(src => new DownloadAttachmentEndPointResponse(src.CorrelationId()));
        }

    }
}
