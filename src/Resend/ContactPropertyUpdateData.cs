using System.Text.Json.Serialization;

namespace Resend;

/// <summary />
public class ContactPropertyUpdateData
{
    /// <summary>
    /// Default value to use when the property is not set for a contact.
    /// </summary>
    /// <remarks>
    /// Must match the type specified in the type field.
    /// </remarks>
    [JsonPropertyName( "fallback_value" )]
    public object? DefaultValue { get; set; }
}