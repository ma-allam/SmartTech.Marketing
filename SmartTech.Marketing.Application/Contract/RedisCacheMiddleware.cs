using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.AspNetCore.Mvc.Controllers;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using SmartTech.Marketing.Application.Contract;

public class RedisCacheMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IDistributedCache _cache;

    public RedisCacheMiddleware(RequestDelegate next, IDistributedCache cache)
    {
        _next = next;
        _cache = cache;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        // Check if the request method is GET
        if (context.Request.Method != HttpMethods.Get)
        {
            await _next(context);
            return;
        }

        // Check if the endpoint is marked with NoCacheAttribute
        var endpoint = context.GetEndpoint();
        if (endpoint != null)
        {
            var actionDescriptor = endpoint.Metadata.GetMetadata<ControllerActionDescriptor>();
            if (actionDescriptor != null)
            {
                var hasNoCacheAttribute = actionDescriptor.MethodInfo.GetCustomAttributes(typeof(NoCacheAttribute), false).Length > 0;
                if (hasNoCacheAttribute)
                {
                    await _next(context);
                    return;
                }
            }
        }

        // Generate a cache key based on the request path and query string
        var cacheKey = GenerateCacheKey(context.Request);

        // Try to get the cached response
        var cachedResponse = await _cache.GetStringAsync(cacheKey);
        if (!string.IsNullOrEmpty(cachedResponse))
        {
            // If a cached response exists, return it
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

            // Cache the response if the status code is 200 OK
            if (context.Response.StatusCode == 200)
            {
                context.Response.Body.Seek(0, SeekOrigin.Begin);
                var responseBody = await new StreamReader(context.Response.Body).ReadToEndAsync();
                await _cache.SetStringAsync(cacheKey, responseBody, new DistributedCacheEntryOptions
                {
                    AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5)  // Set cache duration
                });
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
