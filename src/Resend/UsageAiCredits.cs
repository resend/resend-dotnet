using System.Text.Json.Serialization;

namespace Resend;

/// <summary>
/// AI credits usage.
/// </summary>
public class UsageAiCredits
{
    /// <summary>
    /// AI credits used.
    /// </summary>
    [JsonPropertyName( "used" )]
    public long Used { get; set; }

    /// <summary>
    /// Maximum number of AI credits allowed, or <see langword="null"/> when the account has no limit.
    /// </summary>
    [JsonPropertyName( "limit" )]
    public long? Limit { get; set; }

    /// <summary>
    /// Moment when the AI credits allowance next increases, or <see langword="null"/> when
    /// no increase is scheduled.
    /// </summary>
    [JsonPropertyName( "next_increase_at" )]
    [JsonConverter( typeof( JsonUtcDateTimeConverter ) )]
    public DateTime? MomentNextIncrease { get; set; }
}
