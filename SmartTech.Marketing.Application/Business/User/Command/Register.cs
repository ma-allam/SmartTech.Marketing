using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using SmartTech.Marketing.Application.Contract;
using SmartTech.Marketing.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SmartTech.Marketing.Application.Business.User.Command
{
    public class RegisterHandler : IRequestHandler<RegisterHandlerInput, RegisterHandlerOutput>
    {
        private readonly IDataBaseService _databaseService;
        private readonly ILogger<LoginHandler> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;

        public RegisterHandler(ILogger<LoginHandler> logger, IDataBaseService databaseService, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration)
        {
            _logger = logger;
            _databaseService = databaseService;
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        public async Task<RegisterHandlerOutput> Handle(RegisterHandlerInput request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling Register business logic");
            RegisterHandlerOutput output = new RegisterHandlerOutput(request.CorrelationId());

            using (var transaction = await _databaseService.BeginTransactionAsync(cancellationToken))
            {
                try
                {
                    var user = new ApplicationUser
                    {
                        UserName = request.Username,
                        Email = request.Email,
                    };

                    var result = await _userManager.CreateAsync(user, request.Password);

                    if (!result.Succeeded)
                    {
                        throw new Exception("User creation failed: " + string.Join(", ", result.Errors.Select(e => e.Description)));
                    }

                    var client = new Client
                    {
                        Name = user.UserName,
                        Email = user.Email,
                        ClientType = 1,
                        CountryId = 1,
                        UserId = user.Id
                    };

                    _databaseService.Client.Add(client);
                    await _databaseService.DBSaveChangesAsync(cancellationToken);

                    await transaction.CommitAsync(cancellationToken);

                    // Optionally, sign in the user after registration
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    var token = GenerateJwtToken(user);
                    output.Token = token;
                }
                catch (Exception ex)
                {
                    await transaction.RollbackAsync(cancellationToken);
                    _logger.LogError(ex, "Error occurred during user registration");
                    throw;
                }
            }

            return output;
        }

        public string GenerateJwtToken(ApplicationUser user)
        {
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddMinutes(Convert.ToDouble(_configuration["Jwt:ExpiryMinutes"])),
                signingCredentials: creds);

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
