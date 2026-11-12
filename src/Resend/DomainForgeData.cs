using System.Text.Json.Serialization;

namespace Resend;

/// <summary>
/// Request object for enabling or updating Forge on a domain.
/// </summary>
public class DomainForgeData
{
    /// <summary>
    /// Subdomain used for the `Return-Path` address.
    /// </summary>
    /// <remarks>
    /// When omitted on enable, the API picks a suggested label based on the
    /// domain's current return path.
    /// </remarks>
    [JsonPropertyName( "return_path" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public string? ReturnPath { get; set; }
}
