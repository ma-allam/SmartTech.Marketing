using SmartTech.Marketing.Core.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using SmartTech.Marketing.Application.Contract;
using SmartTech.Marketing.Core.Auth.JWT;
using SmartTech.Marketing.Core.Auth.User;
using SmartTech.Marketing.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace SmartTech.Marketing.Application.Business.User.Command
{
    public class LoginHandler : IRequestHandler<LoginHandlerInput, LoginHandlerOutput>
    {
        private readonly IDataBaseService _databaseService;
        private readonly ILogger<LoginHandler> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public LoginHandler(ILogger<LoginHandler> logger, IDataBaseService databaseService, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration, IHttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _databaseService = databaseService;
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<LoginHandlerOutput> Handle(LoginHandlerInput request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling Login business logic");
            LoginHandlerOutput output = new LoginHandlerOutput(request.CorrelationId());

            var user = await _userManager.FindByNameAsync(request.Username);
            if (user == null) throw new WebApiException($"Account with {request.Username} User Name Not Found, Please contact system administrator");

            var result = await _signInManager.PasswordSignInAsync(user, request.Password, isPersistent: false, lockoutOnFailure: true);

            if (result.Succeeded)
            {
                var token = await GenerateJwtToken(user);
                output.Token = token;

                // Save login history
                var loginTime = DateTime.UtcNow.ToString("o");
                var ipAddress = _httpContextAccessor.HttpContext.Connection.RemoteIpAddress?.ToString();

                var loginToken = new IdentityUserToken<string>
                {
                    UserId = user.Id,
                    LoginProvider = "LoginTracker",
                    Name = "LoginEvent",
                    Value = $"{loginTime};{ipAddress}"
                };

                await _userManager.SetAuthenticationTokenAsync(user, loginToken.LoginProvider, loginToken.Name, loginToken.Value);
            }
            else if (result.IsLockedOut)
            {
                throw new WebApiException("Account locked out due to multiple failed login attempts. Please try again later.");
            }
            else
            {
                throw new WebApiException("Please Check Password");
            }

            return output;
        }

        public async Task<string> GenerateJwtToken(ApplicationUser user)
        {
            var clientid = await _databaseService.Client.Where(o => o.UserId == user.Id).Select(o => o.Id).FirstOrDefaultAsync();
            ActiveContext activeContext = new ActiveContext { UserName = user.UserName, ClientId = clientid };
            var data = JsonConvert.SerializeObject(activeContext, Formatting.None,
                         new JsonSerializerSettings()
                         {
                             ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                         });
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JWTClaims.ActiveContext, data)
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
