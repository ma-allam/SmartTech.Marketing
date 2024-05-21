
using SmartTech.Marketing.Core.Messages;
namespace SmartTech.Marketing.Application.Business
{
    public class TestHandlerOutput : BaseResponse
    {
        public TestHandlerOutput() { }
        public TestHandlerOutput(Guid correlationId) : base(correlationId) { }
        public string? CountryName { get; set; }
    }
}
