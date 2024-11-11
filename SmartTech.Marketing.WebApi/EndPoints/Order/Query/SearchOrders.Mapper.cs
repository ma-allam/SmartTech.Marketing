
using AutoMapper;
using SmartTech.Marketing.Application.Business.Order.Query;

namespace SmartTech.Marketing.WebApi.EndPoints.Order.Query
{
    public class SearchOrdersMapper : Profile
    {
        public SearchOrdersMapper()
        {
            CreateMap<SearchOrdersEndPointRequest, SearchOrdersHandlerInput>()
                .ConstructUsing(src => new SearchOrdersHandlerInput(src.CorrelationId()));
            CreateMap<SearchOrdersHandlerOutput, SearchOrdersEndPointResponse>()
               .ConstructUsing(src => new SearchOrdersEndPointResponse(src.CorrelationId()));
        }

    }
}
