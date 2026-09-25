using System.Text.Json.Serialization;

namespace Resend;

/// <summary>
/// Email usage for the current day.
/// </summary>
public class UsageEmailDaily
{
    /// <summary>
    /// Total emails sent and received today.
    /// </summary>
    [JsonPropertyName( "used" )]
    public long Used { get; set; }

    /// <summary>
    /// Daily email limit, or <see langword="null"/> when the account has no daily limit.
    /// </summary>
    [JsonPropertyName( "limit" )]
    public long? Limit { get; set; }

    /// <summary>
    /// Emails sent today.
    /// </summary>
    [JsonPropertyName( "sent" )]
    public long Sent { get; set; }

    /// <summary>
    /// Emails received today.
    /// </summary>
    [JsonPropertyName( "received" )]
    public long Received { get; set; }

    /// <summary>
    /// Moment when this daily counter resets.
    /// </summary>
    [JsonPropertyName( "resets_at" )]
    [JsonConverter( typeof( JsonUtcDateTimeConverter ) )]
    public DateTime MomentReset { get; set; }
}
