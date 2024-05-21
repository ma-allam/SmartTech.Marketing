
using AutoMapper;
using SmartTech.Marketing.Application.Business;

namespace SmartTech.Marketing.WebApi.EndPoints
{
    public class TestMapper : Profile
    {
        public TestMapper()
        {
            CreateMap<TestEndPointRequest, TestHandlerInput>()
                .ConstructUsing(src => new TestHandlerInput(src.CorrelationId()));
            CreateMap<TestHandlerOutput, TestEndPointResponse>()
               .ConstructUsing(src => new TestEndPointResponse(src.CorrelationId()));
        }

    }
}
