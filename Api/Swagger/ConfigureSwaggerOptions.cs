namespace Api.Swagger;

using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;

/// <summary>
/// Configures the options for SwaggerGen.
/// </summary>
public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
{
    /// <summary>
    /// Configures the SwaggerGen options.
    /// </summary>
    /// <param name="options">The SwaggerGen options to configure.</param>
    public void Configure(SwaggerGenOptions options)
    {
        options.SwaggerDoc("v1", new OpenApiInfo
        {
            Version = "v1",
            Title = "Book Interview API",
            Description = "An ASP.NET Core Web API for managing Books",
        });

        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "Please enter a valid token",
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            BearerFormat = "JWT",
            Scheme = "Bearer"
        });

        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type=ReferenceType.SecurityScheme,
                        Id="Bearer"
                    }
                },
                Array.Empty<string>()
            }
        });

        var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    }
}
