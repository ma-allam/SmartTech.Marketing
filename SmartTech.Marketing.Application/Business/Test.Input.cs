using MediatR;
using SmartTech.Marketing.Core.Messages;
namespace SmartTech.Marketing.Application.Business
{
    public class TestHandlerInput : BaseRequest, IRequest<TestHandlerOutput>
    {
        public TestHandlerInput() { }
        public TestHandlerInput(Guid correlationId) : base(correlationId) { }
    }
}
