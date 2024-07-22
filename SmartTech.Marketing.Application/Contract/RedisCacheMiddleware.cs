using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using SmartTech.Marketing.Core.Cache;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace SmartTech.Marketing.Application.Contract
{
    public class RedisCacheMiddleware
    {
        private readonly RequestDelegate _next;

        public RedisCacheMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Check if the request method is GET
            if (context.Request.Method != HttpMethods.Get)
            {
                await _next(context);
                return;
            }
            var endpoint = context.GetEndpoint();
            if (endpoint?.Metadata?.GetMetadata<NoCacheAttribute>() != null)
            {
                await _next(context);
                return;
            }
            var cacheService = context.RequestServices.GetRequiredService<CacheService>();
            var cacheKey = GenerateCacheKey(context.Request);

            // Try to get the cached response
            var cachedResponse = await cacheService.GetCacheAsync(cacheKey);
            if (!string.IsNullOrEmpty(cachedResponse))
            {
                context.Response.ContentType = "application/json";
                await context.Response.WriteAsync(cachedResponse);
                return;
            }

            // Capture the response body
            var originalBodyStream = context.Response.Body;
            using (var responseBodyStream = new MemoryStream())
            {
                context.Response.Body = responseBodyStream;

                await _next(context);

                // Get the EntityNames header from the response
                var entityNamesHeader = context.Response.Headers["EntityNames"].ToString();
                var entityNames = entityNamesHeader.Split(',').Select(name => name.Trim()).ToArray();

                // Cache the response if the status code is 200 OK
                if (context.Response.StatusCode == 200 && entityNames.Any())
                {
                    context.Response.Body.Seek(0, SeekOrigin.Begin);
                    var responseBody = await new StreamReader(context.Response.Body).ReadToEndAsync();
                    await cacheService.SetCacheAsync(entityNames, cacheKey, responseBody);
                    context.Response.Body.Seek(0, SeekOrigin.Begin);
                }

                // Copy the response body back to the original stream
                await responseBodyStream.CopyToAsync(originalBodyStream);
                context.Response.Body = originalBodyStream;
            }
        }

        private string GenerateCacheKey(HttpRequest request)
        {
            var keyBuilder = new StringBuilder();
            keyBuilder.Append($"{request.Path}");

            foreach (var (key, value) in request.Query.OrderBy(x => x.Key))
            {
                keyBuilder.Append($"|{key}-{value}");
            }

            using (var sha256 = SHA256.Create())
            {
                var hash = sha256.ComputeHash(Encoding.UTF8.GetBytes(keyBuilder.ToString()));
                return Convert.ToBase64String(hash);
            }
        }
    }
}
