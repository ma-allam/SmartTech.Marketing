using SmartTech.Marketing.Core.Messages;

namespace SmartTech.Marketing.Application.Business.DataBaseSeed
{
    public class SeedDatabaseHandlerOutput : BaseResponse
    {
        public SeedDatabaseHandlerOutput() { }
        public SeedDatabaseHandlerOutput(Guid correlationId) : base(correlationId) { }

    }
}
