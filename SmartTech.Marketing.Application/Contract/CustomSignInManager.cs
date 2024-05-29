using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SmartTech.Marketing.Domain.Entities;
using System;
using System.Threading.Tasks;

public class CustomSignInManager : SignInManager<ApplicationUser>
{
    private readonly IHttpContextAccessor _contextAccessor;
    public CustomSignInManager(
        UserManager<ApplicationUser> userManager,
        IHttpContextAccessor contextAccessor,
        IUserClaimsPrincipalFactory<ApplicationUser> claimsFactory,
        IOptions<IdentityOptions> optionsAccessor,
        ILogger<SignInManager<ApplicationUser>> logger,
        IAuthenticationSchemeProvider schemes,
        IUserConfirmation<ApplicationUser> confirmation)
        : base(userManager, contextAccessor, claimsFactory, optionsAccessor, logger, schemes, confirmation)
    {
        _contextAccessor = contextAccessor;
    }

    public override async Task<SignInResult> PasswordSignInAsync(string userName, string password, bool isPersistent, bool lockoutOnFailure)
    {
        var result = await base.PasswordSignInAsync(userName, password, isPersistent, lockoutOnFailure);

        if (result.Succeeded)
        {
            var user = await UserManager.FindByNameAsync(userName);
            if (user != null)
            {
                var loginTime = DateTime.UtcNow.ToString("o");
                var ipAddress = _contextAccessor.HttpContext.Connection.RemoteIpAddress?.ToString();

                var token = new IdentityUserToken<string>
                {
                    UserId = user.Id,
                    LoginProvider = "LoginTracker",
                    Name = "LoginEvent",
                    Value = $"{loginTime};{ipAddress}"
                };

                await UserManager.SetAuthenticationTokenAsync(user, token.LoginProvider, token.Name, token.Value);
            }
        }

        return result;
    }
}
