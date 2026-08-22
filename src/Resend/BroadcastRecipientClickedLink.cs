using System.Text.Json.Serialization;

namespace Resend;

/// <summary>
/// A link clicked by a broadcast recipient.
/// </summary>
public class BroadcastRecipientClickedLink
{
    /// <summary>
    /// The clicked URL.
    /// </summary>
    [JsonPropertyName( "url" )]
    public string Url { get; set; } = default!;

    /// <summary>
    /// Number of times this recipient clicked this URL.
    /// </summary>
    [JsonPropertyName( "clicks" )]
    public int Clicks { get; set; }
}
