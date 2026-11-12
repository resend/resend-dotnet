using System.Text.Json.Serialization;

namespace Resend;

/// <summary>
/// Request object for enabling Forge on a domain.
/// </summary>
public class DomainForgeData
{
    /// <summary>
    /// Subdomain used for the `Return-Path` address.
    /// </summary>
    /// <remarks>
    /// When omitted, the server picks a free DNS label based on the domain's
    /// current return path.
    /// </remarks>
    [JsonPropertyName( "return_path" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public string? ReturnPath { get; set; }
}
