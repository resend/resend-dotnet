using System.Text.Json.Serialization;

namespace Resend;

/// <summary>
/// Segment usage.
/// </summary>
public class UsageSegments
{
    /// <summary>
    /// Segments currently created.
    /// </summary>
    [JsonPropertyName( "used" )]
    public long Used { get; set; }

    /// <summary>
    /// Maximum number of segments allowed, or <see langword="null"/> when the account has no limit.
    /// </summary>
    [JsonPropertyName( "limit" )]
    public long? Limit { get; set; }
}
