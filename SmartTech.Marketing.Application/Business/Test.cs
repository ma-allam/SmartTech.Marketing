using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SmartTech.Marketing.Application.Contract;

namespace SmartTech.Marketing.Application.Business
{
    public class TestHandler : IRequestHandler<TestHandlerInput, TestHandlerOutput>
    {
        private readonly IDataBaseService _databaseService;
        private readonly ILogger<TestHandler> _logger;
        public TestHandler(ILogger<TestHandler> logger, IDataBaseService databaseService)
        {
            _logger = logger;
            _databaseService = databaseService;
        }
        public async Task<TestHandlerOutput> Handle(TestHandlerInput request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling Test business logic");
            TestHandlerOutput output = new TestHandlerOutput(request.CorrelationId());
            output.CountryName = await _databaseService.Country.Select(o=>o.CountryName).FirstOrDefaultAsync();
            return output;
        }
    }
}
