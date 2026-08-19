using System.Text.Json.Serialization;

namespace Resend;

/// <summary>
/// Email metrics for a given reporting period.
/// </summary>
/// <see href="https://resend.com/docs/api-reference/emails/retrieve-email-metrics"/>
public class EmailMetrics
{
    /// <summary />
    [JsonPropertyName( "object" )]
    public string Object { get; set; } = default!;

    /// <summary>
    /// Start of the reporting period.
    /// </summary>
    [JsonPropertyName( "start_date" )]
    [JsonConverter( typeof( JsonUtcDateTimeConverter ) )]
    public DateTime StartDate { get; set; }

    /// <summary>
    /// End of the reporting period.
    /// </summary>
    [JsonPropertyName( "end_date" )]
    [JsonConverter( typeof( JsonUtcDateTimeConverter ) )]
    public DateTime EndDate { get; set; }

    /// <summary>
    /// Metrics included in this response.
    /// </summary>
    [JsonPropertyName( "metrics" )]
    public List<MetricType> Metrics { get; set; } = default!;

    /// <summary>
    /// Dimensions this response is broken down by.
    /// </summary>
    [JsonPropertyName( "dimensions" )]
    public List<MetricDimension> Dimensions { get; set; } = default!;

    /// <summary>
    /// Bucket size used for the <see cref="MetricDimension.Period"/> dimension.
    /// </summary>
    [JsonPropertyName( "granularity" )]
    public MetricsGranularity Granularity { get; set; }

    /// <summary>
    /// Totals for the whole reporting period, one entry per requested metric.
    /// </summary>
    [JsonPropertyName( "totals" )]
    public Dictionary<string, double> Totals { get; set; } = default!;

    /// <summary>
    /// One row per combination of the requested dimensions. Absent when no dimensions
    /// were requested -- in that case only <see cref="Totals"/> is populated.
    /// </summary>
    [JsonPropertyName( "data" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public List<EmailMetricsDataPoint>? Data { get; set; }
}
