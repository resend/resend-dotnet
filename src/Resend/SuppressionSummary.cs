using System.Text.Json.Serialization;

namespace Resend;

/// <summary>
/// A suppressed email address, as returned by the suppression list.
/// </summary>
/// <remarks>
/// The list endpoint does not return the <c>object</c> discriminator that
/// <see cref="Suppression"/> carries; the two shapes are otherwise identical.
/// </remarks>
/// <see href="https://resend.com/docs/api-reference/suppressions/list-suppressions" />
public class SuppressionSummary
{
    /// <summary>
    /// Suppression identifier.
    /// </summary>
    [JsonPropertyName( "id" )]
    public Guid Id { get; set; }

    /// <summary>
    /// Email address that is suppressed.
    /// </summary>
    [JsonPropertyName( "email" )]
    public string Email { get; set; } = default!;

    /// <summary>
    /// Origin of the suppression.
    /// </summary>
    [JsonPropertyName( "origin" )]
    public SuppressionOrigin Origin { get; set; }

    /// <summary>
    /// Identifier of the event that caused the suppression, such as the email
    /// that bounced or was marked as spam.
    /// </summary>
    /// <remarks>
    /// Null for suppressions of <see cref="SuppressionOrigin.Manual"/> origin. Typed as a
    /// string rather than a <see cref="Guid"/> because the underlying column is free-text:
    /// it holds an email identifier today, but nothing at the API or database layer
    /// constrains it, and a non-UUID value would otherwise fail to deserialize -- which on
    /// a list response would throw away the entire page, not just one entry.
    /// </remarks>
    [JsonPropertyName( "source_id" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public string? SourceId { get; set; }

    /// <summary>
    /// Moment when the suppression was created.
    /// </summary>
    [JsonPropertyName( "created_at" )]
    [JsonConverter( typeof( JsonUtcDateTimeConverter ) )]
    public DateTime MomentCreated { get; set; }
}
