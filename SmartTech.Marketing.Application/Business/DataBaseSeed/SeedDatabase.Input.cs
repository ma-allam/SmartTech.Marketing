using MediatR;
using SmartTech.Marketing.Core.Messages;

namespace SmartTech.Marketing.Application.Business.DataBaseSeed
{
    public class SeedDatabaseHandlerInput : BaseRequest, IRequest<SeedDatabaseHandlerOutput>
    {
        public SeedDatabaseHandlerInput() { }
        public SeedDatabaseHandlerInput(Guid correlationId) : base(correlationId) { }
    }
}
