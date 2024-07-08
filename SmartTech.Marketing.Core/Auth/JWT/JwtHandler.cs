using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.AspNetCore.Http;

using Newtonsoft.Json;
using SmartTech.Marketing.Core.AppSetting;
using SmartTech.Marketing.Core.Crypto;
using Microsoft.IdentityModel.JsonWebTokens;
using SmartTech.Marketing.Core.Auth.User;

namespace SmartTech.Marketing.Core.Auth.JWT
{
    public class JwtHandler : IJwtHandler
    {

        private readonly JwtSecurityTokenHandler _jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
        private readonly AuthenticationAppSetting _authenticationOption;
        private readonly EncryptionAppSetting _encryptionOption;
        private readonly IAesCryptoHelper _aesCryptoHelper;

        private readonly IHttpContextAccessor _contextAccessor;

        public JwtHandler(IOptions<AuthenticationAppSetting> authenticationOption, IAesCryptoHelper aesCryptoHelper, IOptions<EncryptionAppSetting> encryptionOption, IHttpContextAccessor contextAccessor)
        {
            _authenticationOption = authenticationOption.Value;
            _aesCryptoHelper = aesCryptoHelper;

            _encryptionOption = encryptionOption.Value;
            _contextAccessor = contextAccessor;
        }


        public string? GetClaim(string token, string claimType)
        {

            var securityToken = _jwtSecurityTokenHandler.ReadToken(token) as JwtSecurityToken;

            var stringClaimValue = securityToken?.Claims.First(claim => claim.Type == claimType).Value;
            return stringClaimValue;
        }
        public JsonWebToken Create(List<Claim> PreClaims)
        {

            var issuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_authenticationOption.Jwt.secretKey));
            var signingCredentials = new SigningCredentials(issuerSigningKey, SecurityAlgorithms.HmacSha384);
            var nowUtc = DateTime.UtcNow;
            var exp = nowUtc.AddMinutes(_authenticationOption.Jwt.expiryMinutes);
            var jwt = new JwtSecurityToken(
                issuer: _authenticationOption.Jwt.issuer,
                audience: _authenticationOption.Jwt.issuer,
                claims: PreClaims,
                notBefore: nowUtc,
                expires: exp,
                signingCredentials: signingCredentials
                );

            var token = _jwtSecurityTokenHandler.WriteToken(jwt);
            string encryptedToken = token;
            if (_encryptionOption.EnableEncryption && _encryptionOption.EnableJwtEncryption)
            {
                encryptedToken = _aesCryptoHelper.EncryptToken(token);
            }

            var responseToken = new JsonWebToken
            {
                Token = encryptedToken,
                ValidTo = jwt.ValidTo,
                Expires = exp.Ticks
            };


            return responseToken;




        }
        public JsonWebToken Create(ActiveContext activeContext, bool UsingCache = true)
        {

            var issuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_authenticationOption.Jwt.secretKey));
            var signingCredentials = new SigningCredentials(issuerSigningKey, SecurityAlgorithms.HmacSha384);
            var nowUtc = DateTime.UtcNow;
            var exp = nowUtc.AddMinutes(_authenticationOption.Jwt.expiryMinutes);
            var subject = new List<Claim>
            {
                new Claim(ClaimTypes.Email, activeContext.EmailAddress.ToString(CultureInfo.InvariantCulture)),
                new Claim(ClaimTypes.Name, activeContext.FullName.ToString(CultureInfo.InvariantCulture)),
                //new Claim(JWTClaims.SeUserId, activeContext.UserData.SeUserId.ToString(CultureInfo.InvariantCulture))
            };
            subject.AddRange(activeContext!.Permissions.Select(item => new Claim(ClaimTypes.Role, item)));


            var jwt = new JwtSecurityToken(
                issuer: _authenticationOption.Jwt.issuer,
                audience: _authenticationOption.Jwt.issuer,
                claims: subject,
                notBefore: nowUtc,
                expires: exp,
                signingCredentials: signingCredentials
                );

            var token = _jwtSecurityTokenHandler.WriteToken(jwt);
            string encryptedToken = token;
            if (_encryptionOption.EnableEncryption && _encryptionOption.EnableJwtEncryption)
            {
                encryptedToken = _aesCryptoHelper.EncryptToken(token);
            }

            var responseToken = new JsonWebToken
            {
                Token = encryptedToken,
                ValidTo = jwt.ValidTo,
                Expires = exp.Ticks
            };

            activeContext!.Token = responseToken;
            //if (_cacheService.IsEnabled && UsingCache)
            //{
            //    _cacheService.Set(activeContext.ActiveContextId.ToString(), activeContext, _authenticationOption.Jwt.expiryMinutes);
            //}


            return responseToken;




        }

        public JsonWebToken CleanCreate(ActiveContext activeContext)
        {

            var issuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_authenticationOption.Jwt.secretKey));
            var signingCredentials = new SigningCredentials(issuerSigningKey, SecurityAlgorithms.HmacSha384);
            var nowUtc = DateTime.UtcNow;
            var exp = nowUtc.AddMinutes(_authenticationOption.Jwt.expiryMinutes);
            var subject = new List<Claim> { new Claim(ClaimTypes.Email, activeContext.EmailAddress.ToString(CultureInfo.InvariantCulture)) };


            var jwt = new JwtSecurityToken(
                issuer: _authenticationOption.Jwt.issuer,
                audience: _authenticationOption.Jwt.issuer,
                claims: subject,
                notBefore: nowUtc,
                expires: exp,
                signingCredentials: signingCredentials
                );

            var token = _jwtSecurityTokenHandler.WriteToken(jwt);
            string encryptedToken = token;
            if (_encryptionOption.EnableEncryption && _encryptionOption.EnableJwtEncryption)
            {
                encryptedToken = _aesCryptoHelper.EncryptToken(token);
            }

            var responseToken = new JsonWebToken
            {
                Token = encryptedToken,
                ValidTo = jwt.ValidTo,
                Expires = exp.Ticks
            };


            return responseToken;




        }
        public TokenContext CreateWithRefreshToken(ActiveContext activeContext)
        {
            TokenContext output = new TokenContext();
            if (activeContext.ActiveContextId == Guid.Empty)
                activeContext.ActiveContextId = Guid.NewGuid();

            var issuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_authenticationOption.Jwt.secretKey));
            var signingCredentials = new SigningCredentials(issuerSigningKey, SecurityAlgorithms.HmacSha384);
            var nowUtc = DateTime.UtcNow;
            var exp = nowUtc.AddMinutes(_authenticationOption.Jwt.expiryMinutes);

            var data = JsonConvert.SerializeObject(activeContext, Formatting.None,
                         new JsonSerializerSettings()
                         {
                             ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                         });
            var subject = new List<Claim>
            {
                new Claim(ClaimTypes.Email, activeContext.EmailAddress.ToString(CultureInfo.InvariantCulture)),
                new Claim(ClaimTypes.Name, activeContext.FullName.ToString(CultureInfo.InvariantCulture)),
                new Claim(JWTClaims.ActiveContext, data)
            };
            subject.AddRange(activeContext.Permissions.Select(item => new Claim(ClaimTypes.Role, item)).ToList());


            var jwt = new JwtSecurityToken(
                issuer: _authenticationOption.Jwt.issuer,
                audience: _authenticationOption.Jwt.issuer,
                claims: subject,
                notBefore: nowUtc,
                expires: exp,
                signingCredentials: signingCredentials
                );

            var token = _jwtSecurityTokenHandler.WriteToken(jwt);
            string encryptedToken = token;
            if (_encryptionOption.EnableEncryption && _encryptionOption.EnableJwtEncryption)
            {
                encryptedToken = _aesCryptoHelper.EncryptToken(token);
            }

            var responseToken = new JsonWebToken
            {
                Token = encryptedToken,
                ValidTo = jwt.ValidTo,
                Expires = exp.Ticks
            };

            activeContext!.Token = responseToken;
            output.AccessToken = responseToken;
            output.RefreshToken = CreateRefreshToken(activeContext);
            return output;
        }
        private string GetIpAddress()
        {
            return _contextAccessor.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
        }
        private JsonWebToken CreateRefreshToken(ActiveContext activeContext)
        {
            var ipAddress = GetIpAddress();

            var issuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_authenticationOption.Jwt.secretKey));
            var signingCredentials = new SigningCredentials(issuerSigningKey, SecurityAlgorithms.HmacSha384);
            var nowUtc = DateTime.UtcNow;
            var exp = nowUtc.AddMinutes(_authenticationOption.Jwt.RefreshExpiryMinutes);
            var data = JsonConvert.SerializeObject(activeContext, Formatting.None,
                         new JsonSerializerSettings()
                         {
                             ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                         });
            var subject = new List<Claim>
            {
                new Claim(ClaimTypes.Email, activeContext.EmailAddress.ToString(CultureInfo.InvariantCulture)),
                new Claim(ClaimTypes.Name, activeContext.FullName.ToString(CultureInfo.InvariantCulture)),
                new Claim(JWTClaims.ActiveContext, data)
            };
            subject.AddRange(activeContext.Permissions.Select(item => new Claim(ClaimTypes.Role, item)).ToList());


            var jwt = new JwtSecurityToken(
                issuer: _authenticationOption.Jwt.issuer,
                audience: _authenticationOption.Jwt.issuer,
                claims: subject,
                notBefore: nowUtc,
                expires: exp,
                signingCredentials: signingCredentials
                );

            var token = _jwtSecurityTokenHandler.WriteToken(jwt);
            string encryptedToken = token;
            if (_encryptionOption.EnableEncryption && _encryptionOption.EnableJwtEncryption)
            {
                encryptedToken = _aesCryptoHelper.EncryptToken(token);
            }

            var responseToken = new JsonWebToken
            {
                Token = encryptedToken,
                ValidTo = jwt.ValidTo,
                Expires = exp.Ticks
            };

            return responseToken;
        }
        public JwtSecurityToken ValidateJwtToken(string token)
        {
            if (token == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_authenticationOption.Jwt.secretKey);
            try
            {
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    // set clockskew to zero so tokens expire exactly at token expiration time (instead of 5 minutes later)
                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                return (JwtSecurityToken)validatedToken;

            }
            catch
            {
                // return null if validation fails
                return null;
            }
        }
    }
}
