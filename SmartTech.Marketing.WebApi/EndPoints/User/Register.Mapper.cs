
using AutoMapper;
using SmartTech.Marketing.Application.Business.User;

namespace SmartTech.Marketing.WebApi.EndPoints.User
{
    public class RegisterMapper : Profile
    {
        public RegisterMapper()
        {
            CreateMap<RegisterEndPointRequest, RegisterHandlerInput>()
                .ConstructUsing(src => new RegisterHandlerInput(src.CorrelationId()));
            CreateMap<RegisterHandlerOutput, RegisterEndPointResponse>()
               .ConstructUsing(src => new RegisterEndPointResponse(src.CorrelationId()));
        }

    }
}
