using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Prometheus;
using SmartTech.Marketing.Application.Contract;
using SmartTech.Marketing.Core.AppSetting;
using SmartTech.Marketing.Core.Exceptions;
using SmartTech.Marketing.Domain.Entities;
using SmartTech.Marketing.Persistence.Context;
using SmartTech.Marketing.WebApi.DependencyInjection;
using StackExchange.Redis;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Security.Claims;
using System.Text;
using Serilog;


var builder = WebApplication.CreateBuilder(args);
// Configure Serilog
builder.Host.UseSerilog((context, services, configuration) => configuration
    .ReadFrom.Configuration(context.Configuration)
    .ReadFrom.Services(services)
    .Enrich.FromLogContext());
builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.Limits.MaxConcurrentConnections = 1000;
    serverOptions.Limits.MaxConcurrentUpgradedConnections = 1000;
});
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();


builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
    .AddEntityFrameworkStores<DatabaseService>()
    .AddDefaultTokenProviders();

builder.Services.AddApiVersioning(o =>
{

    o.AssumeDefaultVersionWhenUnspecified = true;
    o.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
    o.ReportApiVersions = true;
    o.ApiVersionReader = ApiVersionReader.Combine(
        new MediaTypeApiVersionReader("ver"));
});
builder.Services.AddVersionedApiExplorer(
    options =>
    {

        options.GroupNameFormat = "'v'VVV";
        options.SubstituteApiVersionInUrl = true;
    });
builder.Services.AddWebApilayerDependencyInjection(builder.Configuration);



// Register the custom middleware
string basePath = SettingsDependancyInjection.ServiceSettings.BaseServicePath;

var app = builder.Build();
app.ConfigureExceptionHandler(app.Environment);
// Configure the HTTP request pipeline.
//builder.Services.AddEndpointsApiExplorer();
var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();



if (SettingsDependancyInjection.ServiceSettings.EnableSwagger)
{
    app.UseSwagger(c => { c.RouteTemplate = basePath + "/swagger/{documentName}/swagger.json"; });
    app.UseSwaggerUI(options =>
    {
        options.RoutePrefix = $"{basePath}/swagger";
        foreach (var description in provider.ApiVersionDescriptions)
        {
            options.SwaggerEndpoint($"/{basePath}/swagger/{description.GroupName}/swagger.json",
                description.GroupName.ToUpperInvariant());
        }
    });
}
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseCors("CorsPolicy");
app.UseRouting();
app.UseMiddleware<GlobalAuthorizationControlMiddleware>();

app.UseAuthentication();

app.UseAuthorization();
if (SettingsDependancyInjection.RedisSettings.Enable)
{
    app.UseMiddleware<RedisCacheMiddleware>();
}

app.MapControllers();
app.UseMetricServer();

app.UseEndpoints(endpoints =>
{
    endpoints.MapMetrics($"/{basePath}/metrics");
});

app.MapHealthChecks($"/{basePath}/health/live", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});
app.MapHealthChecks($"/{basePath}/", new HealthCheckOptions
{
    AllowCachingResponses = false,
    ResultStatusCodes =
                {
                    [HealthStatus.Healthy] = StatusCodes.Status200OK,
                    [HealthStatus.Degraded] = StatusCodes.Status200OK,
                    [HealthStatus.Unhealthy] = StatusCodes.Status503ServiceUnavailable
                }
});
app.UseHealthChecksPrometheusExporter($"/{basePath}/health/metrics");
try
{
    Log.Information("Starting web application");
    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application start-up failed");
}
finally
{
    Log.CloseAndFlush();
}
//app.Run();
