
using AutoMapper;
using SmartTech.Marketing.Application.Business.ContractManagement.Command;

namespace SmartTech.Marketing.WebApi.EndPoints.ContractManagement.Command
{
    public class UpdateContractMapper : Profile
    {
        public UpdateContractMapper()
        {
            CreateMap<UpdateContractEndPointRequest, UpdateContractHandlerInput>()
                .ConstructUsing(src => new UpdateContractHandlerInput(src.CorrelationId()));
            CreateMap<UpdateContractHandlerOutput, UpdateContractEndPointResponse>()
               .ConstructUsing(src => new UpdateContractEndPointResponse(src.CorrelationId()));
        }

    }
}
