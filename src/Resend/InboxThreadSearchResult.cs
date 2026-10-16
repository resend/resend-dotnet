using System.Text.Json.Serialization;

namespace Resend;

/// <summary>
/// Thread that matched a search.
/// </summary>
/// <see href="https://resend.com/docs/api-reference/inboxes/search-threads" />
public class InboxThreadSearchResult : InboxThread
{
    /// <summary>
    /// Identifier of the email in the thread that matched the search.
    /// </summary>
    /// <remarks>
    /// When the query has words or phrases, it is the most relevant match. Otherwise, it is
    /// the newest. It is the same <see cref="InboxThreadEmail.Id"/> that the list thread
    /// emails method returns, so use it with the get, reply and forward thread email
    /// methods. Null when the search index returns no matched email for the thread.
    /// </remarks>
    [JsonPropertyName( "matched_email_id" )]
    public Guid? MatchedEmailId { get; set; }
}
