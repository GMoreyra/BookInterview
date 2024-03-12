namespace Api.Configurations;

using CrossCutting.Exceptions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

/// <summary>
/// Provides methods for API initialization.
/// </summary>
public static class ApiConfiguration
{
    /// <summary>
    /// Configures JWT authentication for the API.
    /// </summary>
    /// <param name="services">The service collection to add JWT authentication to.</param>
    /// <param name="jwtOptions">The JWT options instance to get JWT settings from.</param>
    /// <returns>The service collection with JWT authentication added.</returns>
    public static IServiceCollection JwtConfiguration(this IServiceCollection services, JwtOptions? jwtOptions)
    {
        Argument.ThrowIfNull(() => jwtOptions);

        services.AddAuthentication(x =>
        {
            x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        }).AddJwtBearer(x =>
        {
            x.TokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = jwtOptions!.Issuer,
                ValidAudience = jwtOptions!.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions!.Key)),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
            };
        });

        return services;
    }
}
