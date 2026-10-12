using System.Text.Json.Serialization;

namespace Resend;

/// <summary>
/// Thread that matched a search.
/// </summary>
/// <see href="https://resend.com/docs/api-reference/inboxes/search-threads" />
public class InboxThreadSearchResult : InboxThread
{
    /// <summary>
    /// Identifies the email in the thread that matched the search.
    /// </summary>
    /// <remarks>
    /// When the query has words or phrases, it is the most relevant match. Otherwise, it is
    /// the newest. The get, reply and forward thread email methods accept it as the email
    /// identifier, but it is not equal to the <see cref="InboxThreadEmail.Id"/> those
    /// methods return. Null when the search index returns no matched email for the thread.
    /// </remarks>
    [JsonPropertyName( "matched_email_id" )]
    public Guid? MatchedEmailId { get; set; }

    /// <summary>
    /// Where the query matched the email in <see cref="MatchedEmailId"/>.
    /// </summary>
    /// <remarks>
    /// Empty when the search has no query.
    /// </remarks>
    [JsonPropertyName( "highlights" )]
    public InboxThreadSearchHighlights Highlights { get; set; } = new();
}
