using System.Text.Json.Serialization;

namespace Resend;

/// <summary>
/// Automation run usage.
/// </summary>
public class UsageAutomationRuns
{
    /// <summary>
    /// Automation runs used.
    /// </summary>
    [JsonPropertyName( "used" )]
    public long Used { get; set; }

    /// <summary>
    /// Maximum number of automation runs allowed.
    /// </summary>
    [JsonPropertyName( "limit" )]
    public long Limit { get; set; }

    /// <summary>
    /// Moment when this counter resets.
    /// </summary>
    [JsonPropertyName( "resets_at" )]
    [JsonConverter( typeof( JsonUtcDateTimeConverter ) )]
    public DateTime MomentReset { get; set; }
}
