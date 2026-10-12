using System.Text.Json.Serialization;

namespace Resend;

/// <summary>
/// Outcome of removing an inbox label.
/// </summary>
/// <see href="https://resend.com/docs/api-reference/inboxes/delete-label" />
public class InboxLabelDeleteResult
{
    /// <summary>
    /// Object type discriminator.
    /// </summary>
    [JsonPropertyName( "object" )]
    public string Object { get; set; } = default!;

    /// <summary>
    /// Label identifier.
    /// </summary>
    [JsonPropertyName( "id" )]
    public Guid Id { get; set; }

    /// <summary>
    /// Whether the label was deleted.
    /// </summary>
    [JsonPropertyName( "deleted" )]
    public bool Deleted { get; set; }
}
