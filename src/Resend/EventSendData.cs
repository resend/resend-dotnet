using System.Text.Json;
using System.Text.Json.Serialization;

namespace Resend;

/// <summary>
/// Request body to send a named event (for example to trigger automations).
/// </summary>
/// <remarks>
/// Provide exactly one of <see cref="ContactId"/> or <see cref="Email"/>.
/// </remarks>
public class EventSendData
{
    /// <summary>
    /// Event name (must match a trigger step).
    /// </summary>
    [JsonPropertyName( "event" )]
    public string Event { get; set; } = default!;

    /// <summary>
    /// Contact to associate with the event.
    /// </summary>
    [JsonPropertyName( "contact_id" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public Guid? ContactId { get; set; }

    /// <summary>
    /// Email address to associate with the event.
    /// </summary>
    [JsonPropertyName( "email" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public string? Email { get; set; }

    /// <summary>
    /// Optional payload passed to templates and conditions.
    /// </summary>
    [JsonPropertyName( "payload" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public JsonElement? Payload { get; set; }
}
