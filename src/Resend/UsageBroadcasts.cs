using System.Text.Json.Serialization;

namespace Resend;

/// <summary>
/// Broadcast usage.
/// </summary>
public class UsageBroadcasts
{
    /// <summary>
    /// Broadcasts sent.
    /// </summary>
    [JsonPropertyName( "used" )]
    public long Used { get; set; }

    /// <summary>
    /// Maximum number of broadcasts allowed. Currently always <see langword="null"/> (unlimited).
    /// </summary>
    [JsonPropertyName( "limit" )]
    public long? Limit { get; set; }
}
