

using SmartTech.Marketing.Core.Messages;

namespace SmartTech.Marketing.WebApi.EndPoints
{
    public class TestEndPointResponse : BaseResponse
    {
        public TestEndPointResponse() { }
        public TestEndPointResponse(Guid correlationId) : base(correlationId) { }
        public string? CountryName { get; set; }
    }
}