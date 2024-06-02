using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SmartTech.Marketing.Application.Contract;
using SmartTech.Marketing.Core.Exceptions;
using SmartTech.Marketing.Domain.Entities;

namespace SmartTech.Marketing.Application.Business.Clients.Command
{
    public class UpdateClientHandler : IRequestHandler<UpdateClientHandlerInput, UpdateClientHandlerOutput>
    {
        private readonly IDataBaseService _databaseService;
        private readonly ILogger<UpdateClientHandler> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        public UpdateClientHandler(ILogger<UpdateClientHandler> logger, IDataBaseService databaseService, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _databaseService = databaseService;
            _userManager = userManager;
        }
        public async Task<UpdateClientHandlerOutput> Handle(UpdateClientHandlerInput request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling UpdateClient business logic");
            UpdateClientHandlerOutput output = new UpdateClientHandlerOutput(request.CorrelationId());
            using (var transaction = await _databaseService.BeginTransactionAsync(cancellationToken))
            {
                try
                {
                    var client = await _databaseService.Client
                        .FirstOrDefaultAsync(c => c.Id == request.ClientId, cancellationToken);

                    if (client == null)
                    {
                        throw new WebApiException("Client not found");
                    }

                    var user = await _userManager.FindByIdAsync(client.UserId);

                    if (user == null)
                    {
                        throw new WebApiException( "User associated with the client not found");
                    }

                    // Update only the provided user details
                    if (!string.IsNullOrWhiteSpace(request.PhoneNumber))
                    {
                        user.PhoneNumber = request.PhoneNumber;
                    }
                    if (!string.IsNullOrWhiteSpace(request.UserName))
                    {
                        user.UserName = request.UserName;
                    }
                    if (!string.IsNullOrWhiteSpace(request.Email))
                    {
                        user.Email = request.Email;
                    }
                    var updateResult = await _userManager.UpdateAsync(user);

                    if (!updateResult.Succeeded)
                    {
                        throw new WebApiException(WebApiExceptionSource.DynamicMessage, "User update failed: " + string.Join(", ", updateResult.Errors.Select(e => e.Description)));
                    }

                    // Update only the provided client details
                    if (!string.IsNullOrWhiteSpace(request.Name))
                    {
                        client.Name = request.Name;
                    }
                    if (!string.IsNullOrWhiteSpace(request.PhoneNumber))
                    {
                        client.PhoneNumber = request.PhoneNumber;
                    }
                    if (!string.IsNullOrWhiteSpace(request.Email))
                    {
                        client.Email = user.Email;
                    }
                    if (request.SelectedClientType!=0)
                    {
                        client.ClientType = request.SelectedClientType;
                    }
                    if (request.SelectedCountry != 0)
                    {
                        client.CountryId = request.SelectedCountry;
                    }

                    _databaseService.Client.Update(client);
                    await _databaseService.DBSaveChangesAsync(cancellationToken);


                    output.Message = "Client updated successfully.";
                    await transaction.CommitAsync(cancellationToken);
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync(cancellationToken);
                    _logger.LogError(ex, "Error occurred during client update");
                    throw new WebApiException(WebApiExceptionSource.DynamicMessage, ex.Message);
                }
            }
            return output;
        }
    }
}
