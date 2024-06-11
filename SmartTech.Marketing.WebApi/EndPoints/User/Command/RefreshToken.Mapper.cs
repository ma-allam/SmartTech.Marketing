
using AutoMapper;
using SmartTech.Marketing.Application.Business.User.Command;

namespace SmartTech.Marketing.WebApi.EndPoints.User.Command
{
    public class RefreshTokenMapper : Profile
    {
        public RefreshTokenMapper()
        {
            CreateMap<RefreshTokenEndPointRequest, RefreshTokenHandlerInput>()
                .ConstructUsing(src => new RefreshTokenHandlerInput(src.CorrelationId()));
            CreateMap<RefreshTokenHandlerOutput, RefreshTokenEndPointResponse>()
               .ConstructUsing(src => new RefreshTokenEndPointResponse(src.CorrelationId()));
        }

    }
}
