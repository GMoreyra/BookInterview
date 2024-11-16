
using System.ComponentModel.DataAnnotations;

namespace Api.Options;
/// <summary>
/// Represents the settings required for JWT authentication.
/// </summary>
public class JwtOptions
{
    /// <summary>
    /// The section name of the JWT settings in the configuration file.
    /// </summary>
    public const string SectionName = "JwtOptions";

    /// <summary>
    /// Gets or sets the issuer of the JWT.
    /// </summary>
    [Required(AllowEmptyStrings = false)]
    public required string Issuer { get; set; }

    /// <summary>
    /// Gets or sets the audience of the JWT.
    /// </summary>
    [Required(AllowEmptyStrings = false)]
    public required string Audience { get; set; }

    /// <summary>
    /// Gets or sets the key used for signing the JWT.
    /// </summary>
    /// <remarks>
    /// This should not be stored here for security reasons. 
    /// Consider storing it in a secure place like Azure Key Vault.
    /// </remarks>
    [Required(AllowEmptyStrings = false)]
    public required string Key { get; set; }
}
