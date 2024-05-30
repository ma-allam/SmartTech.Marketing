using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SmartTech.Marketing.Application.Contract;
using SmartTech.Marketing.Core.Exceptions;
using SmartTech.Marketing.Domain.Entities;

namespace SmartTech.Marketing.Application.Business.User.Command
{
    public class AssignRoleHandler : IRequestHandler<AssignRoleHandlerInput, AssignRoleHandlerOutput>
    {
        private readonly IDataBaseService _databaseService;
        private readonly ILogger<AssignRoleHandler> _logger;
        private readonly UserManager<ApplicationUser> _userManager;

        public AssignRoleHandler(ILogger<AssignRoleHandler> logger, IDataBaseService databaseService, UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _databaseService = databaseService;
            _userManager = userManager;
        }
        public async Task<AssignRoleHandlerOutput> Handle(AssignRoleHandlerInput request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling AssignRole business logic");
            AssignRoleHandlerOutput output = new AssignRoleHandlerOutput(request.CorrelationId());


            var user = await _userManager.FindByNameAsync(request.Username);
            if (user == null)
            {
                throw new WebApiException("User not found");
            }

            var result = await _userManager.AddToRoleAsync(user, request.RoleName);
            if (result.Succeeded)
            {
                output.Message = "Role assigned successfully";
            }
            else
            {
                throw new WebApiException(WebApiExceptionSource.DynamicMessage, result.Errors.ToString());


            }

            return output;
        }
    }
}
