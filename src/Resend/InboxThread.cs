using System.Text.Json.Serialization;

namespace Resend;

/// <summary>
/// Thread as it appears in the list of an inbox's threads.
/// </summary>
/// <remarks>
/// A thread is a conversation made up of one or more emails. Retrieving a single thread
/// returns <see cref="InboxThreadSummary"/> instead.
/// </remarks>
/// <see href="https://resend.com/docs/api-reference/inboxes/list-threads" />
public class InboxThread
{
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
    /// Sender email address. Can include the display name.
    /// </summary>
    [JsonPropertyName( "from" )]
    public EmailAddress? From { get; set; }

    /// <summary>
    /// Recipients of the most recent email.
    /// </summary>
    /// <remarks>
    /// For a received email, these are the addresses it was delivered to, not the full
    /// <c>To</c> header.
    /// </remarks>
    [JsonPropertyName( "to" )]
    public EmailAddressList To { get; set; } = new EmailAddressList();

    /// <summary>
    /// CC recipients of the most recent email.
    /// </summary>
    [JsonPropertyName( "cc" )]
    public EmailAddressList Cc { get; set; } = new EmailAddressList();

    /// <summary>
    /// BCC recipients of the most recent email.
    /// </summary>
    /// <remarks>
    /// Always empty for a received email.
    /// </remarks>
    [JsonPropertyName( "bcc" )]
    public EmailAddressList Bcc { get; set; } = new EmailAddressList();

    /// <summary>
    /// Labels attached to the thread.
    /// </summary>
    [JsonPropertyName( "labels" )]
    public List<InboxThreadLabel> Labels { get; set; } = new();

    /// <summary>
    /// Number of emails in the thread.
    /// </summary>
    [JsonPropertyName( "message_count" )]
    public int MessageCount { get; set; }

    /// <summary>
    /// Whether the most recent email in the thread has an attachment.
    /// </summary>
    [JsonPropertyName( "has_attachment" )]
    public bool HasAttachment { get; set; }

    /// <summary>
    /// Whether the thread has an unsent draft.
    /// </summary>
    [JsonPropertyName( "has_draft" )]
    public bool HasDraft { get; set; }

    /// <summary>
    /// True only when every email in the thread is read.
    /// </summary>
    [JsonPropertyName( "read" )]
    public bool IsRead { get; set; }

    /// <summary>
    /// Moment when the thread was last active.
    /// </summary>
    [JsonPropertyName( "received_at" )]
    [JsonConverter( typeof( JsonUtcDateTimeConverter ) )]
    public DateTime MomentReceived { get; set; }

    /// <summary>
    /// Folder the thread is in.
    /// </summary>
    /// <remarks>
    /// Every thread is in exactly one folder.
    /// </remarks>
    [JsonPropertyName( "folder" )]
    public InboxFolder Folder { get; set; }
}
