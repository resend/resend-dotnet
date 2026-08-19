namespace Resend;

/// <summary>
/// Query parameters for <see cref="IResend.EmailMetricsAsync"/>.
/// </summary>
public class EmailMetricsQuery
{
    /// <summary>
    /// Start of the reporting period. Defaults server-side to 6 days before <see cref="EndDate"/>.
    /// </summary>
    public DateTime? StartDate { get; set; }

    /// <summary>
    /// End of the reporting period. Defaults server-side to now.
    /// </summary>
    public DateTime? EndDate { get; set; }

    /// <summary>
    /// IANA timezone (for example <c>America/New_York</c>) used to bucket results. Defaults
    /// server-side to <c>UTC</c>.
    /// </summary>
    public string? Timezone { get; set; }

    /// <summary>
    /// Bucket size used when <see cref="MetricDimension.Period"/> is one of <see cref="Dimensions"/>.
    /// Defaults server-side to <see cref="MetricsGranularity.Daily"/>.
    /// </summary>
    public MetricsGranularity? Granularity { get; set; }

    /// <summary>
    /// Metrics to include in the response. Defaults server-side to all metrics.
    /// </summary>
    public List<MetricType>? Metrics { get; set; }

    /// <summary>
    /// Dimensions to break the results down by. Defaults server-side to none, in which case
    /// the response carries only <see cref="EmailMetrics.Totals"/>, no <see cref="EmailMetrics.Data"/>.
    /// </summary>
    /// <remarks>
    /// <see cref="MetricDimension.Email"/> cannot be combined with <see cref="MetricDimension.Broadcast"/>
    /// -- the API rejects that combination with a validation error.
    /// </remarks>
    public List<MetricDimension>? Dimensions { get; set; }

    /// <summary>
    /// Restrict results to these sending domains. Maximum 100.
    /// </summary>
    public List<Guid>? DomainId { get; set; }

    /// <summary>
    /// Restrict results to these emails. Maximum 100.
    /// </summary>
    /// <remarks>
    /// Cannot be combined with the <see cref="MetricDimension.Broadcast"/> dimension or
    /// with <see cref="BroadcastId"/>.
    /// </remarks>
    public List<Guid>? EmailId { get; set; }

    /// <summary>
    /// Restrict results to these broadcasts. Maximum 100.
    /// </summary>
    /// <remarks>
    /// Cannot be combined with the <see cref="MetricDimension.Email"/> dimension or
    /// with <see cref="EmailId"/>.
    /// </remarks>
    public List<Guid>? BroadcastId { get; set; }
}
