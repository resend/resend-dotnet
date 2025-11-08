using System.Text.Json.Serialization;

namespace Resend;

/// <summary>
/// Properties of Contact.
/// </summary>
public class Contact
{
    /// <summary>
    /// Contact identifier.
    /// </summary>
    [JsonPropertyName( "id" )]
    public Guid Id { get; set; }

    /// <summary>
    /// Email address of the contact.
    /// </summary>
    [JsonPropertyName( "email" )]
    public string Email { get; set; } = default!;

    /// <summary>
    /// First name of the contact.
    /// </summary>
    [JsonPropertyName( "first_name" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public string? FirstName { get; set; }

    /// <summary>
    /// Last name of the contact.
    /// </summary>
    [JsonPropertyName( "last_name" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public string? LastName { get; set; }

    /// <summary>
    /// Moment in which the contact was created.
    /// </summary>
    [JsonPropertyName( "created_at" )]
    [JsonConverter( typeof( JsonUtcDateTimeConverter ) )]
    public DateTime MomentCreated { get; set; }

    /// <summary>
    /// Whether the contact is unsubscribed.
    /// </summary>
    [JsonPropertyName( "unsubscribed" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public bool? IsUnsubscribed { get; set; }

    /// <summary>
    /// Key/value custom properties for the contact.
    /// </summary>
    [JsonPropertyName( "properties" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public Dictionary<string,string>? Properties { get; set; }
}