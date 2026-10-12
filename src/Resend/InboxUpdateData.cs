using System.Text.Json.Serialization;

namespace Resend;

/// <summary>
/// Request body to update an inbox. Provide at least one of <see cref="Name"/> or
/// <see cref="FromName"/>.
/// </summary>
/// <see href="https://resend.com/docs/api-reference/inboxes/update-inbox" />
public class InboxUpdateData
{
    /// <summary>
    /// New internal name for the inbox. Recipients do not see it.
    /// </summary>
    /// <remarks>
    /// Between 1 and 64 characters after trimming.
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
    /// sending. An empty string removes it.
    /// </remarks>
    [JsonPropertyName( "from_name" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public string? FromName { get; set; }
}
