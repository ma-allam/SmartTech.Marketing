
using AutoMapper;
using SmartTech.Marketing.Application.Business.Clients.Query;

namespace SmartTech.Marketing.WebApi.EndPoints.Client.Query
{
    public class GetAllClientsMapper : Profile
    {
        public GetAllClientsMapper()
        {
            CreateMap<GetAllClientsEndPointRequest, GetAllClientsHandlerInput>()
                .ConstructUsing(src => new GetAllClientsHandlerInput(src.CorrelationId()));
            CreateMap<GetAllClientsHandlerOutput, GetAllClientsEndPointResponse>()
               .ConstructUsing(src => new GetAllClientsEndPointResponse(src.CorrelationId()));
        }

    }
}
