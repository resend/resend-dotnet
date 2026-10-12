using System.Text.Json.Serialization;

namespace Resend;

/// <summary>
/// Folder of an inbox thread.
/// </summary>
/// <see href="https://resend.com/docs/api-reference/inboxes/list-threads" />
[JsonConverter( typeof( JsonStringEnumValueConverter<InboxFolder> ) )]
public enum InboxFolder
{
    /// <summary>
    /// Threads with at least one received email.
    /// </summary>
    [JsonStringValue( "inbox" )]
    Inbox,

    /// <summary>
    /// Threads moved to the archive.
    /// </summary>
    [JsonStringValue( "archive" )]
    Archive,

    /// <summary>
    /// Threads moved to spam.
    /// </summary>
    [JsonStringValue( "spam" )]
    Spam,

    /// <summary>
    /// Threads whose emails are all outbound.
    /// </summary>
    /// <remarks>
    /// Assigned automatically. A thread cannot be moved to this folder.
    /// </remarks>
    [JsonStringValue( "sent" )]
    Sent,

    /// <summary>
    /// Threads scheduled for deletion in 30 days.
    /// </summary>
    [JsonStringValue( "trash" )]
    Trash,
}
