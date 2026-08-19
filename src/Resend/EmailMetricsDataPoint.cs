using System.Text.Json;
using System.Text.Json.Serialization;

namespace Resend;

/// <summary>
/// One row of <see cref="EmailMetrics.Data"/>. Which dimension fields are populated depends on
/// the dimensions requested via <see cref="EmailMetricsQuery.Dimensions"/>.
/// </summary>
public class EmailMetricsDataPoint
{
    /// <summary>
    /// Time bucket this row covers, present when <see cref="MetricDimension.Period"/> was requested.
    /// </summary>
    [JsonPropertyName( "period" )]
    public string? Period { get; set; }

    /// <summary>
    /// Sending domain identifier, present when <see cref="MetricDimension.Domain"/> was requested.
    /// </summary>
    [JsonPropertyName( "domain_id" )]
    public Guid? DomainId { get; set; }

    /// <summary>
    /// Sending domain name, present when <see cref="MetricDimension.Domain"/> was requested.
    /// </summary>
    [JsonPropertyName( "domain_name" )]
    public string? DomainName { get; set; }

    /// <summary>
    /// Email identifier, present when <see cref="MetricDimension.Email"/> was requested.
    /// </summary>
    [JsonPropertyName( "email_id" )]
    public Guid? EmailId { get; set; }

    /// <summary>
    /// Broadcast identifier, present when <see cref="MetricDimension.Broadcast"/> was requested.
    /// </summary>
    [JsonPropertyName( "broadcast_id" )]
    public Guid? BroadcastId { get; set; }

    /// <summary>
    /// Broadcast display name, present when <see cref="MetricDimension.Broadcast"/> was requested.
    /// </summary>
    [JsonPropertyName( "broadcast_name" )]
    public string? BroadcastName { get; set; }

    /// <summary>
    /// Metric values for this row, keyed by metric name (for example <c>delivered</c>,
    /// <c>opened</c>) -- the API returns these as sibling fields of the dimension keys above,
    /// rather than nested under their own key, and the set present depends on the requested
    /// <see cref="EmailMetricsQuery.Metrics"/>.
    /// </summary>
    [JsonExtensionData]
    public Dictionary<string, JsonElement> MetricValues { get; set; } = new();
}
