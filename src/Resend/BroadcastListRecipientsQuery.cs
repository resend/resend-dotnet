namespace Resend;

/// <summary>
/// Query parameters for <see cref="IResend.BroadcastListRecipientsAsync"/>.
/// </summary>
public class BroadcastListRecipientsQuery : PaginatedQuery
{
    /// <summary>
    /// Filters recipients whose email contains this value.
    /// </summary>
    public string? Email { get; set; }

    /// <summary>
    /// Filters bounced recipients by bounce classification.
    /// </summary>
    /// <remarks>
    /// Only meaningful when the requested event type is <see cref="BroadcastRecipientEventType.Bounced"/>.
    /// </remarks>
    public BroadcastRecipientBounceType? BounceType { get; set; }
}
