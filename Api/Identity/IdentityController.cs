namespace Api.Identity;

using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

/// <summary>
/// Controller responsible for identity related operations such as token generation.
/// </summary>
public class IdentityController : ControllerBase
{
    private const string TokenKey = "thisKeyShouldBeInAzureKeyVaultOrSomewhereSafe";
    private static readonly TimeSpan TokenExpiration = TimeSpan.FromMinutes(30);

    /// <summary>
    /// Generates a JWT token for the given user request.
    /// </summary>
    /// <param name="request">The user request containing user details.</param>
    /// <returns>A JWT token.</returns>
    [HttpPost("token")]
    public IActionResult GenerateToken([FromBody] TokenRequest request)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(TokenKey);

        var claims = new List<Claim>
        {
            new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new(JwtRegisteredClaimNames.Sub, request.Email),
            new(JwtRegisteredClaimNames.Email, request.Email),
            new("userid", request.UserId),
        };

        var claim = new Claim(nameof(request.IsAdmin), request.IsAdmin.ToString()!, ClaimValueTypes.Boolean);
        claims.Add(claim);

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.Add(TokenExpiration),
            Issuer = "BookInterviewAPI",
            Audience = "BookInterviewClient",
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);

        var jwtToken = tokenHandler.WriteToken(token);

        return Ok(jwtToken);
    }
}
