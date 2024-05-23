using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace SmartTech.Marketing.Core.Extensions
{
    public static class ClaimsPrincipalExtensions
    {

        public static decimal UserId(this ClaimsPrincipal user)
        {
            var userId = decimal.Parse(user?.Identity?.Name ?? "0");
            return userId;
        }

        public static string? GetClaim(this ClaimsPrincipal user, string claimstr)
        {
            var stringClaimValueObject = user?.Claims.FirstOrDefault(claim => claim.Type == claimstr);
            var stringClaimValue = stringClaimValueObject?.Value;
            return stringClaimValue;
        }
        public static T? GetClaim<T>(this ClaimsPrincipal user, string claimstr)
        {
            var stringClaimValueObject = user?.Claims.FirstOrDefault(claim => claim.Type == claimstr);
            if (stringClaimValueObject == null)
                return default;
            var stringClaimValue = stringClaimValueObject.Value;
            if (!string.IsNullOrEmpty(stringClaimValue))
            {

                return (T)Convert.ChangeType(stringClaimValue, typeof(T));
            }
            return default;
        }
        public static decimal UserId(this IHttpContextAccessor context)
        {
            return context?.HttpContext?.User?.UserId() ?? 0;
        }

        public static string GetLanguage(this IHttpContextAccessor context)
        {
            if (context?.HttpContext != null)
            {
                var headerValue = context.HttpContext.Request.Headers["Accept-Language"];
                if (headerValue.Any() == false) return "en";
                return headerValue.ToString();

            }
            return "en-US";
        }
        public static bool IsEnglish(this IHttpContextAccessor context)
        {
            if (context?.HttpContext != null)
            {
                if (context.GetLanguage().ToLower().Contains("en"))
                    return true;
                else
                {
                    return false;
                }

            }
            return true;
        }
    }
}
