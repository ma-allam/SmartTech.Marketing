using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using SmartTech.Marketing.Core.AppSetting;

namespace SmartTech.Marketing.Core.Crypto
{
    public class EncryptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly EncryptionAppSetting _encryptionOption;
        private readonly IOptions<AuthenticationAppSetting> _jwtOptions;
        private IAesCryptoHelper _aesCryptoHelper;

        public EncryptionMiddleware(RequestDelegate next, IOptions<AuthenticationAppSetting> jwtOptions, IOptions<EncryptionAppSetting> encryptionOptions)
        {
            _next = next;
            _encryptionOption = encryptionOptions.Value;
            _jwtOptions = jwtOptions;
        }

        public async Task Invoke(HttpContext httpContext, IAesCryptoHelper aesCryptoHelper, ILogger logger)
        {
            _aesCryptoHelper = aesCryptoHelper;

            if (_encryptionOption.EnableEncryption)
            {
                if (httpContext.Request.Headers.ContainsKey("ClientId"))
                {
                    RsaCryptoHelper cryptoHelper = new RsaCryptoHelper(_encryptionOption);
                    var encryptedKey = httpContext.Request.Headers["ClientId"].ToString();
                    var decryptedKey = cryptoHelper.Decrypt(encryptedKey);
                    _aesCryptoHelper.Key = decryptedKey;
                    _aesCryptoHelper.TokenKey = _jwtOptions.Value.Jwt.secretKey;
                    if (_encryptionOption.EnableJwtEncryption && httpContext.Request.Headers.ContainsKey("Authorization"))
                    {
                        var encryptedToken = httpContext.Request.Headers["Authorization"].ToString();
                        encryptedToken = encryptedToken.Replace("Bearer ", "");
                        var decryptedToken = _aesCryptoHelper.DecryptToken(encryptedToken);
                        httpContext.Request.Headers["Authorization"] = "Bearer " + decryptedToken;
                    }

                    if (!string.IsNullOrEmpty(_aesCryptoHelper.Key))
                    {
                        if (_encryptionOption.EnableResponseBodyEncryption)
                            httpContext.Response.Body = _aesCryptoHelper.EncryptStream(httpContext.Response.Body);

                        if (_encryptionOption.EnableRequestBodyEncryption && httpContext.Request.ContentLength > 0)
                        {
                            httpContext.Request.Body = _aesCryptoHelper.DecryptStream(httpContext.Request.Body);
                        }

                        if (_encryptionOption.EnableRequestQueryStringEncryption &&
                            httpContext.Request.QueryString.HasValue)
                        {
                            string decryptedString = _aesCryptoHelper.DecryptString(httpContext.Request.QueryString.Value!.Substring(1));
                            httpContext.Request.QueryString = new QueryString($"?{decryptedString}");
                        }
                    }
                }
            }

            string foo = "EncryptionMiddleware Time taken: ";
            logger.LogInformation(foo);
            await _next(httpContext);

        }
    }
}
