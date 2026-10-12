using System.Text.Json.Serialization;

namespace Resend;

/// <summary>
/// Inbox, as returned by the list endpoint.
/// </summary>
/// <remarks>
/// Retrieve an inbox with <see cref="IResend.InboxRetrieveAsync(Guid, CancellationToken)"/>
/// to get all its fields.
/// </remarks>
/// <see href="https://resend.com/docs/api-reference/inboxes/list-inboxes" />
public class InboxSummary
{
    /// <summary>
    /// Inbox identifier.
    /// </summary>
    [JsonPropertyName( "id" )]
    public Guid Id { get; set; }

    /// <summary>
    /// Internal name for the inbox. Recipients do not see it.
    /// </summary>
    [JsonPropertyName( "name" )]
    public string? Name { get; set; }

    /// <summary>
    /// Address of the inbox.
    /// </summary>
    [JsonPropertyName( "email_address" )]
    public string EmailAddress { get; set; } = default!;

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
    /// Moment of the newest received email in the Inbox folder.
    /// </summary>
    /// <remarks>
    /// Null when the folder has no received email.
    /// </remarks>
    [JsonPropertyName( "last_received" )]
    [JsonConverter( typeof( JsonUtcDateTimeConverter ) )]
    public DateTime? MomentLastReceived { get; set; }
}
