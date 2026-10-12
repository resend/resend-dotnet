using System.Text.Json.Serialization;

namespace Resend;

/// <summary>
/// Request body to create an inbox label.
/// </summary>
/// <see href="https://resend.com/docs/api-reference/inboxes/create-label" />
public class InboxLabelCreateData
{
    /// <summary>
    /// Name of the label.
    /// </summary>
    /// <remarks>
    /// Max 64 characters. Leading and trailing whitespace is trimmed. Must not match another
    /// label on the inbox, compared without case.
    /// </remarks>
    [JsonPropertyName( "name" )]
    public string Name { get; set; } = default!;

    /// <summary>
    /// Color of the label, as a <c>#RRGGBB</c> hex color.
    /// </summary>
    /// <remarks>
    /// It is stored in uppercase. When omitted, a random color from the dashboard palette
    /// is used.
    /// </remarks>
    [JsonPropertyName( "color" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public string? Color { get; set; }
}
