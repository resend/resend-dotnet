using System.Text.Json.Serialization;

namespace Resend;

/// <summary>
/// API rate limit that applies to this account.
/// </summary>
public class UsageRateLimit
{
    /// <summary>
    /// Maximum number of requests allowed within <see cref="Duration"/>.
    /// </summary>
    [JsonPropertyName( "limit" )]
    public int Limit { get; set; }

    /// <summary>
    /// Length of the rate limit window (for example <c>1000ms</c>).
    /// </summary>
    [JsonPropertyName( "duration" )]
    public string Duration { get; set; } = default!;
}
