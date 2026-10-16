using System.Text.Json.Serialization;

namespace Resend;

/// <summary>
/// Outcome of removing an inbox draft.
/// </summary>
/// <see href="https://resend.com/docs/api-reference/inboxes/delete-draft" />
public class InboxDraftDeleteResult
{
    /// <summary>
    /// Object type discriminator.
    /// </summary>
    [JsonPropertyName( "object" )]
    public string Object { get; set; } = default!;

    /// <summary>
    /// Draft identifier.
    /// </summary>
    [JsonPropertyName( "id" )]
    public Guid Id { get; set; }

    /// <summary>
    /// Whether the draft was deleted.
    /// </summary>
    [JsonPropertyName( "deleted" )]
    public bool Deleted { get; set; }
}
