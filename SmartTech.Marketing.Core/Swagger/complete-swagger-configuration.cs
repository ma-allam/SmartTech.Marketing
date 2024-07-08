using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using System.Text.RegularExpressions;

using Microsoft.OpenApi.Any;

/// <summary>
/// Extension methods for configuring Swagger in the application.
/// </summary>
public static class SwaggerServiceExtensions
{
    /// <summary>
    /// Adds and configures Swagger services to the specified IServiceCollection.
    /// </summary>
    /// <param name="services">The IServiceCollection to add services to.</param>
    /// <returns>The IServiceCollection for further configuration.</returns>
    public static IServiceCollection AddSwagger(this IServiceCollection services)
    {
        services.AddSwaggerGen(c =>
        {
            // Configure JWT authentication for Swagger UI
            c.AddSecurityDefinition(JwtBearerDefaults.AuthenticationScheme, new OpenApiSecurityScheme
            {
                In = ParameterLocation.Header,
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey
            });
            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" }
                    },
                    new string[] {}
                }
            });

            // Add the custom schema ID generator
            c.CustomSchemaIds(CustomSchemaIdSelector);
        });

        // Add the ConfigureSwaggerOptions to support API versioning
        services.AddTransient<IConfigureOptions<SwaggerGenOptions>, ConfigureSwaggerOptions>();

        services.AddSwaggerGen(
            options =>
            {
                options.EnableAnnotations();
                // Add the SwaggerDefaultValues filter to enhance operation documentation
                options.OperationFilter<SwaggerDefaultValues>();
            });

        return services;
    }

    /// <summary>
    /// Custom schema ID generator to create unique IDs for Swagger schemas.
    /// </summary>
    public static string CustomSchemaIdSelector(Type modelType)
    {
        // Get the full name of the type (including namespace)
        string fullName = modelType.FullName;

        // Replace dots with underscores to avoid issues in OpenAPI spec
        string schemaId = fullName.Replace(".", "_");

        // Remove any generic type parameters
        schemaId = Regex.Replace(schemaId, @"`\d+", string.Empty);

        // If the type is nested, include the parent type name
        if (modelType.IsNested)
        {
            schemaId = schemaId.Replace("+", "_");
        }

        // Handle array types
        if (modelType.IsArray)
        {
            schemaId = $"ArrayOf{schemaId.Replace("[]", string.Empty)}";
        }

        // Handle generic types
        if (modelType.IsGenericType)
        {
            var genericArguments = modelType.GetGenericArguments()
                .Select(CustomSchemaIdSelector);
            var genericPartIndex = schemaId.IndexOf("_");
            if (genericPartIndex > 0)
            {
                schemaId = schemaId.Substring(0, genericPartIndex);
            }
            schemaId = $"{schemaId}Of{string.Join("And", genericArguments)}";
        }

        return schemaId;
    }
}


/// <summary>
/// Configures Swagger options, including API versioning and XML documentation.
/// </summary>
public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
{
    private readonly IApiVersionDescriptionProvider provider;

    /// <summary>
    /// Initializes a new instance of the ConfigureSwaggerOptions class.
    /// </summary>
    /// <param name="provider">The IApiVersionDescriptionProvider used to generate Swagger documents.</param>
    public ConfigureSwaggerOptions(IApiVersionDescriptionProvider provider) => this.provider = provider;

    /// <summary>
    /// Configures the Swagger options.
    /// </summary>
    /// <param name="options">The SwaggerGenOptions to configure.</param>
    public void Configure(SwaggerGenOptions options)
    {
        // Include XML comments in the Swagger documentation
        List<string> xmlFiles = Directory.GetFiles(AppContext.BaseDirectory, "*.xml", SearchOption.TopDirectoryOnly).ToList();
        xmlFiles.ForEach(xmlFile => options.IncludeXmlComments(xmlFile));

        // Create a swagger document for each discovered API version
        foreach (var description in provider.ApiVersionDescriptions)
        {
            options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
        }

        // Add the custom schema ID selector here as well to ensure consistency
        options.CustomSchemaIds(x => SwaggerServiceExtensions.CustomSchemaIdSelector(x));
    }

    /// <summary>
    /// Creates the OpenApiInfo for a specific API version.
    /// </summary>
    /// <param name="description">The ApiVersionDescription for which to create the info.</param>
    /// <returns>An OpenApiInfo object containing the API information.</returns>
    private static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
    {
        var apiAssembly = Assembly.GetEntryAssembly();
        string apiName = apiAssembly?.FullName?.Split(',')[0] ?? "";
        var apiNameReplaced = apiName!.Replace(".WebApi", "");
        ///var apiNameReplaced = apiName!.Replace("SmartTech.Marketing.", "")!.Replace(".WebApi", "");

        var info = new OpenApiInfo()
        {
            Title = apiNameReplaced + " Service",
            Version = description.ApiVersion.ToString(),
            Description = "Manage all service business",
            Contact = new OpenApiContact() { Name = "Mahmoud Allam", Email = "eng.ma.mo.allam@hotmail.com" },
            License = new OpenApiLicense() { Name = "MIT", Url = new Uri("https://opensource.org/licenses/MIT") }
        };
        if (description.IsDeprecated)
        {
            info.Description += " This API version has been deprecated.";
        }

        return info;
    }
}



/// <summary>
/// Operation filter to add default values and enhance Swagger operation documentation.
/// </summary>
public class SwaggerDefaultValues : IOperationFilter
{
    /// <summary>
    /// Applies the filter to the specified operation using the given context.
    /// </summary>
    /// <param name="operation">The operation to apply the filter to.</param>
    /// <param name="context">The current operation filter context.</param>
    public void Apply(OpenApiOperation operation, OperationFilterContext context)
    {
        var apiDescription = context.ApiDescription;

        // Mark the operation as deprecated if necessary
        operation.Deprecated |= apiDescription.IsDeprecated();

        if (operation.Parameters == null)
        {
            return;
        }

        // Update parameter metadata
        foreach (var parameter in operation.Parameters)
        {
            var description = apiDescription.ParameterDescriptions.First(p => p.Name == parameter.Name);

            // Set the parameter description if not already set
            if (parameter.Description == null)
            {
                parameter.Description = description.ModelMetadata?.Description;
            }

            // Set the default value if available
            if (parameter.Schema.Default == null && description.DefaultValue != null)
            {
                parameter.Schema.Default = new OpenApiString(description.DefaultValue.ToString());
            }

            // Mark the parameter as required if necessary
            parameter.Required |= description.IsRequired;
        }
    }
}
