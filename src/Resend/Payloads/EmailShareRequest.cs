using System.Text.Json.Serialization;

namespace Resend.Payloads;

/// <summary />
public class EmailShareRequest
{
    /// <summary />
    [JsonPropertyName( "expires_in" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public string? ExpiresIn { get; set; }
}
