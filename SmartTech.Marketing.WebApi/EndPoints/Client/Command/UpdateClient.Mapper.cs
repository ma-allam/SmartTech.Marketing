
using AutoMapper;
using SmartTech.Marketing.Application.Business.Clients.Command;

namespace SmartTech.Marketing.WebApi.EndPoints.Client.Command
{
    public class UpdateClientMapper : Profile
    {
        public UpdateClientMapper()
        {
            CreateMap<UpdateClientEndPointRequest, UpdateClientHandlerInput>()
                .ConstructUsing(src => new UpdateClientHandlerInput(src.CorrelationId()));
            CreateMap<UpdateClientHandlerOutput, UpdateClientEndPointResponse>()
               .ConstructUsing(src => new UpdateClientEndPointResponse(src.CorrelationId()));
        }

    }
}
