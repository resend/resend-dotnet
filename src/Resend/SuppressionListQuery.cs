namespace Resend;

/// <summary>
/// Query parameters for <see cref="IResend.SuppressionListAsync"/>.
/// </summary>
public class SuppressionListQuery : PaginatedQuery
{
    /// <summary>
    /// Filter by the origin of the suppression.
    /// </summary>
    public SuppressionOrigin? Origin { get; set; }
}
