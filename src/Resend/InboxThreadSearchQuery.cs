namespace Resend;

/// <summary>
/// Query parameters for <see cref="IResend.InboxThreadSearchAsync"/>.
/// </summary>
/// <remarks>
/// Different parameters combine, so a thread must match all of them.
/// <see cref="InboxThreadListQuery.Folders"/>, <see cref="InboxThreadListQuery.Labels"/>
/// and <see cref="InboxThreadListQuery.IsRead"/> apply to the thread. The other filters
/// must all match the same email in the thread. When any of these email filters is set, the
/// matching email must itself be in one of the folders. For <see cref="InboxFolder.Sent"/>,
/// that means an email the inbox sent.
/// </remarks>
public class InboxThreadSearchQuery : InboxThreadListQuery
{
    /// <summary>
    /// Free text to search for in the subject, body, sender, recipients, and attachment
    /// names.
    /// </summary>
    /// <remarks>
    /// Every word must match. Wrap words in double quotes to match an exact phrase. Start a
    /// word or quoted phrase with <c>-</c> to exclude it. Max 256 characters and 32 words.
    /// </remarks>
    public string? Query { get; set; }

    /// <summary>
    /// Senders. Each value matches part of the sender's address or name, without case.
    /// </summary>
    /// <remarks>
    /// Returns threads that match any of them. Up to 20 values.
    /// </remarks>
    public List<string>? From { get; set; }

    /// <summary>
    /// Recipients, matched the same way as <see cref="From"/>. Up to 20 values.
    /// </summary>
    public List<string>? To { get; set; }

    /// <summary>
    /// CC recipients, matched the same way as <see cref="From"/>. Up to 20 values.
    /// </summary>
    public List<string>? Cc { get; set; }

    /// <summary>
    /// BCC recipients, matched the same way as <see cref="From"/>. Up to 20 values.
    /// </summary>
    public List<string>? Bcc { get; set; }

    /// <summary>
    /// True matches emails with an attachment. False matches emails without one.
    /// </summary>
    public bool? HasAttachment { get; set; }

    /// <summary>
    /// Matches emails sent on or after this date.
    /// </summary>
    /// <remarks>
    /// Accepts a date like <c>2026-09-01</c> or an ISO 8601 timestamp, in UTC unless the
    /// timestamp has an offset. Typed as a string because a date and a timestamp match
    /// differently.
    /// </remarks>
    public string? StartDate { get; set; }

    /// <summary>
    /// Matches emails sent on or before this date.
    /// </summary>
    /// <remarks>
    /// Same formats as <see cref="StartDate"/>. A date covers that whole day, so a start
    /// date of <c>2026-09-01</c> and an end date of <c>2026-09-30</c> cover all of
    /// September. A timestamp is used as given. An end date before the start date returns
    /// a 422 validation_error.
    /// </remarks>
    public string? EndDate { get; set; }
}
