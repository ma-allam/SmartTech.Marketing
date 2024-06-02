using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using SmartTech.Marketing.Application.Contract;

public class GlobalAuthorizationControlMiddleware
{
    private readonly RequestDelegate _next;
    private readonly IServiceProvider _serviceProvider;

    public GlobalAuthorizationControlMiddleware(RequestDelegate next, IServiceProvider serviceProvider)
    {
        _next = next;
        _serviceProvider = serviceProvider;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var dbContext = scope.ServiceProvider.GetRequiredService<IDataBaseService>();
            bool isAuthorizationRequired = false;

            var sysparam = (await dbContext.SysParam
            .Where(s => s.Id == (int)SysParamEnum.IsAuthorizationRequired)
            .FirstOrDefaultAsync());

            if (sysparam != null)
            {
                isAuthorizationRequired = sysparam.ParamValue;
            }

            if (!isAuthorizationRequired)
            {
                var endpoint = context.GetEndpoint();
                if (endpoint != null)
                {
                    var metadataCollection = new EndpointMetadataCollection(
                        endpoint.Metadata.Where(m => !(m is AuthorizeAttribute)).ToList()
                    );

                    context.SetEndpoint(new Endpoint(
                        endpoint.RequestDelegate,
                        metadataCollection,
                        endpoint.DisplayName
                    ));
                }
            }
        }

        await _next(context);
    }
    public enum SysParamEnum : int
    {
        IsAuthorizationRequired = 1
    }
}
