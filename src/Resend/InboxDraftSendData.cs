using System.Text.Json.Serialization;

namespace Resend;

/// <summary>
/// Optional request body to send an inbox draft.
/// </summary>
/// <see href="https://resend.com/docs/api-reference/inboxes/send-draft" />
public class InboxDraftSendData
{
    /// <summary>
    /// Custom headers to add to the email.
    /// </summary>
    /// <remarks>
    /// The In-Reply-To, References and Reply-To headers are set by Resend and cannot be set
    /// here. Sending one of them, in any letter case, returns a 422.
    /// </remarks>
    [JsonPropertyName( "headers" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public Dictionary<string, string>? Headers { get; set; }
}
