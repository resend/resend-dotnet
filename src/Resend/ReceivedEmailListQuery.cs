namespace Resend;

/// <summary>
/// Query parameters for
/// <see cref="IResend.ReceivedEmailListAsync(ReceivedEmailListQuery?, CancellationToken)"/>.
/// </summary>
/// <remarks>
/// Different parameters combine, so an email must match all of them.
/// </remarks>
public class ReceivedEmailListQuery : PaginatedQuery
{
    /// <summary>
    /// Recipients. Each value matches an envelope recipient exactly, with case.
    /// </summary>
    /// <remarks>
    /// Returns emails received by any of them. Up to 20 values.
    /// </remarks>
    public List<string>? To { get; set; }

    /// <summary>
    /// Matches emails received on or after this date.
    /// </summary>
    /// <remarks>
    /// Accepts a date like <c>2026-09-01</c> or an ISO 8601 timestamp, in UTC unless the
    /// timestamp has an offset. Typed as a string because a date and a timestamp match
    /// differently.
    /// </remarks>
    public string? StartDate { get; set; }

    /// <summary>
    /// Matches emails received on or before this date.
    /// </summary>
    /// <remarks>
    /// Same formats as <see cref="StartDate"/>. A date covers that whole day, so a start
    /// date of <c>2026-09-01</c> and an end date of <c>2026-09-30</c> cover all of
    /// September. A timestamp is used as given. An end date before the start date returns
    /// a 422 validation_error.
    /// </remarks>
    public string? EndDate { get; set; }
}
