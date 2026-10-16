using System.Text.Json.Serialization;

namespace Resend;

/// <summary>
/// Request body to update an inbox draft. Provide at least one of <see cref="To"/>,
/// <see cref="Cc"/>, <see cref="Bcc"/>, <see cref="Subject"/>, <see cref="TextBody"/>,
/// <see cref="HtmlBody"/> or <see cref="Attachments"/>.
/// </summary>
/// <remarks>
/// A property that is not assigned is left out of the request and keeps its current value.
/// Assigning null sends null, which clears the field. Setting <see cref="TextBody"/> or
/// <see cref="HtmlBody"/> replaces the whole body, so the body part you do not assign
/// becomes null. The merged draft must contain at least one non-empty field, and its
/// combined recipients cannot exceed 50.
/// </remarks>
/// <see href="https://resend.com/docs/api-reference/inboxes/update-draft" />
[JsonConverter( typeof( InboxDraftUpdateDataConverter ) )]
public class InboxDraftUpdateData
{
    private readonly HashSet<string> _assigned = new();
    private EmailAddressList? _to;
    private EmailAddressList? _cc;
    private EmailAddressList? _bcc;
    private string? _subject;
    private string? _html;
    private string? _text;
    private List<EmailAttachment>? _attachments;


    /// <summary>
    /// Recipient email address (list).
    /// </summary>
    /// <remarks>
    /// Each value can include a friendly name. Assign null to clear.
    /// </remarks>
    public EmailAddressList? To
    {
        get => _to;
        set { _to = value; _assigned.Add( "to" ); }
    }

    /// <summary>
    /// Cc/carbon-copy recipient email address.
    /// </summary>
    /// <remarks>
    /// Assign null or an empty list to clear.
    /// </remarks>
    public EmailAddressList? Cc
    {
        get => _cc;
        set { _cc = value; _assigned.Add( "cc" ); }
    }

    /// <summary>
    /// Bcc/blind carbon copy recipient email address.
    /// </summary>
    /// <remarks>
    /// Assign null or an empty list to clear.
    /// </remarks>
    public EmailAddressList? Bcc
    {
        get => _bcc;
        set { _bcc = value; _assigned.Add( "bcc" ); }
    }

    /// <summary>
    /// Subject.
    /// </summary>
    /// <remarks>
    /// Max 2000 characters. Must not contain a line break. Assign null to clear.
    /// </remarks>
    public string? Subject
    {
        get => _subject;
        set { _subject = value; _assigned.Add( "subject" ); }
    }

    /// <summary>
    /// The HTML version of the draft.
    /// </summary>
    /// <remarks>
    /// Assign null to clear.
    /// </remarks>
    public string? HtmlBody
    {
        get => _html;
        set { _html = value; _assigned.Add( "html" ); }
    }

    /// <summary>
    /// The plain text version of the draft.
    /// </summary>
    /// <remarks>
    /// Assign null to clear.
    /// </remarks>
    public string? TextBody
    {
        get => _text;
        set { _text = value; _assigned.Add( "text" ); }
    }


    /// <summary>
    /// Files to store on the draft.
    /// </summary>
    /// <remarks>
    /// Each one needs <see cref="EmailAttachment.Content"/>, <see cref="EmailAttachment.Filename"/>
    /// and <see cref="EmailAttachment.ContentType"/>. A path or a content identifier returns
    /// a 422. The files on a draft can total at most 25 MB. An empty file or a blocked file
    /// type returns a 422. Assigning a list replaces every attachment on the draft. Assign an
    /// empty list or null to remove them all.
    /// </remarks>
    public List<EmailAttachment>? Attachments
    {
        get => _attachments;
        set { _attachments = value; _assigned.Add( "attachments" ); }
    }


    /// <summary />
    internal bool IsAssigned( string propertyName )
    {
        return _assigned.Contains( propertyName );
    }
}
