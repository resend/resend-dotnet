using System.Text.Json.Serialization;

namespace Resend;

/// <summary>
/// Summary of a single inbox thread.
/// </summary>
/// <remarks>
/// The emails in the thread are listed with
/// <see cref="IResend.InboxThreadEmailListAsync(InboxIdOrEmail, Guid, PaginatedQuery?, CancellationToken)"/>.
/// </remarks>
/// <see href="https://resend.com/docs/api-reference/inboxes/get-thread" />
public class InboxThreadSummary
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
    /// Subject of the thread.
    /// </summary>
    [JsonPropertyName( "subject" )]
    public string? Subject { get; set; }

    /// <summary>
    /// Folder the thread is in.
    /// </summary>
    [JsonPropertyName( "folder" )]
    public InboxFolder Folder { get; set; }

    /// <summary>
    /// Labels attached to the thread.
    /// </summary>
    [JsonPropertyName( "labels" )]
    public List<InboxThreadLabel> Labels { get; set; } = new();

    /// <summary>
    /// True only when every email in the thread is read.
    /// </summary>
    [JsonPropertyName( "read" )]
    public bool IsRead { get; set; }
}
