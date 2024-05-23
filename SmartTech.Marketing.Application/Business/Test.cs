using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SmartTech.Marketing.Application.Contract;
using SmartTech.Marketing.Core.Auth.Contract;

namespace SmartTech.Marketing.Application.Business
{
    public class TestHandler : IRequestHandler<TestHandlerInput, TestHandlerOutput>
    {
        private readonly IDataBaseService _databaseService;
        private readonly ILogger<TestHandler> _logger;
        private readonly ICurrentUserService _currentUserService;
        public TestHandler(ILogger<TestHandler> logger, IDataBaseService databaseService,ICurrentUserService currentUserService)
        {
            _logger = logger;
            _databaseService = databaseService;
            _currentUserService = currentUserService;
        }
        public async Task<TestHandlerOutput> Handle(TestHandlerInput request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling Test business logic");
            TestHandlerOutput output = new TestHandlerOutput(request.CorrelationId());
            _currentUserService.Load();
            var n=_currentUserService.activeContext.UserName;
            output.CountryName = await _databaseService.Country.Select(o=>o.CountryName).FirstOrDefaultAsync();
            return output;
        }
    }
}
