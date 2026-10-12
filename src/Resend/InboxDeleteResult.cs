using System.Text.Json.Serialization;

namespace Resend;

/// <summary>
/// Outcome of removing an inbox.
/// </summary>
/// <see href="https://resend.com/docs/api-reference/inboxes/delete-inbox" />
public class InboxDeleteResult
{
    /// <summary>
    /// Object type discriminator.
    /// </summary>
    [JsonPropertyName( "object" )]
    public string Object { get; set; } = default!;

    /// <summary>
    /// Inbox identifier.
    /// </summary>
    [JsonPropertyName( "id" )]
    public Guid Id { get; set; }

    /// <summary>
    /// Whether the inbox was deleted.
    /// </summary>
    [JsonPropertyName( "deleted" )]
    public bool Deleted { get; set; }
}
