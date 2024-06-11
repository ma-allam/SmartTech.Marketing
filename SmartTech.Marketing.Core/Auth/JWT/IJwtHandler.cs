using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using SmartTech.Marketing.Core.Auth.User;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace SmartTech.Marketing.Core.Auth.JWT
{
    public interface IJwtHandler
    {
        JwtSecurityToken ValidateJwtToken(string token);
        JsonWebToken Create(List<Claim> PreClaims);
        TokenContext CreateWithRefreshToken(ActiveContext activeContext);
        JsonWebToken CleanCreate(ActiveContext activeContext);
        string? GetClaim(string token, string claimType);
        JsonWebToken Create(ActiveContext activeContext, bool UsingCache = true);
    }
}
