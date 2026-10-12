namespace Resend;

/// <summary>
/// Query parameters for <see cref="IResend.InboxThreadListAsync"/>.
/// </summary>
/// <remarks>
/// The <see cref="PaginatedQuery.After"/> and <see cref="PaginatedQuery.Before"/> cursors
/// take a thread identifier, and only one of them can be passed.
/// </remarks>
public class InboxThreadListQuery : PaginatedQuery
{
    /// <summary>
    /// Folders to list.
    /// </summary>
    /// <remarks>
    /// A thread is returned when any of its emails is in one of the folders. Defaults
    /// server-side to <see cref="InboxFolder.Inbox"/>, or to inbox, archive and sent when
    /// <see cref="Labels"/> is set. <see cref="InboxFolder.Spam"/> and
    /// <see cref="InboxFolder.Trash"/> are only included when you name them.
    /// </remarks>
    public List<InboxFolder>? Folders { get; set; }

    /// <summary>
    /// Label identifiers. Returns threads with any of these labels.
    /// </summary>
    /// <remarks>
    /// Up to 50 identifiers. An identifier that does not exist in the inbox returns a 404.
    /// </remarks>
    public List<Guid>? Labels { get; set; }

    /// <summary>
    /// True returns threads where every email is read. False returns threads with at least
    /// one unread email.
    /// </summary>
    public bool? IsRead { get; set; }
}
