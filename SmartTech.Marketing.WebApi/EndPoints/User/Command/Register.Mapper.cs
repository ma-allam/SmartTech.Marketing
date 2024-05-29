
using AutoMapper;
using SmartTech.Marketing.Application.Business.User.Command;

namespace SmartTech.Marketing.WebApi.EndPoints.User.Command
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
