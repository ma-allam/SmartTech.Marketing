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

namespace SmartTech.Marketing.Application.Business.User
{
    public class LoginHandler : IRequestHandler<LoginHandlerInput, LoginHandlerOutput>
    {
        private readonly IDataBaseService _databaseService;
        private readonly ILogger<LoginHandler> _logger;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;
        public LoginHandler(ILogger<LoginHandler> logger, IDataBaseService databaseService, UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IConfiguration configuration)
        {
            _logger = logger;
            _databaseService = databaseService;
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }
        public async Task<LoginHandlerOutput> Handle(LoginHandlerInput request, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Handling Login business logic");
            LoginHandlerOutput output = new LoginHandlerOutput(request.CorrelationId());
         

            var user = await _userManager.FindByNameAsync(request.Username);
            if (user == null) throw new WebApiException($"Account with {request.Username} User Name Not Found, Please contact system administrator");
            if (await _userManager.CheckPasswordAsync(user, request.Password))
            {
                var token = GenerateJwtToken(user);
                output.Token=token;
            }
            else
            {
                throw new WebApiException("Please Check  Password");
                //throw new InvalidOperationException();
            }
            return output;
        }
        public string GenerateJwtToken(ApplicationUser user)
        {
            ActiveContext activeContext = new ActiveContext { UserName=user.UserName};
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
