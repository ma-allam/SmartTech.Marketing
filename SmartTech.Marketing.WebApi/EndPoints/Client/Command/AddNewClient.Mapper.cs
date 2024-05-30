
using AutoMapper;
using SmartTech.Marketing.Application.Business.Clients.Command;

namespace SmartTech.Marketing.WebApi.EndPoints.Client.Query.Command
{
    public class AddNewClientMapper : Profile
    {
        public AddNewClientMapper()
        {
            CreateMap<AddNewClientEndPointRequest, AddNewClientHandlerInput>()
                .ConstructUsing(src => new AddNewClientHandlerInput(src.CorrelationId()));
            CreateMap<AddNewClientHandlerOutput, AddNewClientEndPointResponse>()
               .ConstructUsing(src => new AddNewClientEndPointResponse(src.CorrelationId()));
        }

    }
}
