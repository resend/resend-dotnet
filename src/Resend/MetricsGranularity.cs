using System.Text.Json.Serialization;

namespace Resend;

/// <summary>
/// Bucket size used to group metrics data points when <see cref="MetricDimension.Period"/>
/// is one of the requested dimensions.
/// </summary>
[JsonConverter( typeof( JsonStringEnumValueConverter<MetricsGranularity> ) )]
public enum MetricsGranularity
{
    /// <summary>
    /// One data point per hour.
    /// </summary>
    [JsonStringValue( "hourly" )]
    Hourly = 1,

    /// <summary>
    /// One data point per day.
    /// </summary>
    [JsonStringValue( "daily" )]
    Daily,

    /// <summary>
    /// One data point per week.
    /// </summary>
    [JsonStringValue( "weekly" )]
    Weekly,

    /// <summary>
    /// One data point per month.
    /// </summary>
    [JsonStringValue( "monthly" )]
    Monthly,
}
