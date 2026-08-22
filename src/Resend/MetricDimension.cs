using System.Text.Json.Serialization;

namespace Resend;

/// <summary>
/// A dimension by which email metrics can be broken down.
/// </summary>
/// <remarks>
/// <see cref="Email"/> cannot be combined with <see cref="Broadcast"/> -- the API rejects
/// that combination with a validation error.
/// </remarks>
[JsonConverter( typeof( JsonStringEnumValueConverter<MetricDimension> ) )]
public enum MetricDimension
{
    /// <summary>
    /// Break down results into time buckets, sized per the requested granularity.
    /// </summary>
    [JsonStringValue( "period" )]
    Period = 1,

    /// <summary>
    /// Break down results by sending domain.
    /// </summary>
    [JsonStringValue( "domain" )]
    Domain,

    /// <summary>
    /// Break down results by individual email.
    /// </summary>
    [JsonStringValue( "email" )]
    Email,

    /// <summary>
    /// Break down results by broadcast.
    /// </summary>
    [JsonStringValue( "broadcast" )]
    Broadcast,
}
