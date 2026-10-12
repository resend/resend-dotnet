using System.Text.Json.Serialization;

namespace Resend;

/// <summary>
/// Named, colored tag that belongs to one inbox.
/// </summary>
/// <see href="https://resend.com/docs/api-reference/inboxes/list-labels" />
public class InboxLabel
{
    /// <summary>
    /// Label identifier.
    /// </summary>
    [JsonPropertyName( "id" )]
    public Guid Id { get; set; }

    /// <summary>
    /// Name of the label.
    /// </summary>
    [JsonPropertyName( "name" )]
    public string Name { get; set; } = default!;

    /// <summary>
    /// Color of the label, as a <c>#RRGGBB</c> hex color in uppercase.
    /// </summary>
    [JsonPropertyName( "color" )]
    public string Color { get; set; } = default!;

    /// <summary>
    /// Moment when the label was created.
    /// </summary>
    [JsonPropertyName( "created_at" )]
    [JsonConverter( typeof( JsonUtcDateTimeConverter ) )]
    public DateTime MomentCreated { get; set; }
}
