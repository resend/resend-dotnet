using System.Text.Json.Serialization;

namespace Resend;

/// <summary>
/// Request object to create an email sender domain.
/// </summary>
public class DomainAddData
{
    /// <summary>
    /// Domain name.
    /// </summary>
    [JsonPropertyName( "name" )]
    public string Name { get; set; } = default!;

    /// <summary>
    /// Delivery region.
    /// </summary>
    [JsonPropertyName( "region" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public DeliveryRegion? Region { get; set; }

    /// <summary>
    /// Overrides the default value of `Return-Path`.
    /// </summary>
    /// <remarks>
    /// By default, Resend will use the `send` subdomain for the `Return-Path` address.
    /// </remarks>
    [JsonPropertyName( "custom_return_path" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public string? CustomReturnPath { get; set; }
}