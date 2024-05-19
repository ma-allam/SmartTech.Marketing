using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.Extensions.Options;
using SmartTech.Marketing.WebApi.DependencyInjection;
using SmartTech.Marketing.WebApi.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(c =>
{

    c.EnableAnnotations();
    c.OperationFilter<SwaggerDefaultValues>();
});
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

app.UseAuthorization();

app.MapControllers();

app.Run();
