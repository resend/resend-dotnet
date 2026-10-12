using System.Text.Json.Serialization;

namespace Resend;

/// <summary>
/// Request body to update an inbox thread. Provide at least one field.
/// </summary>
/// <remarks>
/// A label identifier cannot be both added and removed in the same request.
/// </remarks>
/// <see href="https://resend.com/docs/api-reference/inboxes/update-thread" />
public class InboxThreadUpdateData
{
    /// <summary>
    /// Marks every email in the thread read or unread.
    /// </summary>
    [JsonPropertyName( "read" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public bool? IsRead { get; set; }

    /// <summary>
    /// Moves the thread to this folder.
    /// </summary>
    /// <remarks>
    /// Moving a thread out of <see cref="InboxMoveFolder.Trash"/> restores it and clears its
    /// deletion deadline.
    /// </remarks>
    [JsonPropertyName( "folder" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public InboxMoveFolder? Folder { get; set; }

    /// <summary>
    /// Applies this label to the thread, the same as one entry in <see cref="AddLabelIds"/>.
    /// </summary>
    /// <remarks>
    /// A label identifier that does not exist in the inbox returns a 404.
    /// </remarks>
    [Obsolete( "Use AddLabelIds instead." )]
    [JsonPropertyName( "label_id" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public Guid? LabelId { get; set; }

    /// <summary>
    /// Label identifiers to apply to the thread.
    /// </summary>
    /// <remarks>
    /// Between 1 and 10 identifiers. A label identifier that does not exist in the inbox
    /// returns a 404.
    /// </remarks>
    [JsonPropertyName( "add_label_ids" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public List<Guid>? AddLabelIds { get; set; }

    /// <summary>
    /// Label identifiers to remove from the thread.
    /// </summary>
    /// <remarks>
    /// Between 1 and 10 identifiers. A label identifier that does not exist in the inbox
    /// returns a 404.
    /// </remarks>
    [JsonPropertyName( "remove_label_ids" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public List<Guid>? RemoveLabelIds { get; set; }
}
