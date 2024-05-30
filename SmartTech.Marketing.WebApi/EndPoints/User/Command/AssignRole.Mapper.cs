
using AutoMapper;
using SmartTech.Marketing.Application.Business.User.Command;

namespace SmartTech.Marketing.WebApi.EndPoints.User.Command
{
    public class AssignRoleMapper : Profile
    {
        public AssignRoleMapper()
        {
            CreateMap<AssignRoleEndPointRequest, AssignRoleHandlerInput>()
                .ConstructUsing(src => new AssignRoleHandlerInput(src.CorrelationId()));
            CreateMap<AssignRoleHandlerOutput, AssignRoleEndPointResponse>()
               .ConstructUsing(src => new AssignRoleEndPointResponse(src.CorrelationId()));
        }

    }
}
