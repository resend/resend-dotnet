using System.Text.Json.Serialization;

namespace Resend;

/// <summary />
public class ContactProperty
{
    /// <summary>
    /// Property identifier.
    /// </summary>
    [JsonPropertyName( "id" )]
    public Guid Id { get; set; }

    /// <summary>
    /// Property key.
    /// </summary>
    /// <remarks>
    /// Max length is 50 characters. Only alphanumeric characters and underscores are allowed.
    /// </remarks>
    [JsonPropertyName( "key" )]
    public string Key { get; set; } = default!;

    /// <summary>
    /// Property key.
    /// </summary>
    [JsonPropertyName( "type" )]
    public ContactPropertyType PropertyType { get; set; }

    /// <summary>
    /// Default value to use when the property is not set for a contact.
    /// </summary>
    /// <remarks>
    /// Must match the type specified in the type field.
    /// </remarks>
    [JsonPropertyName( "fallback_value" )]
    public object? DefaultValue { get; set; }

    /// <summary>
    /// Moment when the property was created.
    /// </summary>
    [JsonPropertyName( "created_at" )]
    [JsonConverter( typeof( JsonUtcDateTimeConverter ) )]
    public DateTime MomentCreated { get; set; }
}