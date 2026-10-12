using System.Text.Json.Serialization;

namespace Resend;

/// <summary>
/// Outcome of moving an inbox thread to trash.
/// </summary>
/// <see href="https://resend.com/docs/api-reference/inboxes/delete-thread" />
public class InboxThreadDeleteResult
{
    /// <summary>
    /// Object type discriminator.
    /// </summary>
    [JsonPropertyName( "object" )]
    public string Object { get; set; } = default!;

    /// <summary>
    /// Thread identifier.
    /// </summary>
    [JsonPropertyName( "id" )]
    public Guid Id { get; set; }

    /// <summary>
    /// True when this call moved the thread to trash. False when the thread was already
    /// there.
    /// </summary>
    [JsonPropertyName( "deleted" )]
    public bool Deleted { get; set; }
}
