using System.Text.Json.Serialization;

namespace Resend;

/// <summary>
/// Action that the agent of an inbox can take.
/// </summary>
/// <see href="https://resend.com/docs/api-reference/inboxes/update-agent" />
[JsonConverter( typeof( JsonStringEnumValueConverter<InboxAgentAction> ) )]
public enum InboxAgentAction
{
    /// <summary>
    /// Drafts a reply.
    /// </summary>
    [JsonStringValue( "draft_reply" )]
    DraftReply,

    /// <summary>
    /// Forwards the thread.
    /// </summary>
    [JsonStringValue( "forward_thread" )]
    ForwardThread,

    /// <summary>
    /// Adds labels to the thread.
    /// </summary>
    [JsonStringValue( "add_labels" )]
    AddLabels,

    /// <summary>
    /// Assigns the thread.
    /// </summary>
    [JsonStringValue( "assign_thread" )]
    AssignThread,

    /// <summary>
    /// Marks the thread as spam.
    /// </summary>
    [JsonStringValue( "mark_as_spam" )]
    MarkAsSpam,

    /// <summary>
    /// Archives the thread.
    /// </summary>
    [JsonStringValue( "archive_thread" )]
    ArchiveThread,

    /// <summary>
    /// Deletes the thread.
    /// </summary>
    [JsonStringValue( "delete_thread" )]
    DeleteThread,
}
