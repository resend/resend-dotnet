using System.Text.Json;
using System.Text.Json.Serialization;

namespace Resend;

/// <summary>
/// Request body to update an event definition (typically the payload schema).
/// </summary>
public class EventUpdateData
{
    /// <summary>
    /// Payload schema as flat key/type pairs; include <c>null</c> to clear the schema.
    /// </summary>
    [JsonPropertyName( "schema" )]
    public JsonElement? Schema { get; set; }
}
