using System.Text.Json.Serialization;

namespace Resend.Payloads;

/// <summary />
public class DomainVerifyRequest
{
    /// <summary />
    [JsonPropertyName( "scope" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public List<DomainVerificationScope>? Scope { get; set; }
}
