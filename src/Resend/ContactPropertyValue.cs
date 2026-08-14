using System.Text.Json.Serialization;

namespace Resend;

/// <summary>
/// Custom property value on a contact.
/// </summary>
public class ContactPropertyValue
{
    /// <summary>
    /// Property value.
    /// </summary>
    /// <remarks>
    /// A string, number, or boolean, depending on the type field.
    /// </remarks>
    [JsonPropertyName( "value" )]
    public object? Value { get; set; }

    /// <summary>
    /// Property data-type.
    /// </summary>
    [JsonPropertyName( "type" )]
    public ContactPropertyType PropertyType { get; set; }
}
