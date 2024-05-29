using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SmartTech.Marketing.Application.Contract;

namespace SmartTech.Marketing.Application.Business.Clients.Query
{
    public class SearchClientsHandler : IRequestHandler<SearchClientsHandlerInput, SearchClientsHandlerOutput>
    {
        private readonly IDataBaseService _databaseService;
        private readonly ILogger<SearchClientsHandler> _logger;
        public SearchClientsHandler(ILogger<SearchClientsHandler> logger, IDataBaseService databaseService)
        {
            _logger = logger;
            _databaseService = databaseService;
        }
        public async Task<SearchClientsHandlerOutput> Handle(SearchClientsHandlerInput request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling SearchClients business logic");
            SearchClientsHandlerOutput output = new SearchClientsHandlerOutput(request.CorrelationId());
            var query = _databaseService.Client.AsQueryable();

            if (!string.IsNullOrEmpty(request.Name))
            {
                query = query.Where(c => c.Name.ToLower().Contains(request.Name.ToLower()));
            }

            if (!string.IsNullOrEmpty(request.PhoneNumber))
            {
                query = query.Where(c => c.PhoneNumber.Contains(request.PhoneNumber));
            }

            if (!string.IsNullOrEmpty(request.Email))
            {
                query = query.Where(c => c.Email.ToLower().Contains(request.Email.ToLower()));
            }

            if (request.ClientType.HasValue)
            {
                query = query.Where(c => c.ClientType == request.ClientType.Value);
            }

            if (request.CountryId.HasValue)
            {
                query = query.Where(c => c.CountryId == request.CountryId.Value);
            }

            output.Clients = await query.Select(o => new ClientData
            {
                ClientId = o.Id,
                Name = o.Name,
                Email = o.Email,
                Username = o.User.UserName,
                PhoneNumber = o.PhoneNumber,
                Country = new Data { Id = o.CountryId, Name = o.Country.CountryName },
                ClientType = new Data { Id = o.ClientType, Name = o.ClientTypeNavigation.Type }
            }).ToListAsync();
            return output;
        }
    }
}
