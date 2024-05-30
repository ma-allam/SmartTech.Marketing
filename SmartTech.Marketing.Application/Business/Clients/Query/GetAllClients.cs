using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SmartTech.Marketing.Application.Contract;
using SmartTech.Marketing.Core.Auth.Contract;

namespace SmartTech.Marketing.Application.Business.Clients.Query
{
    public class GetAllClientsHandler : IRequestHandler<GetAllClientsHandlerInput, GetAllClientsHandlerOutput>
    {
        private readonly IDataBaseService _databaseService;
        private readonly ILogger<GetAllClientsHandler> _logger;
        private readonly ICurrentUserService _currentUserService;
        public GetAllClientsHandler(ILogger<GetAllClientsHandler> logger, IDataBaseService databaseService, ICurrentUserService currentUserService = null)
        {
            _logger = logger;
            _databaseService = databaseService;
            _currentUserService = currentUserService;
        }
        public async Task<GetAllClientsHandlerOutput> Handle(GetAllClientsHandlerInput request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling GetAllClients business logic");
            GetAllClientsHandlerOutput output = new GetAllClientsHandlerOutput(request.CorrelationId());
          
            output.Clients = await _databaseService.Client.Select(o => new ClientData { ClientId=o.Id,Name = o.Name, Email = o.Email, Username = o.User.UserName, PhoneNumber = o.PhoneNumber, Country = new Data { Id = o.CountryId, Name = o.Country.CountryName }, ClientType = new Data { Id = o.ClientType, Name = o.ClientTypeNavigation.Type } }).ToListAsync();
            return output;
        }
    }
}
