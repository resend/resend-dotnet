using System.Text.Json.Serialization;

namespace Resend;

/// <summary>
/// Contact usage.
/// </summary>
public class UsageContacts
{
    /// <summary>
    /// Contacts currently stored.
    /// </summary>
    [JsonPropertyName( "used" )]
    public long Used { get; set; }

    /// <summary>
    /// Maximum number of contacts allowed.
    /// </summary>
    [JsonPropertyName( "limit" )]
    public long Limit { get; set; }
}
