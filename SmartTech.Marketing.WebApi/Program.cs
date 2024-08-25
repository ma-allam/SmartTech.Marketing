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
using SmartTech.Marketing.WebApi.Swagger;
using StackExchange.Redis;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
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
// Configure Redis caching
//if (SettingsDependancyInjection.RedisSettings.Enable)
//{
//    builder.Services.AddStackExchangeRedisCache(options =>
//    {
//        options.Configuration = SettingsDependancyInjection.RedisSettings.OnPrem.Server;
//        options.InstanceName = SettingsDependancyInjection.RedisSettings.OnPrem.InstanceName;
//    });
//}
//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//})
//.AddJwtBearer(options =>
//{
//    options.TokenValidationParameters = new TokenValidationParameters
//    {
//        ValidateIssuer = true,
//        ValidateAudience = true,
//        ValidateLifetime = true,
//        ValidateIssuerSigningKey = true,
//        ValidIssuer = builder.Configuration["Jwt:Issuer"],
//        ValidAudience = builder.Configuration["Jwt:Audience"],
//        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])),
//        RoleClaimType = ClaimTypes.Role
//    };
//});
//builder.Services.Configure<IdentityOptions>(options =>
//{
//    // Password settings.
//    options.Password.RequireDigit = true;
//    options.Password.RequireLowercase = true;
//    options.Password.RequireNonAlphanumeric = true;
//    options.Password.RequireUppercase = true;
//    options.Password.RequiredLength = 6;
//    options.Password.RequiredUniqueChars = 1;

//    // Lockout settings.
//    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
//    options.Lockout.MaxFailedAccessAttempts = 5;
//    options.Lockout.AllowedForNewUsers = true;

//    // User settings.
//    options.User.AllowedUserNameCharacters =
//    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
//    options.User.RequireUniqueEmail = false;



//});
//builder.Services.ConfigureApplicationCookie(options =>
//{
//    // Cookie settings
//    options.Cookie.HttpOnly = true;
//    options.ExpireTimeSpan = TimeSpan.FromMinutes(60);

//    options.LoginPath = "/Account/Login";
//    options.AccessDeniedPath = "/Account/AccessDenied";
//    options.SlidingExpiration = true;
//});

//builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
//builder.Services.AddSwaggerGen();
//builder.Services.AddSwaggerGen(c =>
//{
//    c.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
//    {
//        Description = "Standard Authorization header using the Bearer scheme. Example: \"bearer {token}\"",
//        In = ParameterLocation.Header,
//        Name = "Authorization",
//        Type = SecuritySchemeType.ApiKey
//    });

//    c.OperationFilter<SecurityRequirementsOperationFilter>();
//    c.EnableAnnotations();
//    c.OperationFilter<SwaggerDefaultValues>();
//});
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


// Configure Redis caching
//if (SettingsDependancyInjection.RedisSettings.Enable)
//{

//    builder.Services.AddSingleton<IConnectionMultiplexer>(sp =>
//        {
//            var configuration = ConfigurationOptions.Parse(SettingsDependancyInjection.RedisSettings.OnPrem.Server, true);
//            return ConnectionMultiplexer.Connect(configuration);
//        });
//}
// Register the cache service
//builder.Services.AddScoped<CacheService>();

//// Register the change tracker interceptor
//builder.Services.AddScoped<ChangeTrackerInterceptor>();

// Register the custom middleware
string basePath = SettingsDependancyInjection.ServiceSettings.BaseServicePath;

var app = builder.Build();
app.ConfigureExceptionHandler(app.Environment);
// Configure the HTTP request pipeline.
builder.Services.AddEndpointsApiExplorer();
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
// Use the custom middleware with factory pattern to resolve scoped services
//app.Use(async (context, next) =>
//{
//    var cacheService = context.RequestServices.GetRequiredService<CacheService>();
//    var middleware = new RedisCacheMiddleware(next, cacheService);
//    await middleware.InvokeAsync(context);
//});
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
app.Run();
