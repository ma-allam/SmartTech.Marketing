
using AutoMapper;
using SmartTech.Marketing.Application.Business.User.Command;

namespace SmartTech.Marketing.WebApi.EndPoints.User.Command
{
    public class AddRoleMapper : Profile
    {
        public AddRoleMapper()
        {
            CreateMap<AddRoleEndPointRequest, AddRoleHandlerInput>()
                .ConstructUsing(src => new AddRoleHandlerInput(src.CorrelationId()));
            CreateMap<AddRoleHandlerOutput, AddRoleEndPointResponse>()
               .ConstructUsing(src => new AddRoleEndPointResponse(src.CorrelationId()));
        }

    }
}
