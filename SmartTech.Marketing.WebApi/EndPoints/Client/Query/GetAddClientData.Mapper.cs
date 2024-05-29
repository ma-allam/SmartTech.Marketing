
using AutoMapper;
using SmartTech.Marketing.Application.Business.Clients.Query;

namespace SmartTech.Marketing.WebApi.EndPoints.Client.Query
{
    public class GetAddClientDataMapper : Profile
    {
        public GetAddClientDataMapper()
        {
            CreateMap<GetAddClientDataEndPointRequest, GetAddClientDataHandlerInput>()
                .ConstructUsing(src => new GetAddClientDataHandlerInput(src.CorrelationId()));
            CreateMap<GetAddClientDataHandlerOutput, GetAddClientDataEndPointResponse>()
               .ConstructUsing(src => new GetAddClientDataEndPointResponse(src.CorrelationId()));
        }

    }
}
