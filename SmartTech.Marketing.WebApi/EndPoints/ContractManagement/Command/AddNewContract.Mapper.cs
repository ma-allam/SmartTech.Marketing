
using AutoMapper;
using SmartTech.Marketing.Application.Business.ContractManagement.Command;

namespace SmartTech.Marketing.WebApi.EndPoints.ContractManagement.Command
{
    public class AddNewContractMapper : Profile
    {
        public AddNewContractMapper()
        {
            CreateMap<AddNewContractEndPointRequest, AddNewContractHandlerInput>()
                .ConstructUsing(src => new AddNewContractHandlerInput(src.CorrelationId()));
            CreateMap<AddNewContractHandlerOutput, AddNewContractEndPointResponse>()
               .ConstructUsing(src => new AddNewContractEndPointResponse(src.CorrelationId()));
        }

    }
}
