
using AutoMapper;
using SmartTech.Marketing.Application.Business.ContractManagement.Query;

namespace SmartTech.Marketing.WebApi.EndPoints.ContractManagement.Query
{
    public class GetContractByIdMapper : Profile
    {
        public GetContractByIdMapper()
        {
            CreateMap<GetContractByIdEndPointRequest, GetContractByIdHandlerInput>()
                .ConstructUsing(src => new GetContractByIdHandlerInput(src.CorrelationId()));
            CreateMap<GetContractByIdHandlerOutput, GetContractByIdEndPointResponse>()
               .ConstructUsing(src => new GetContractByIdEndPointResponse(src.CorrelationId()));
        }

    }
}
