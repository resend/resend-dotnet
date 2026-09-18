using System.Text.Json.Serialization;

namespace Resend.Webhooks;

/// <summary />
public class SuppressionEventData : IWebhookData
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
    /// Null for suppressions of <see cref="SuppressionOrigin.Manual"/> origin.
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
