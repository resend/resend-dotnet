using System.Text.Json;
using System.Text.Json.Serialization;

namespace Resend;

/// <summary>
/// Custom property value on a contact.
/// </summary>
public class ContactPropertyValue
{
    /// <summary>
    /// Property value; a string, number, or boolean, depending on <see cref="PropertyType"/>.
    /// </summary>
    [JsonPropertyName( "value" )]
    public JsonElement Value { get; set; }

    /// <summary>
    /// Property data-type.
    /// </summary>
    [JsonPropertyName( "type" )]
    public ContactPropertyType PropertyType { get; set; }
}
