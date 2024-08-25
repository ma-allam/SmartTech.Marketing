using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SmartTech.Marketing.Core.Exceptions;
using SmartTech.Marketing.Domain.Entities;
using SmartTech.Marketing.Persistence.Context;
using SmartTech.Marketing.WebApi.DependencyInjection;
using SmartTech.Marketing.WebApi.Swagger;
using Swashbuckle.AspNetCore.Filters;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Security.Claims;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

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


var app = builder.Build();
app.ConfigureExceptionHandler(app.Environment);
// Configure the HTTP request pipeline.
builder.Services.AddEndpointsApiExplorer();

if (app.Environment.IsDevelopment())
{
    string basePath = "api";
    var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
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

app.MapControllers();

app.Run();
