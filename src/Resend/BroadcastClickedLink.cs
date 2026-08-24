using System.Text.Json.Serialization;

namespace Resend;

/// <summary>
/// A single clicked link in a broadcast clicked-links list response.
/// </summary>
public class BroadcastClickedLink
{
    /// <summary>
    /// An opaque cursor for this row, used only for pagination.
    /// It does not identify any entity in Resend.
    /// </summary>
    [JsonPropertyName( "id" )]
    public string Id { get; set; } = default!;

    /// <summary>
    /// The URL that was clicked.
    /// </summary>
    [JsonPropertyName( "url" )]
    public string Url { get; set; } = default!;

    /// <summary>
    /// Total number of clicks on this URL.
    /// </summary>
    [JsonPropertyName( "clicks" )]
    public int Clicks { get; set; }

    /// <summary>
    /// Number of unique clicks on this URL.
    /// </summary>
    [JsonPropertyName( "unique_clicks" )]
    public int UniqueClicks { get; set; }
}
