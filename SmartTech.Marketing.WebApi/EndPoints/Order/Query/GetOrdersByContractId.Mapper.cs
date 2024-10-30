
using AutoMapper;
using SmartTech.Marketing.Application.Business.Order.Query;

namespace SmartTech.Marketing.WebApi.EndPoints.Order.Query
{
    public class GetOrdersByContractIdMapper : Profile
    {
        public GetOrdersByContractIdMapper()
        {
            CreateMap<GetOrdersByContractIdEndPointRequest, GetOrdersByContractIdHandlerInput>()
                .ConstructUsing(src => new GetOrdersByContractIdHandlerInput(src.CorrelationId()));
            CreateMap<GetOrdersByContractIdHandlerOutput, GetOrdersByContractIdEndPointResponse>()
               .ConstructUsing(src => new GetOrdersByContractIdEndPointResponse(src.CorrelationId()));
        }

    }
}
