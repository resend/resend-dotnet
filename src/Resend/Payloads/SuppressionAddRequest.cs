using System.Text.Json.Serialization;

namespace Resend.Payloads;

/// <summary>
/// Request object to add a suppression.
/// </summary>
public class SuppressionAddRequest
{
    /// <summary>
    /// Email address to suppress.
    /// </summary>
    [JsonPropertyName( "email" )]
    public string Email { get; set; } = default!;
}
