using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SmartTech.Marketing.Application.Contract;

namespace SmartTech.Marketing.Application.Business.Clients.Query
{
    public class GetClientByIdHandler : IRequestHandler<GetClientByIdHandlerInput, GetClientByIdHandlerOutput>
    {
        private readonly IDataBaseService _databaseService;
        private readonly ILogger<GetClientByIdHandler> _logger;
        public GetClientByIdHandler(ILogger<GetClientByIdHandler> logger, IDataBaseService databaseService)
        {
            _logger = logger;
            _databaseService = databaseService;
        }
        public async Task<GetClientByIdHandlerOutput> Handle(GetClientByIdHandlerInput request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling GetClientById business logic");
            GetClientByIdHandlerOutput output = new GetClientByIdHandlerOutput(request.CorrelationId());
            output.Client = await _databaseService.Client.Where(o => o.Id == request.ClientId).Select(o => new ClientData
            {
                ClientId = o.Id,
                Name = o.Name,
                Email = o.Email,
                Username = o.User.UserName,
                PhoneNumber = o.PhoneNumber,
                Country = new Data { Id = o.CountryId, Name = o.Country.CountryName },
                ClientType = new Data { Id = o.ClientType, Name = o.ClientTypeNavigation.Type }
            }).FirstOrDefaultAsync();
            return output;
        }
    }
}
