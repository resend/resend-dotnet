using System.Text.Json.Serialization;

namespace Resend;

/// <summary />
public class DomainUpdateData
{
    /// <summary>
    /// Track clicks within the body of each HTML email.
    /// </summary>
    [JsonPropertyName( "click_tracking" )]
    public bool TrackClicks { get; set; } = default!;

    /// <summary>
    /// Track the open rate of each email.
    /// </summary>
    [JsonPropertyName( "open_tracking" )]
    public bool TrackOpen { get; set; } = default!;

    /// <summary>
    /// TLS mode.
    /// </summary>
    [JsonPropertyName( "tls" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public TlsMode? TlsMode { get; set; }

    /// <summary>
    /// Configure sending and receiving for this domain. At least one capability must remain enabled.
    /// </summary>
    [JsonPropertyName( "capabilities" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public DomainCapabilities? Capabilities { get; set; }

    /// <summary>
    /// Subdomain for click and open tracking (for example, <c>links</c> for <c>links.example.com</c>).
    /// </summary>
    /// <remarks>
    /// This value can only be set after it has been specified, never cleared. After changing the tracking
    /// subdomain, verify DNS again; until then, the previous value may still be used for tracked links.
    /// </remarks>
    [JsonPropertyName( "tracking_subdomain" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public string? TrackingSubdomain { get; set; }

    /// <summary>
    /// Move the domain's Forge return path to another host.
    /// </summary>
    /// <remarks>
    /// Only possible while the Forge record is still verifying. Moving the path
    /// does not start verification: fetch the new record with
    /// <see cref="IResend.DomainRetrieveAsync(Guid, CancellationToken)"/>, add it
    /// to your DNS, then call
    /// <see cref="IResend.DomainVerifyAsync(Guid, IEnumerable{DomainVerificationScope}, CancellationToken)"/>
    /// with <see cref="DomainVerificationScope.Forge"/>.
    /// </remarks>
    [JsonPropertyName( "forge_return_path" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public string? ForgeReturnPath { get; set; }
}


/// <summary />
[JsonConverter( typeof( JsonStringEnumValueConverter<TlsMode> ) )]
public enum TlsMode
{
    /// <summary>
    /// Opportunistic TLS means that it always attempts to make a
    /// secure connection to the receiving mail server. If it can't
    /// establish a secure connection, it sends the message unencrypted.
    /// </summary>
    [JsonStringValue( "opportunistic" )]
    Opportunistic = 1,

    /// <summary>
    /// Enforced TLS on the other hand, requires that the email
    /// communication must use TLS no matter what. If the
    /// receiving server does not support TLS, the email will
    /// not be sent.
    /// </summary>
    [JsonStringValue( "enforced" )]
    Enforced,
}