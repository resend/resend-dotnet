using System.Text.Json.Serialization;

namespace Resend;

/// <summary>
/// A broadcast recipient, as returned for a given <see cref="BroadcastRecipientEventType"/>.
/// </summary>
/// <see href="https://resend.com/docs/api-reference/broadcasts/list-broadcast-recipients" />
public class BroadcastRecipient
{
    /// <summary>
    /// Opaque cursor identifying this row, used only for pagination.
    /// </summary>
    /// <remarks>
    /// Does not identify any entity in Resend -- use <see cref="ContactId"/> to reference
    /// the contact.
    /// </remarks>
    [JsonPropertyName( "id" )]
    public string Id { get; set; } = default!;

    /// <summary>
    /// Identifier of the contact matching this recipient.
    /// </summary>
    /// <remarks>
    /// Null when the recipient's email no longer maps to a contact.
    /// </remarks>
    [JsonPropertyName( "contact_id" )]
    public Guid? ContactId { get; set; }

    /// <summary>
    /// The recipient's email address.
    /// </summary>
    [JsonPropertyName( "email" )]
    public string Email { get; set; } = default!;

    /// <summary>
    /// Number of times this recipient triggered the event.
    /// </summary>
    /// <remarks>
    /// Only present when the requested <see cref="BroadcastRecipientEventType"/> is
    /// <see cref="BroadcastRecipientEventType.Opened"/> or <see cref="BroadcastRecipientEventType.Clicked"/>.
    /// </remarks>
    [JsonPropertyName( "count" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public int? Count { get; set; }

    /// <summary>
    /// The bounce classification.
    /// </summary>
    /// <remarks>
    /// Only present when the requested <see cref="BroadcastRecipientEventType"/> is
    /// <see cref="BroadcastRecipientEventType.Bounced"/>.
    /// </remarks>
    [JsonPropertyName( "bounce_type" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public BroadcastRecipientBounceType? BounceType { get; set; }

    /// <summary>
    /// The links this recipient clicked.
    /// </summary>
    /// <remarks>
    /// Only present when the requested <see cref="BroadcastRecipientEventType"/> is
    /// <see cref="BroadcastRecipientEventType.Clicked"/>.
    /// </remarks>
    [JsonPropertyName( "clicked_links" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public List<BroadcastRecipientClickedLink>? ClickedLinks { get; set; }
}
