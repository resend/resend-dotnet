using System.Text.Json;
using System.Text.Json.Serialization;

namespace Resend;

/// <summary>
/// Request body to create an event definition.
/// </summary>
public class EventCreateData
{
    /// <summary>
    /// Event name (for example <c>user.created</c>).
    /// </summary>
    [JsonPropertyName( "name" )]
    public string Name { get; set; } = default!;

    /// <summary>
    /// Optional payload schema as flat key/type pairs.
    /// </summary>
    [JsonPropertyName( "schema" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public JsonElement? Schema { get; set; }
}
