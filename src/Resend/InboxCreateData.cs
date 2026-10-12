using System.Text.Json.Serialization;

namespace Resend;

/// <summary>
/// Request body to create an inbox.
/// </summary>
/// <see href="https://resend.com/docs/api-reference/inboxes/create-inbox" />
public class InboxCreateData
{
    /// <summary>
    /// Inbox email address on one of your domains.
    /// </summary>
    /// <remarks>
    /// The API trims and lowercases it. The part before the <c>@</c> is at most 64
    /// characters and contains only letters, numbers, and <c>. _ % + -</c>. It cannot start
    /// or end with a dot or contain two dots in a row.
    /// </remarks>
    [JsonPropertyName( "email_address" )]
    public string EmailAddress { get; set; } = default!;

    /// <summary>
    /// Internal name for the inbox. Recipients do not see it.
    /// </summary>
    /// <remarks>
    /// Max 64 characters. Defaults to <see cref="EmailAddress"/>.
    /// </remarks>
    [JsonPropertyName( "name" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public string? Name { get; set; }

    /// <summary>
    /// Name recipients see when mail is sent from this inbox.
    /// </summary>
    /// <remarks>
    /// A plain name, not a <c>Name &lt;email&gt;</c> address. Max 78 characters. It cannot
    /// contain a colon, <c>&lt;</c>, <c>&gt;</c>, line breaks, or other control characters.
    /// An email address inside the name must be on a domain the team has verified for
    /// sending. An empty string leaves it unset.
    /// </remarks>
    [JsonPropertyName( "from_name" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public string? FromName { get; set; }

    /// <summary>
    /// When true, Resend provisions a receiving address so you can receive mail without
    /// adding an MX record.
    /// </summary>
    /// <remarks>
    /// The address is returned in <see cref="Inbox.ReceivingAddress"/>. Defaults
    /// server-side to false.
    /// </remarks>
    [JsonPropertyName( "forwarding" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public bool? Forwarding { get; set; }
}
