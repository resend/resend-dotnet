using System.Text.Json.Serialization;

namespace Resend;

/// <summary>
/// Contact data, for creation and update.
/// </summary>
public class ContactData
{
    /// <summary>
    /// Email address of the contact.
    /// </summary>
    /// <remarks>
    /// Required during Contact/Create, optional during Contact/Update.
    /// </remarks>
    [JsonPropertyName( "email" )]
    public string? Email { get; set; }

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
    /// Whether the contact is unsubscribed.
    /// </summary>
    [JsonPropertyName( "unsubscribed" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public bool? IsUnsubscribed { get; set; }

    /// <summary>
    /// Key/value custom properties for the contact.
    /// </summary>
    /// <remarks>
    /// Values may be a string, a number or null. A null value clears the property.
    /// Boolean values are accepted only during Contact/Update.
    /// </remarks>
    [JsonPropertyName( "properties" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public Dictionary<string, object?>? Properties { get; set; }
}
