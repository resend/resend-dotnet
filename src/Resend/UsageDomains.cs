using System.Text.Json.Serialization;

namespace Resend;

/// <summary>
/// Verified domain usage.
/// </summary>
public class UsageDomains
{
    /// <summary>
    /// Domains currently verified.
    /// </summary>
    [JsonPropertyName( "used" )]
    public long Used { get; set; }

    /// <summary>
    /// Maximum number of domains allowed, or <see langword="null"/> when the account has no limit.
    /// </summary>
    [JsonPropertyName( "limit" )]
    public long? Limit { get; set; }
}
