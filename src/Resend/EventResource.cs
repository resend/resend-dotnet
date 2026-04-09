using System.Text.Json;
using System.Text.Json.Serialization;

namespace Resend;

/// <summary>
/// A named event definition (schema and metadata) used with automations.
/// </summary>
public class EventResource
{
    /// <summary>
    /// Object type discriminator.
    /// </summary>
    [JsonPropertyName( "object" )]
    public string Object { get; set; } = default!;

    /// <summary>
    /// Event identifier.
    /// </summary>
    [JsonPropertyName( "id" )]
    public Guid Id { get; set; }

    /// <summary>
    /// Event name (matches trigger configuration).
    /// </summary>
    [JsonPropertyName( "name" )]
    public string Name { get; set; } = default!;

    /// <summary>
    /// Optional payload schema as flat key/type pairs (values: <c>string</c>, <c>number</c>, <c>boolean</c>, <c>date</c>).
    /// </summary>
    [JsonPropertyName( "schema" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public JsonElement? Schema { get; set; }

    /// <summary>
    /// When the event was created.
    /// </summary>
    [JsonPropertyName( "created_at" )]
    [JsonConverter( typeof( JsonUtcDateTimeConverter ) )]
    public DateTime MomentCreated { get; set; }

    /// <summary>
    /// When the event was last updated.
    /// </summary>
    [JsonPropertyName( "updated_at" )]
    [JsonConverter( typeof( JsonUtcDateTimeConverter ) )]
    public DateTime MomentUpdated { get; set; }
}
