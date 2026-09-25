using System.Text.Json.Serialization;

namespace Resend;

/// <summary>
/// Email usage for the current billing month.
/// </summary>
public class UsageEmailMonthly
{
    /// <summary>
    /// Total emails sent and received this month.
    /// </summary>
    [JsonPropertyName( "used" )]
    public long Used { get; set; }

    /// <summary>
    /// Monthly email limit.
    /// </summary>
    [JsonPropertyName( "limit" )]
    public long Limit { get; set; }

    /// <summary>
    /// Emails sent this month.
    /// </summary>
    [JsonPropertyName( "sent" )]
    public long Sent { get; set; }

    /// <summary>
    /// Emails received this month.
    /// </summary>
    [JsonPropertyName( "received" )]
    public long Received { get; set; }

    /// <summary>
    /// Moment when this monthly counter resets.
    /// </summary>
    [JsonPropertyName( "resets_at" )]
    [JsonConverter( typeof( JsonUtcDateTimeConverter ) )]
    public DateTime MomentReset { get; set; }
}
