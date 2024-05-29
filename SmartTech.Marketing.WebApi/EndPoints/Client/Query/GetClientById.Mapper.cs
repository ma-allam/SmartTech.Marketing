
using AutoMapper;
using SmartTech.Marketing.Application.Business.Clients.Query;

namespace SmartTech.Marketing.WebApi.EndPoints.Client.Query
{
    public class GetClientByIdMapper : Profile
    {
        public GetClientByIdMapper()
        {
            CreateMap<GetClientByIdEndPointRequest, GetClientByIdHandlerInput>()
                .ConstructUsing(src => new GetClientByIdHandlerInput(src.CorrelationId()));
            CreateMap<GetClientByIdHandlerOutput, GetClientByIdEndPointResponse>()
               .ConstructUsing(src => new GetClientByIdEndPointResponse(src.CorrelationId()));
        }

    }
}
