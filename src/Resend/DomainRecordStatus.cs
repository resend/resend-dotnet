using System.Text.Json.Serialization;

namespace Resend;

/// <summary />
/// <see href="https://resend.com/docs/dashboard/domains/introduction#understand-a-domain-status" />
/// <see href="https://github.com/resend/resend-node/blob/canary/src/domains/interfaces/domain.ts" />
[JsonConverter( typeof( JsonStringEnumValueConverter<DomainRecordStatus> ) )]
public enum DomainRecordStatus
{
    /// <summary>
    /// Record validation hasn't been explicitly requested.
    /// </summary>
    [JsonStringValue( "not_started" )]
    NotStarted,

    /// <summary>
    /// Record validation has been started and is currently executing.
    /// </summary>
    [JsonStringValue( "pending" )]
    Pending,

    /// <summary>
    /// Record validation has failed: Resend was unable to detect the
    /// necessary DNS record within 72h.
    /// </summary>
    [JsonStringValue( "failed" )]
    Failed,

    /// <summary>
    /// Previously verified DNS record no longer resolves.
    /// </summary>
    /// <remarks>
    /// For a previously verified record, Resend will periodically check for the DNS
    /// record required for verification. If at some point, Resend is unable to detect
    /// the record, the status would change to “<see cref="TemporaryFailure" />”. Resend will
    /// recheck for the DNS record for 72 hours, and if it’s unable to detect the
    /// record, the record status would change to “<see cref="Failed" />”. If it’s able to detect
    /// the record, the record status would change to “<see cref="Verified"/>”.
    /// </remarks>
    [JsonStringValue( "temporary_failure" )]
    TemporaryFailure,

    /// <summary>
    /// DNS record is successfully verified.
    /// </summary>
    [JsonStringValue( "verified" )]
    Verified,
}
