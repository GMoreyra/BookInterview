namespace Api.Identity;

/// <summary>
/// Represents a request for a token.
/// </summary>
public class TokenRequest
{
    /// <summary>
    /// Gets or sets the user ID.
    /// </summary>
    public required string UserId { get; set; }

    /// <summary>
    /// Gets or sets the email.
    /// </summary>
    public required string Email { get; set; }

    /// <summary>
    /// Gets or sets the admin status.
    /// </summary>
    public required bool IsAdmin { get; set; }
}
