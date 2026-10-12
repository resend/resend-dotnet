using Microsoft.AspNetCore.Mvc;
using Resend.Payloads;
using System.Net;
using System.Text.Json;

namespace Resend.ApiServer.Controllers;

/// <summary />
[ApiController]
public class InboxController : ControllerBase
{
    private readonly ILogger<InboxController> _logger;


    /// <summary />
    public InboxController( ILogger<InboxController> logger )
    {
        _logger = logger;
    }


    /// <summary />
    [HttpPost]
    [Route( "inboxes" )]
    public ObjectId InboxCreate( [FromBody] InboxCreateData request )
    {
        _logger.LogDebug( "InboxCreate" );

        return new ObjectId()
        {
            Object = "inbox",
            Id = Guid.NewGuid(),
        };
    }


    /// <summary />
    [HttpGet]
    [Route( "inboxes" )]
    public PaginatedResult<InboxSummary> InboxList()
    {
        _logger.LogDebug( "InboxList" );

        var list = new List<InboxSummary>();
        list.Add( new InboxSummary()
        {
            Id = Guid.NewGuid(),
            Name = "Customer Support",
            EmailAddress = "support@example.com",
            FromName = null,
            UnreadCount = 3,
            MomentLastReceived = DateTime.UtcNow,
        } );

        return new PaginatedResult<InboxSummary>() { HasMore = false, Data = list };
    }


    /// <summary />
    [HttpGet]
    [Route( "inboxes/{inbox}" )]
    public Inbox InboxRetrieve( [FromRoute] string inbox )
    {
        _logger.LogDebug( "InboxRetrieve" );

        var isId = Guid.TryParse( inbox, out var id );

        return new Inbox()
        {
            Object = "inbox",
            Id = isId == true ? id : Guid.NewGuid(),
            Name = "Customer Support",
            EmailAddress = isId == true ? "support@example.com" : inbox,
            DomainId = Guid.NewGuid(),
            ReceivingAddress = null,
            FromName = "Ada from Support",
            UnreadCount = 3,
            DraftCount = 2,
            MomentLastReceived = null,
            MomentCreated = DateTime.UtcNow,
        };
    }


    /// <summary />
    [HttpPatch]
    [Route( "inboxes/{id:guid}" )]
    public ActionResult<ObjectId> InboxUpdate( [FromRoute] Guid id, [FromBody] InboxUpdateData request )
    {
        _logger.LogDebug( "InboxUpdate" );

        if ( request.Name == null && request.FromName == null )
            return Invalid( "At least one of `name` or `from_name` is required." );

        return new ObjectId()
        {
            Object = "inbox",
            Id = id,
        };
    }


    /// <summary />
    [HttpDelete]
    [Route( "inboxes/{id:guid}" )]
    public InboxDeleteResult InboxDelete( [FromRoute] Guid id )
    {
        _logger.LogDebug( "InboxDelete" );

        return new InboxDeleteResult()
        {
            Object = "inbox",
            Id = id,
            Deleted = true,
        };
    }


    /// <summary />
    [HttpGet]
    [Route( "inboxes/{id:guid}/agent" )]
    public InboxAgent InboxAgentRetrieve( [FromRoute] Guid id )
    {
        _logger.LogDebug( "InboxAgentRetrieve" );

        return new InboxAgent()
        {
            Object = "inbox_agent",
            Instructions = null,
            Tone = "friendly and concise",
            EnabledActions = new List<InboxAgentAction>() { InboxAgentAction.DraftReply, InboxAgentAction.AddLabels },
        };
    }


    /// <summary />
    [HttpPatch]
    [Route( "inboxes/{id:guid}/agent" )]
    public ActionResult<ObjectId> InboxAgentUpdate( [FromRoute] Guid id, [FromBody] JsonElement request )
    {
        _logger.LogDebug( "InboxAgentUpdate" );

        var fields = new[] { "instructions", "tone", "enabled_actions" };

        if ( request.ValueKind != JsonValueKind.Object || fields.Any( x => request.TryGetProperty( x, out _ ) ) == false )
            return Invalid( "At least one of `instructions`, `tone`, or `enabled_actions` is required." );

        return new ObjectId()
        {
            Object = "inbox_agent",
            Id = AgentId( id ),
        };
    }


    /// <summary />
    [HttpGet]
    [Route( "inboxes/{id:guid}/labels" )]
    public ListOf<InboxLabel> InboxLabelList( [FromRoute] Guid id )
    {
        _logger.LogDebug( "InboxLabelList" );

        var list = new List<InboxLabel>();
        list.Add( new InboxLabel()
        {
            Id = Guid.NewGuid(),
            Name = "Urgent",
            Color = "#E93D82",
            MomentCreated = DateTime.UtcNow,
        } );

        return new ListOf<InboxLabel>() { Data = list };
    }


    /// <summary />
    [HttpPost]
    [Route( "inboxes/{id:guid}/labels" )]
    public ObjectId InboxLabelCreate( [FromRoute] Guid id, [FromBody] InboxLabelCreateData request )
    {
        _logger.LogDebug( "InboxLabelCreate" );

        return new ObjectId()
        {
            Object = "inbox_label",
            Id = Guid.NewGuid(),
        };
    }


    /// <summary />
    [HttpPatch]
    [Route( "inboxes/{id:guid}/labels/{labelId:guid}" )]
    public ObjectId InboxLabelUpdate( [FromRoute] Guid id, [FromRoute] Guid labelId, [FromBody] InboxLabelUpdateData request )
    {
        _logger.LogDebug( "InboxLabelUpdate" );

        return new ObjectId()
        {
            Object = "inbox_label",
            Id = labelId,
        };
    }


    /// <summary />
    [HttpDelete]
    [Route( "inboxes/{id:guid}/labels/{labelId:guid}" )]
    public InboxLabelDeleteResult InboxLabelDelete( [FromRoute] Guid id, [FromRoute] Guid labelId )
    {
        _logger.LogDebug( "InboxLabelDelete" );

        return new InboxLabelDeleteResult()
        {
            Object = "inbox_label",
            Id = labelId,
            Deleted = true,
        };
    }


    /// <summary />
    [HttpGet]
    [Route( "inboxes/{id:guid}/threads" )]
    public PaginatedResult<InboxThread> InboxThreadList( [FromRoute] Guid id, [FromQuery] string? folders = null, [FromQuery] string? read = null )
    {
        _logger.LogDebug( "InboxThreadList" );

        var list = new List<InboxThread>();
        list.Add( Thread( folders, read ) );

        return new PaginatedResult<InboxThread>() { HasMore = true, Data = list };
    }


    /// <summary />
    [HttpGet]
    [Route( "inboxes/{id:guid}/threads/search" )]
    public PaginatedResult<InboxThreadSearchResult> InboxThreadSearch( [FromRoute] Guid id, [FromQuery] string? query = null, [FromQuery] string? folders = null, [FromQuery] string? read = null )
    {
        _logger.LogDebug( "InboxThreadSearch" );

        var thread = Thread( folders, read );

        var list = new List<InboxThreadSearchResult>();
        list.Add( new InboxThreadSearchResult()
        {
            Id = thread.Id,
            Subject = thread.Subject,
            From = thread.From,
            To = thread.To,
            Labels = thread.Labels,
            MessageCount = thread.MessageCount,
            HasAttachment = thread.HasAttachment,
            IsRead = thread.IsRead,
            MomentReceived = thread.MomentReceived,
            Folder = thread.Folder,
            MatchedEmailId = Guid.NewGuid(),
            Highlights = new InboxThreadSearchHighlights()
            {
                Subject = query == null ? null : new List<string>() { $"September **{query}**" },
            },
        } );

        return new PaginatedResult<InboxThreadSearchResult>() { HasMore = false, Data = list };
    }


    /// <summary />
    [HttpGet]
    [Route( "inboxes/{id:guid}/threads/{threadId:guid}" )]
    public InboxThreadSummary InboxThreadRetrieve( [FromRoute] Guid id, [FromRoute] Guid threadId )
    {
        _logger.LogDebug( "InboxThreadRetrieve" );

        return ThreadSummary( threadId, InboxFolder.Inbox, false );
    }


    /// <summary />
    [HttpPatch]
    [Route( "inboxes/{id:guid}/threads/{threadId:guid}" )]
    public ActionResult<InboxThreadSummary> InboxThreadUpdate( [FromRoute] Guid id, [FromRoute] Guid threadId, [FromBody] InboxThreadUpdateData request )
    {
        _logger.LogDebug( "InboxThreadUpdate" );

#pragma warning disable CS0618
        var labelId = request.LabelId;
#pragma warning restore CS0618

        if ( request.IsRead == null && request.Folder == null && labelId == null && request.AddLabelIds == null && request.RemoveLabelIds == null )
            return Invalid( "At least one of `read`, `folder`, `label_id`, `add_label_ids`, or `remove_label_ids` is required." );

        var add = ( request.AddLabelIds ?? new List<Guid>() ).Concat( labelId.HasValue == true ? new[] { labelId.Value } : Array.Empty<Guid>() ).Distinct().ToList();
        var remove = request.RemoveLabelIds ?? new List<Guid>();

        if ( add.Intersect( remove ).Any() == true )
            return Invalid( "A label ID cannot be both added and removed in the same request." );

        var folder = request.Folder switch
        {
            InboxMoveFolder.Archive => InboxFolder.Archive,
            InboxMoveFolder.Spam => InboxFolder.Spam,
            InboxMoveFolder.Trash => InboxFolder.Trash,
            _ => InboxFolder.Inbox,
        };

        var summary = ThreadSummary( threadId, folder, request.IsRead ?? false );
        summary.Labels = add.Select( x => new InboxThreadLabel() { Id = x, Name = "Urgent", Color = "#E93D82" } ).ToList();

        return summary;
    }


    /// <summary />
    [HttpDelete]
    [Route( "inboxes/{id:guid}/threads/{threadId:guid}" )]
    public InboxThreadDeleteResult InboxThreadDelete( [FromRoute] Guid id, [FromRoute] Guid threadId )
    {
        _logger.LogDebug( "InboxThreadDelete" );

        return new InboxThreadDeleteResult()
        {
            Object = "inbox_thread",
            Id = threadId,
            Deleted = true,
        };
    }


    /// <summary />
    [HttpGet]
    [Route( "inboxes/{id:guid}/threads/{threadId:guid}/emails" )]
    public PaginatedResult<InboxThreadEmail> InboxThreadEmailList( [FromRoute] Guid id, [FromRoute] Guid threadId )
    {
        _logger.LogDebug( "InboxThreadEmailList" );

        var list = new List<InboxThreadEmail>();
        list.Add( ThreadEmail( Guid.NewGuid() ) );

        return new PaginatedResult<InboxThreadEmail>() { HasMore = false, Data = list };
    }


    /// <summary />
    [HttpGet]
    [Route( "inboxes/{id:guid}/threads/{threadId:guid}/emails/{emailId:guid}" )]
    public InboxThreadEmail InboxThreadEmailRetrieve( [FromRoute] Guid id, [FromRoute] Guid threadId, [FromRoute] Guid emailId )
    {
        _logger.LogDebug( "InboxThreadEmailRetrieve" );

        return ThreadEmail( emailId );
    }


    /// <summary />
    [HttpPost]
    [Route( "inboxes/{id:guid}/threads/{threadId:guid}/emails/{emailId:guid}/reply" )]
    public ActionResult<ObjectId> InboxThreadEmailReply( [FromRoute] Guid id, [FromRoute] Guid threadId, [FromRoute] Guid emailId, [FromBody] InboxThreadEmailReplyData request )
    {
        _logger.LogDebug( "InboxThreadEmailReply" );

        if ( string.IsNullOrWhiteSpace( request.HtmlBody ) == true && string.IsNullOrWhiteSpace( request.TextBody ) == true )
            return Invalid( "At least one of `html` or `text` must contain more than whitespace." );

        return new ObjectId()
        {
            Id = Guid.NewGuid(),
        };
    }


    /// <summary />
    [HttpPost]
    [Route( "inboxes/{id:guid}/threads/{threadId:guid}/emails/{emailId:guid}/forward" )]
    public ActionResult<ObjectId> InboxThreadEmailForward( [FromRoute] Guid id, [FromRoute] Guid threadId, [FromRoute] Guid emailId, [FromBody] InboxThreadEmailForwardData request )
    {
        _logger.LogDebug( "InboxThreadEmailForward" );

        if ( request.To.Count == 0 )
            return Invalid( "The `to` field must name at least one address." );

        return new ObjectId()
        {
            Id = Guid.NewGuid(),
        };
    }


    /// <summary />
    [HttpGet]
    [Route( "inboxes/{id:guid}/drafts" )]
    public PaginatedResult<InboxDraftSummary> InboxDraftList( [FromRoute] Guid id )
    {
        _logger.LogDebug( "InboxDraftList" );

        var list = new List<InboxDraftSummary>();
        list.Add( new InboxDraftSummary()
        {
            Id = Guid.NewGuid(),
            DraftType = InboxDraftType.Standalone,
            To = new EmailAddressList() { "ada@example.org" },
            Subject = "Refund for order 1041",
            Snippet = "Refund issued for order 1041.",
            MomentUpdated = DateTime.UtcNow,
        } );

        return new PaginatedResult<InboxDraftSummary>() { HasMore = false, Data = list };
    }


    /// <summary />
    [HttpPost]
    [Route( "inboxes/{id:guid}/drafts" )]
    public ActionResult<ObjectId> InboxDraftCreate( [FromRoute] Guid id, [FromBody] InboxDraftCreateData request )
    {
        _logger.LogDebug( "InboxDraftCreate" );

        if ( request.ThreadId.HasValue != request.ReplyToEmailId.HasValue )
            return Invalid( "`thread_id` and `reply_to_email_id` must be provided together." );

        return StatusCode( (int) HttpStatusCode.Created, new ObjectId()
        {
            Object = "inbox_draft",
            Id = Guid.NewGuid(),
        } );
    }


    /// <summary />
    [HttpGet]
    [Route( "inboxes/{id:guid}/drafts/{draftId:guid}" )]
    public InboxDraft InboxDraftRetrieve( [FromRoute] Guid id, [FromRoute] Guid draftId )
    {
        _logger.LogDebug( "InboxDraftRetrieve" );

        return Draft( draftId );
    }


    /// <summary />
    [HttpPatch]
    [Route( "inboxes/{id:guid}/drafts/{draftId:guid}" )]
    public ActionResult<InboxDraft> InboxDraftUpdate( [FromRoute] Guid id, [FromRoute] Guid draftId, [FromBody] JsonElement request )
    {
        _logger.LogDebug( "InboxDraftUpdate" );

        if ( request.ValueKind != JsonValueKind.Object )
            return Invalid( "At least one draft content field is required." );

        var data = request.Deserialize<InboxDraftUpdateData>()!;
        var fields = new[] { "to", "cc", "bcc", "subject", "text", "html" };

        if ( fields.Any( x => request.TryGetProperty( x, out _ ) ) == false )
            return Invalid( "At least one draft content field is required." );

        var draft = Draft( draftId );

        if ( request.TryGetProperty( "to", out _ ) == true )
            draft.To = data.To;

        if ( request.TryGetProperty( "cc", out _ ) == true )
            draft.Cc = data.Cc ?? new EmailAddressList();

        if ( request.TryGetProperty( "bcc", out _ ) == true )
            draft.Bcc = data.Bcc ?? new EmailAddressList();

        if ( request.TryGetProperty( "subject", out _ ) == true )
            draft.Subject = data.Subject;

        if ( request.TryGetProperty( "text", out _ ) == true || request.TryGetProperty( "html", out _ ) == true )
        {
            draft.TextBody = data.TextBody;
            draft.HtmlBody = data.HtmlBody;
        }

        return draft;
    }


    /// <summary />
    [HttpDelete]
    [Route( "inboxes/{id:guid}/drafts/{draftId:guid}" )]
    public InboxDraftDeleteResult InboxDraftDelete( [FromRoute] Guid id, [FromRoute] Guid draftId )
    {
        _logger.LogDebug( "InboxDraftDelete" );

        return new InboxDraftDeleteResult()
        {
            Object = "inbox_draft",
            Id = draftId,
            Deleted = true,
        };
    }


    /// <summary />
    [HttpPost]
    [Route( "inboxes/{id:guid}/drafts/{draftId:guid}/send" )]
    public ObjectId InboxDraftSend( [FromRoute] Guid id, [FromRoute] Guid draftId )
    {
        _logger.LogDebug( "InboxDraftSend" );

        return new ObjectId()
        {
            Id = Guid.NewGuid(),
        };
    }


    /// <summary />
    private UnprocessableEntityObjectResult Invalid( string message )
    {
        return UnprocessableEntity( new ErrorResponse()
        {
            StatusCode = (int) HttpStatusCode.UnprocessableEntity,
            ErrorType = ErrorType.ValidationError,
            Message = message,
        } );
    }


    /// <summary>
    /// The agent has its own identifier, distinct from the inbox identifier.
    /// </summary>
    private static Guid AgentId( Guid inboxId )
    {
        var bytes = inboxId.ToByteArray();
        bytes[ 0 ] ^= 0xFF;

        return new Guid( bytes );
    }


    /// <summary />
    private static InboxThread Thread( string? folders, string? read )
    {
        var folder = folders?.Split( ',' ).First() switch
        {
            "archive" => InboxFolder.Archive,
            "spam" => InboxFolder.Spam,
            "sent" => InboxFolder.Sent,
            "trash" => InboxFolder.Trash,
            _ => InboxFolder.Inbox,
        };

        return new InboxThread()
        {
            Id = Guid.NewGuid(),
            Subject = "Refund for order 1041",
            From = "Ada Lovelace <ada@example.org>",
            To = new EmailAddressList() { "support@example.com" },
            Labels = new List<InboxThreadLabel>()
            {
                new InboxThreadLabel() { Id = Guid.NewGuid(), Name = "Urgent", Color = "#E93D82" },
            },
            MessageCount = 2,
            HasAttachment = true,
            HasDraft = false,
            IsRead = read == "true",
            MomentReceived = DateTime.UtcNow,
            Folder = folder,
        };
    }


    /// <summary />
    private static InboxThreadSummary ThreadSummary( Guid threadId, InboxFolder folder, bool read )
    {
        return new InboxThreadSummary()
        {
            Object = "inbox_thread",
            Id = threadId,
            Subject = "Refund for order 1041",
            Folder = folder,
            IsRead = read,
        };
    }


    /// <summary />
    private static InboxThreadEmail ThreadEmail( Guid emailId )
    {
        return new InboxThreadEmail()
        {
            Id = emailId,
            Direction = InboxEmailDirection.Inbound,
            From = "Ada Lovelace <ada@example.org>",
            To = new EmailAddressList() { "support@example.com" },
            ReplyTo = new EmailAddressList() { "replies@example.org" },
            Subject = "Refund for order 1041",
            MessageId = "<1041@example.org>",
            HtmlBody = "<p>Could I get a refund for order 1041?</p>",
            TextBody = "Could I get a refund for order 1041?",
            Attachments = new List<InboxThreadEmailAttachment>()
            {
                new InboxThreadEmailAttachment() { Id = Guid.NewGuid().ToString(), Filename = "receipt.pdf", FileSize = 20841 },
            },
            IsRead = false,
            MomentReceived = DateTime.UtcNow,
        };
    }


    /// <summary />
    private static InboxDraft Draft( Guid draftId )
    {
        return new InboxDraft()
        {
            Object = "inbox_draft",
            Id = draftId,
            DraftType = InboxDraftType.Standalone,
            To = new EmailAddressList() { "ada@example.org" },
            Subject = "Refund for order 1041",
            HtmlBody = "<p>Refund issued for order 1041.</p>",
            TextBody = "Refund issued for order 1041.",
            MomentCreated = DateTime.UtcNow,
            MomentUpdated = DateTime.UtcNow,
        };
    }
}
