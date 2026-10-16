using System.Text.Json.Serialization;

namespace Resend;

/// <summary>
/// Request body to update an inbox thread. Provide at least one of <see cref="IsRead"/>
/// or <see cref="Folder"/>.
/// </summary>
/// <remarks>
/// To change the labels of a thread, use <see cref="IResend.InboxThreadLabelAddAsync"/>,
/// <see cref="IResend.InboxThreadLabelSetAsync"/> and <see cref="IResend.InboxThreadLabelRemoveAsync"/>.
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
}
