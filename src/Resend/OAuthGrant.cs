using System.Text.Json.Serialization;

namespace Resend;

/// <summary>
/// OAuth grant, representing access a user has delegated to an OAuth client.
/// </summary>
public class OAuthGrant
{
    /// <summary>
    /// OAuth grant identifier.
    /// </summary>
    [JsonPropertyName( "id" )]
    public Guid Id { get; set; }

    /// <summary>
    /// Identifier of the OAuth client the grant was issued to.
    /// </summary>
    [JsonPropertyName( "client_id" )]
    public Guid ClientId { get; set; }

    /// <summary>
    /// Scopes granted to the OAuth client.
    /// </summary>
    [JsonPropertyName( "scopes" )]
    public List<string> Scopes { get; set; } = default!;

    /// <summary>
    /// Resource the grant is restricted to, if any.
    /// </summary>
    [JsonPropertyName( "resource" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public string? Resource { get; set; }

    /// <summary>
    /// Moment in which the grant was created.
    /// </summary>
    [JsonPropertyName( "created_at" )]
    [JsonConverter( typeof( JsonUtcDateTimeConverter ) )]
    public DateTime MomentCreated { get; set; }

    /// <summary>
    /// Moment in which the grant was revoked, if it has been revoked.
    /// </summary>
    [JsonPropertyName( "revoked_at" )]
    [JsonConverter( typeof( JsonUtcDateTimeConverter ) )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public DateTime? MomentRevoked { get; set; }

    /// <summary>
    /// Reason the grant was revoked, if it has been revoked.
    /// </summary>
    [JsonPropertyName( "revoked_reason" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public string? RevokedReason { get; set; }

    /// <summary>
    /// OAuth client the grant was issued to.
    /// </summary>
    [JsonPropertyName( "client" )]
    public OAuthGrantClient Client { get; set; } = default!;
}


/// <summary>
/// OAuth client associated with a grant.
/// </summary>
public class OAuthGrantClient
{
    /// <summary>
    /// Display name of the OAuth client.
    /// </summary>
    [JsonPropertyName( "name" )]
    public string Name { get; set; } = default!;

    /// <summary>
    /// URI of the OAuth client logo, if any.
    /// </summary>
    [JsonPropertyName( "logo_uri" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public string? LogoUri { get; set; }
}


/// <summary>
/// Outcome of revoking an OAuth grant.
/// </summary>
public class OAuthGrantRevoked
{
    /// <summary>
    /// OAuth grant identifier.
    /// </summary>
    [JsonPropertyName( "id" )]
    public Guid Id { get; set; }

    /// <summary>
    /// Moment in which the grant was revoked.
    /// </summary>
    [JsonPropertyName( "revoked_at" )]
    [JsonConverter( typeof( JsonUtcDateTimeConverter ) )]
    public DateTime MomentRevoked { get; set; }

    /// <summary>
    /// Reason the grant was revoked.
    /// </summary>
    [JsonPropertyName( "revoked_reason" )]
    public string RevokedReason { get; set; } = default!;
}
