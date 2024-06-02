
using AutoMapper;
using SmartTech.Marketing.Application.Business.ContractManagement.Query;

namespace SmartTech.Marketing.WebApi.EndPoints.ContractManagement.Query
{
    public class GetAllContractsMapper : Profile
    {
        public GetAllContractsMapper()
        {
            CreateMap<GetAllContractsEndPointRequest, GetAllContractsHandlerInput>()
                .ConstructUsing(src => new GetAllContractsHandlerInput(src.CorrelationId()));
            CreateMap<GetAllContractsHandlerOutput, GetAllContractsEndPointResponse>()
               .ConstructUsing(src => new GetAllContractsEndPointResponse(src.CorrelationId()));
        }

    }
}
