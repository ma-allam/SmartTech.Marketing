
using AutoMapper;
using SmartTech.Marketing.Application.Business.Order.Command;

namespace SmartTech.Marketing.WebApi.EndPoints.Order.Command
{
    public class AddNewOrderMapper : Profile
    {
        public AddNewOrderMapper()
        {
            CreateMap<AddNewOrderEndPointRequest, AddNewOrderHandlerInput>()
                .ConstructUsing(src => new AddNewOrderHandlerInput(src.CorrelationId()));
            CreateMap<AddNewOrderHandlerOutput, AddNewOrderEndPointResponse>()
               .ConstructUsing(src => new AddNewOrderEndPointResponse(src.CorrelationId()));
        }

    }
}
