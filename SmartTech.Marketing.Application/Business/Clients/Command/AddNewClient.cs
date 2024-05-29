using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SmartTech.Marketing.Application.Contract;
using SmartTech.Marketing.Core.Exceptions;
using SmartTech.Marketing.Domain.Entities;

namespace SmartTech.Marketing.Application.Business.Clients.Command
{
    public class AddNewClientHandler : IRequestHandler<AddNewClientHandlerInput, AddNewClientHandlerOutput>
    {
        private readonly IDataBaseService _databaseService;
        private readonly ILogger<AddNewClientHandler> _logger;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public AddNewClientHandler(ILogger<AddNewClientHandler> logger, IDataBaseService databaseService, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _logger = logger;
            _databaseService = databaseService;
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task<AddNewClientHandlerOutput> Handle(AddNewClientHandlerInput request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling AddNewClient business logic");
            AddNewClientHandlerOutput output = new AddNewClientHandlerOutput(request.CorrelationId());
            using (var transaction = await _databaseService.BeginTransactionAsync(cancellationToken))
            {
                try
                {
                    var user = new ApplicationUser
                    {
                        PhoneNumber = request.PhoneNumber,
                        UserName = request.Username,
                        Email = request.Email,
                    };

                    var result = await _userManager.CreateAsync(user, request.Password);

                    if (!result.Succeeded)
                    {
                        throw new WebApiException(WebApiExceptionSource.DynamicMessage, "User creation failed: " + string.Join(", ", result.Errors.Select(e => e.Description)));
                    }

                    var client = new Client
                    {
                        Name = request.Name,
                        PhoneNumber = request.PhoneNumber,
                        Email = user.Email,
                        ClientType = request.SelectedClientType,
                        CountryId = request.SelectedCountry,
                        UserId = user.Id
                    };

                    _databaseService.Client.Add(client);
                    await _databaseService.DBSaveChangesAsync(cancellationToken);

                    await transaction.CommitAsync(cancellationToken);
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync(cancellationToken);
                    _logger.LogError(ex, "Error occurred during user registration");
                    throw new WebApiException(WebApiExceptionSource.DynamicMessage, ex.Message);
                }
            }
            return output;
        }
    }
}
