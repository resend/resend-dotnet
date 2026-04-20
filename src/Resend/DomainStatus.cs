using System.Text.Json.Serialization;

namespace Resend;

/// <summary />
/// <see href="https://resend.com/docs/dashboard/domains/introduction#understand-a-domain-status" />
/// <see href="https://github.com/resend/resend-node/blob/canary/src/domains/interfaces/domain.ts" />
[JsonConverter( typeof( JsonStringEnumValueConverter<DomainStatus> ) )]
public enum DomainStatus
{
    /// <summary>
    /// Domain has been created, but validation hasn't been explicitly requested.
    /// </summary>
    /// <remarks>
    /// Validation can be initiated by calling the <see cref="IResend.DomainVerifyAsync(Guid, CancellationToken)"/>
    /// method.
    /// </remarks>
    [JsonStringValue( "not_started" )]
    NotStarted,

    /// <summary>
    /// Validation has been started and is currently executing.
    /// </summary>
    /// <remarks>
    /// May take up to 72h to conclude.
    /// </remarks>
    [JsonStringValue( "pending" )]
    Pending,

    /// <summary>
    /// Validation has failed: Resend was unable to detect necessary DNS
    /// records within 72h.
    /// </summary>
    [JsonStringValue( "failed" )]
    Failed,

    /// <summary>
    /// Domain is successfully verified for sending in Resend.
    /// </summary>
    [JsonStringValue( "verified" )]
    Verified,

    /// <summary>
    /// Domain is partially verified: some capabilities (for example,
    /// sending) are verified while others are still pending.
    /// </summary>
    [JsonStringValue( "partially_verified" )]
    PartiallyVerified,

    /// <summary>
    /// Domain is partially failed: some capabilities are verified while
    /// others have failed validation.
    /// </summary>
    [JsonStringValue( "partially_failed" )]
    PartiallyFailed,
}
