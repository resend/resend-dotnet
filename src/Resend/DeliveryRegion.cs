﻿using System.Text.Json.Serialization;

namespace Resend;

/// <summary>
/// Region/data center from which emails are sent from.
/// </summary>
/// <see href="https://github.com/resend/resend-node/blob/canary/src/domains/interfaces/domain.ts" />
[JsonConverter( typeof( JsonStringEnumValueConverter<DeliveryRegion> ) )]
public enum DeliveryRegion
{
    /// <summary>
    /// United States East (North Virginia)
    /// </summary>
    [JsonStringValue( "us-east-1" )]
    UsEast1 = 1,

    /// <summary>
    /// Europe (Ireland)
    /// </summary>
    [JsonStringValue( "eu-west-1" )]
    EuWest1,

    /// <summary>
    /// South America (Sao Paulo, Brazil).
    /// </summary>
    [JsonStringValue( "sa-east-1" )]
    SaEast1,

    /// <summary>
    /// Asia Pacific (Tokyo)
    /// </summary>
    [JsonStringValue( "ap-northeast-1" )]
    ApNorthEast1,
}
