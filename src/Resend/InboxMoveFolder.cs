using System.Text.Json.Serialization;

namespace Resend;

/// <summary>
/// Folder that an inbox thread can be moved to.
/// </summary>
/// <remarks>
/// <see cref="InboxFolder.Sent"/> is assigned automatically, so it is not a move target.
/// </remarks>
/// <see href="https://resend.com/docs/api-reference/inboxes/update-thread" />
[JsonConverter( typeof( JsonStringEnumValueConverter<InboxMoveFolder> ) )]
public enum InboxMoveFolder
{
    /// <summary>
    /// Inbox folder.
    /// </summary>
    [JsonStringValue( "inbox" )]
    Inbox,

    /// <summary>
    /// Archive folder.
    /// </summary>
    [JsonStringValue( "archive" )]
    Archive,

    /// <summary>
    /// Spam folder.
    /// </summary>
    [JsonStringValue( "spam" )]
    Spam,

    /// <summary>
    /// Trash folder. The thread is scheduled for deletion in 30 days.
    /// </summary>
    [JsonStringValue( "trash" )]
    Trash,
}
