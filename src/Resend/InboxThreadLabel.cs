using System.Text.Json.Serialization;

namespace Resend;

/// <summary>
/// Label attached to an inbox thread.
/// </summary>
/// <see href="https://resend.com/docs/api-reference/inboxes/get-thread" />
public class InboxThreadLabel
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
}
