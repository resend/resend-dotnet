using System.Text.Json.Serialization;

namespace Resend.Payloads;

/// <summary />
public class DomainForgeEnableRequest
{
    /// <summary />
    [JsonPropertyName( "custom_return_path" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public string? CustomReturnPath { get; set; }
}
