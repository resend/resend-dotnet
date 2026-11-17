using System.Text.Json.Serialization;

namespace Resend;

/// <summary>
/// Request object for enabling Forge on a domain.
/// </summary>
public class DomainForgeData
{
    /// <summary>
    /// Overrides the default value of `Return-Path`.
    /// </summary>
    /// <remarks>
    /// When omitted, the server picks a free DNS label. Set once, it cannot be
    /// changed afterwards.
    /// </remarks>
    [JsonPropertyName( "custom_return_path" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public string? CustomReturnPath { get; set; }
}
