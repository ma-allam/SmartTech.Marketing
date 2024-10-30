
using AutoMapper;
using SmartTech.Marketing.Application.Business.Order.Command;

namespace SmartTech.Marketing.WebApi.EndPoints.Order.Command
{
    public class UpdateOrderMapper : Profile
    {
        public UpdateOrderMapper()
        {
            CreateMap<UpdateOrderEndPointRequest, UpdateOrderHandlerInput>()
                .ConstructUsing(src => new UpdateOrderHandlerInput(src.CorrelationId()));
            CreateMap<UpdateOrderHandlerOutput, UpdateOrderEndPointResponse>()
               .ConstructUsing(src => new UpdateOrderEndPointResponse(src.CorrelationId()));
        }

    }
}
