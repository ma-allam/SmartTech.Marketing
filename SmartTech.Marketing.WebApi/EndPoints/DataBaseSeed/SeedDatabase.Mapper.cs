
using AutoMapper;
using SmartTech.Marketing.Application.Business.DataBaseSeed;

namespace SmartTech.Marketing.WebApi.EndPoints.DataBaseSeed
{
    public class SeedDatabaseMapper : Profile
    {
        public SeedDatabaseMapper()
        {
            CreateMap<SeedDatabaseEndPointRequest, SeedDatabaseHandlerInput>()
                .ConstructUsing(src => new SeedDatabaseHandlerInput(src.CorrelationId()));
            CreateMap<SeedDatabaseHandlerOutput, SeedDatabaseEndPointResponse>()
               .ConstructUsing(src => new SeedDatabaseEndPointResponse(src.CorrelationId()));
        }

    }
}
