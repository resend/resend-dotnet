using System.Text.Json.Serialization;

namespace Resend;

/// <summary>
/// Response from accepting an event (HTTP 202).
/// </summary>
public class EventSendResult
{
    /// <summary>
    /// Object type discriminator.
    /// </summary>
    [JsonPropertyName( "object" )]
    public string Object { get; set; } = default!;

    /// <summary>
    /// Event name that was accepted.
    /// </summary>
    [JsonPropertyName( "event" )]
    public string Event { get; set; } = default!;
}
