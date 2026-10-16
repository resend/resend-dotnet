using System.Text.Json.Serialization;

namespace Resend;

/// <summary>
/// Inbox that sends and receives email at an address on one of your domains.
/// </summary>
/// <remarks>
/// Returned when retrieving a single inbox. The list endpoint returns
/// <see cref="InboxSummary"/>, which carries fewer fields.
/// </remarks>
/// <see href="https://resend.com/docs/api-reference/inboxes/get-inbox" />
public class Inbox
{
    /// <summary>
    /// Object type discriminator.
    /// </summary>
    [JsonPropertyName( "object" )]
    public string Object { get; set; } = default!;

    /// <summary>
    /// Inbox identifier.
    /// </summary>
    [JsonPropertyName( "id" )]
    public Guid Id { get; set; }

    /// <summary>
    /// Internal name for the inbox. Recipients do not see it.
    /// </summary>
    /// <remarks>
    /// Falls back to the inbox email address.
    /// </remarks>
    [JsonPropertyName( "name" )]
    public string Name { get; set; } = default!;

    /// <summary>
    /// Address of the inbox.
    /// </summary>
    [JsonPropertyName( "email_address" )]
    public string EmailAddress { get; set; } = default!;

    /// <summary>
    /// Identifier of the domain that owns the address.
    /// </summary>
    [JsonPropertyName( "domain_id" )]
    public Guid DomainId { get; set; }

    /// <summary>
    /// Address to forward mail to when forwarding is enabled.
    /// </summary>
    /// <remarks>
    /// Null when forwarding is not enabled.
    /// </remarks>
    [JsonPropertyName( "receiving_address" )]
    public string? ReceivingAddress { get; set; }

    /// <summary>
    /// Name recipients see when mail is sent from this inbox.
    /// </summary>
    /// <remarks>
    /// Null when not set.
    /// </remarks>
    [JsonPropertyName( "from_name" )]
    public string? FromName { get; set; }

    /// <summary>
    /// Number of unread threads in the Inbox folder.
    /// </summary>
    [JsonPropertyName( "unread" )]
    public int UnreadCount { get; set; }

    /// <summary>
    /// Number of unsent drafts.
    /// </summary>
    [JsonPropertyName( "drafts" )]
    public int DraftCount { get; set; }

    /// <summary>
    /// Moment of the newest received email in the Inbox folder.
    /// </summary>
    /// <remarks>
    /// Null when the folder has no received email.
    /// </remarks>
    [JsonPropertyName( "last_received" )]
    [JsonConverter( typeof( JsonUtcDateTimeConverter ) )]
    public DateTime? MomentLastReceived { get; set; }

    /// <summary>
    /// Moment when the inbox was created.
    /// </summary>
    [JsonPropertyName( "created_at" )]
    [JsonConverter( typeof( JsonUtcDateTimeConverter ) )]
    public DateTime MomentCreated { get; set; }

    /// <summary>
    /// Moment when the inbox was last updated.
    /// </summary>
    [JsonPropertyName( "updated_at" )]
    [JsonConverter( typeof( JsonUtcDateTimeConverter ) )]
    public DateTime MomentUpdated { get; set; }
}
