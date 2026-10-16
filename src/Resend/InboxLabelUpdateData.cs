using System.Text.Json.Serialization;

namespace Resend;

/// <summary>
/// Request body to update an inbox label. Omitted fields keep their current value.
/// </summary>
/// <see href="https://resend.com/docs/api-reference/inboxes/update-label" />
public class InboxLabelUpdateData
{
    /// <summary>
    /// New name for the label.
    /// </summary>
    /// <remarks>
    /// Max 64 characters. Leading and trailing whitespace is trimmed. Must not match another
    /// label on the inbox, compared without case.
    /// </remarks>
    [JsonPropertyName( "name" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public string? Name { get; set; }

    /// <summary>
    /// New color for the label, as a <c>#RRGGBB</c> hex color.
    /// </summary>
    /// <remarks>
    /// It is stored in uppercase.
    /// </remarks>
    [JsonPropertyName( "color" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public string? Color { get; set; }
}
