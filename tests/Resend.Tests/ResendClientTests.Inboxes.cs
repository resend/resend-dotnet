using System.Net;

namespace Resend.Tests;

/// <summary />
public partial class ResendClientTests
{
    /// <summary />
    [Fact]
    public async Task InboxCreate()
    {
        var resp = await _resend.InboxCreateAsync( new InboxCreateData()
        {
            EmailAddress = "support@example.com",
            Name = "Customer Support",
            FromName = "Ada from Support",
            Forwarding = true,
        } );

        Assert.NotNull( resp );
        Assert.True( resp.Success );
        Assert.NotEqual( Guid.Empty, resp.Content );
    }


    /// <summary />
    [Fact]
    public async Task InboxList()
    {
        var resp = await _resend.InboxListAsync( new PaginatedQuery()
        {
            Limit = 10,
        } );

        Assert.NotNull( resp );
        Assert.True( resp.Success );
        Assert.NotNull( resp.Content );
        Assert.False( resp.Content.HasMore );
        Assert.Single( resp.Content.Data );
        Assert.Equal( "support@example.com", resp.Content.Data[ 0 ].EmailAddress );
        Assert.Null( resp.Content.Data[ 0 ].FromName );
        Assert.Equal( 3, resp.Content.Data[ 0 ].UnreadCount );
    }


    /// <summary />
    [Fact]
    public async Task InboxRetrieve()
    {
        var inboxId = Guid.NewGuid();

        var resp = await _resend.InboxRetrieveAsync( inboxId );

        Assert.NotNull( resp );
        Assert.True( resp.Success );
        Assert.NotNull( resp.Content );
        Assert.Equal( "inbox", resp.Content.Object );
        Assert.Equal( inboxId, resp.Content.Id );
        Assert.Equal( "Ada from Support", resp.Content.FromName );
        Assert.Null( resp.Content.ReceivingAddress );
        Assert.Null( resp.Content.MomentLastReceived );
        Assert.Equal( 2, resp.Content.DraftCount );
    }


    /// <summary>
    /// The path parameter accepts an email address, which must survive URL-encoding.
    /// </summary>
    [Fact]
    public async Task InboxRetrieveByEmail()
    {
        var resp = await _resend.InboxRetrieveAsync( "support+eu@example.com" );

        Assert.NotNull( resp );
        Assert.True( resp.Success );
        Assert.NotNull( resp.Content );
        Assert.Equal( "support+eu@example.com", resp.Content.EmailAddress );
    }


    /// <summary />
    [Fact]
    public async Task InboxUpdate()
    {
        var inboxId = Guid.NewGuid();

        var resp = await _resend.InboxUpdateAsync( inboxId, new InboxUpdateData()
        {
            FromName = "",
        } );

        Assert.NotNull( resp );
        Assert.True( resp.Success );
        Assert.Equal( inboxId, resp.Content );
    }


    /// <summary />
    [Fact]
    public async Task InboxUpdateRequiresAField()
    {
        var ex = await Assert.ThrowsAsync<ResendException>( () => _resend.InboxUpdateAsync( Guid.NewGuid(), new InboxUpdateData() ) );

        Assert.Equal( HttpStatusCode.UnprocessableEntity, ex.StatusCode );
        Assert.Equal( ErrorType.ValidationError, ex.ErrorType );
    }


    /// <summary />
    [Fact]
    public async Task InboxDelete()
    {
        var inboxId = Guid.NewGuid();

        var resp = await _resend.InboxDeleteAsync( inboxId );

        Assert.NotNull( resp );
        Assert.True( resp.Success );
        Assert.NotNull( resp.Content );
        Assert.Equal( inboxId, resp.Content.Id );
        Assert.True( resp.Content.Deleted );
    }


    /// <summary />
    [Fact]
    public async Task InboxAgentRetrieve()
    {
        var resp = await _resend.InboxAgentRetrieveAsync( Guid.NewGuid() );

        Assert.NotNull( resp );
        Assert.True( resp.Success );
        Assert.NotNull( resp.Content );
        Assert.Equal( "inbox_agent", resp.Content.Object );
        Assert.Null( resp.Content.Instructions );
        Assert.Equal( "friendly and concise", resp.Content.Tone );
        Assert.Equal( new[] { InboxAgentAction.DraftReply, InboxAgentAction.AddLabels }, resp.Content.EnabledActions );
    }


    /// <summary />
    [Fact]
    public async Task InboxAgentUpdate()
    {
        var resp = await _resend.InboxAgentUpdateAsync( Guid.NewGuid(), new InboxAgentUpdateData()
        {
            Instructions = null,
            EnabledActions = new List<InboxAgentAction>(),
        } );

        Assert.NotNull( resp );
        Assert.True( resp.Success );
        Assert.NotEqual( Guid.Empty, resp.Content );
    }


    /// <summary>
    /// An update without any assigned field sends an empty object, which the API rejects.
    /// </summary>
    [Fact]
    public async Task InboxAgentUpdateRequiresAField()
    {
        var ex = await Assert.ThrowsAsync<ResendException>( () => _resend.InboxAgentUpdateAsync( Guid.NewGuid(), new InboxAgentUpdateData() ) );

        Assert.Equal( HttpStatusCode.UnprocessableEntity, ex.StatusCode );
        Assert.Equal( ErrorType.ValidationError, ex.ErrorType );
    }


    /// <summary />
    [Fact]
    public async Task InboxLabelList()
    {
        var resp = await _resend.InboxLabelListAsync( Guid.NewGuid() );

        Assert.NotNull( resp );
        Assert.True( resp.Success );
        Assert.NotNull( resp.Content );
        Assert.Single( resp.Content );
        Assert.Equal( "Urgent", resp.Content[ 0 ].Name );
        Assert.Equal( "#E93D82", resp.Content[ 0 ].Color );
    }


    /// <summary />
    [Fact]
    public async Task InboxLabelCreate()
    {
        var resp = await _resend.InboxLabelCreateAsync( Guid.NewGuid(), new InboxLabelCreateData()
        {
            Name = "Urgent",
            Color = "#E93D82",
        } );

        Assert.NotNull( resp );
        Assert.True( resp.Success );
        Assert.NotEqual( Guid.Empty, resp.Content );
    }


    /// <summary />
    [Fact]
    public async Task InboxLabelUpdate()
    {
        var labelId = Guid.NewGuid();

        var resp = await _resend.InboxLabelUpdateAsync( Guid.NewGuid(), labelId, new InboxLabelUpdateData()
        {
            Color = "#F76B15",
        } );

        Assert.NotNull( resp );
        Assert.True( resp.Success );
        Assert.Equal( labelId, resp.Content );
    }


    /// <summary />
    [Fact]
    public async Task InboxLabelDelete()
    {
        var labelId = Guid.NewGuid();

        var resp = await _resend.InboxLabelDeleteAsync( Guid.NewGuid(), labelId );

        Assert.NotNull( resp );
        Assert.True( resp.Success );
        Assert.NotNull( resp.Content );
        Assert.Equal( labelId, resp.Content.Id );
        Assert.True( resp.Content.Deleted );
    }


    /// <summary />
    [Fact]
    public async Task InboxThreadList()
    {
        var resp = await _resend.InboxThreadListAsync( Guid.NewGuid(), new InboxThreadListQuery()
        {
            Folders = new List<InboxFolder>() { InboxFolder.Archive },
            IsRead = true,
            Limit = 5,
        } );

        Assert.NotNull( resp );
        Assert.True( resp.Success );
        Assert.NotNull( resp.Content );
        Assert.True( resp.Content.HasMore );

        var thread = Assert.Single( resp.Content.Data );
        Assert.Equal( InboxFolder.Archive, thread.Folder );
        Assert.True( thread.IsRead );
        Assert.Equal( "ada@example.org", thread.From!.Email );
        Assert.Equal( "Ada Lovelace", thread.From.DisplayName );
        Assert.Single( thread.Labels );
    }


    /// <summary />
    [Fact]
    public async Task InboxThreadSearch()
    {
        var resp = await _resend.InboxThreadSearchAsync( Guid.NewGuid(), new InboxThreadSearchQuery()
        {
            Query = "invoice",
            From = new List<string>() { "ada@example.org" },
            HasAttachment = true,
            StartDate = "2026-09-01",
        } );

        Assert.NotNull( resp );
        Assert.True( resp.Success );
        Assert.NotNull( resp.Content );

        var result = Assert.Single( resp.Content.Data );
        Assert.NotNull( result.MatchedEmailId );
        Assert.Equal( new[] { "September **invoice**" }, result.Highlights.Subject );
        Assert.Null( result.Highlights.Body );
    }


    /// <summary />
    [Fact]
    public async Task InboxThreadRetrieve()
    {
        var threadId = Guid.NewGuid();

        var resp = await _resend.InboxThreadRetrieveAsync( Guid.NewGuid(), threadId );

        Assert.NotNull( resp );
        Assert.True( resp.Success );
        Assert.NotNull( resp.Content );
        Assert.Equal( "inbox_thread", resp.Content.Object );
        Assert.Equal( threadId, resp.Content.Id );
        Assert.Equal( InboxFolder.Inbox, resp.Content.Folder );
    }


    /// <summary />
    [Fact]
    public async Task InboxThreadUpdate()
    {
        var threadId = Guid.NewGuid();
        var labelId = Guid.NewGuid();

        var resp = await _resend.InboxThreadUpdateAsync( Guid.NewGuid(), threadId, new InboxThreadUpdateData()
        {
            IsRead = true,
            Folder = InboxMoveFolder.Archive,
            AddLabelIds = new List<Guid>() { labelId },
        } );

        Assert.NotNull( resp );
        Assert.True( resp.Success );
        Assert.NotNull( resp.Content );
        Assert.Equal( threadId, resp.Content.Id );
        Assert.Equal( InboxFolder.Archive, resp.Content.Folder );
        Assert.True( resp.Content.IsRead );
        Assert.Equal( labelId, Assert.Single( resp.Content.Labels ).Id );
    }


    /// <summary />
    [Fact]
    public async Task InboxThreadUpdateRequiresAField()
    {
        var ex = await Assert.ThrowsAsync<ResendException>( () => _resend.InboxThreadUpdateAsync( Guid.NewGuid(), Guid.NewGuid(), new InboxThreadUpdateData() ) );

        Assert.Equal( HttpStatusCode.UnprocessableEntity, ex.StatusCode );
        Assert.Equal( ErrorType.ValidationError, ex.ErrorType );
    }


    /// <summary />
    [Fact]
    public async Task InboxThreadUpdateRejectsSameLabelAddedAndRemoved()
    {
        var labelId = Guid.NewGuid();

        var ex = await Assert.ThrowsAsync<ResendException>( () => _resend.InboxThreadUpdateAsync( Guid.NewGuid(), Guid.NewGuid(), new InboxThreadUpdateData()
        {
            AddLabelIds = new List<Guid>() { labelId },
            RemoveLabelIds = new List<Guid>() { labelId },
        } ) );

        Assert.Equal( HttpStatusCode.UnprocessableEntity, ex.StatusCode );
        Assert.Equal( ErrorType.ValidationError, ex.ErrorType );
    }


    /// <summary />
    [Fact]
    public async Task InboxThreadDelete()
    {
        var threadId = Guid.NewGuid();

        var resp = await _resend.InboxThreadDeleteAsync( Guid.NewGuid(), threadId );

        Assert.NotNull( resp );
        Assert.True( resp.Success );
        Assert.NotNull( resp.Content );
        Assert.Equal( threadId, resp.Content.Id );
        Assert.True( resp.Content.Deleted );
    }


    /// <summary />
    [Fact]
    public async Task InboxThreadEmailList()
    {
        var resp = await _resend.InboxThreadEmailListAsync( Guid.NewGuid(), Guid.NewGuid() );

        Assert.NotNull( resp );
        Assert.True( resp.Success );
        Assert.NotNull( resp.Content );

        var email = Assert.Single( resp.Content.Data );
        Assert.Equal( InboxEmailDirection.Inbound, email.Direction );
        Assert.Equal( "replies@example.org", Assert.Single( email.ReplyTo ).Email );
        Assert.Equal( "receipt.pdf", Assert.Single( email.Attachments ).Filename );
    }


    /// <summary />
    [Fact]
    public async Task InboxThreadEmailRetrieve()
    {
        var emailId = Guid.NewGuid();

        var resp = await _resend.InboxThreadEmailRetrieveAsync( Guid.NewGuid(), Guid.NewGuid(), emailId );

        Assert.NotNull( resp );
        Assert.True( resp.Success );
        Assert.NotNull( resp.Content );
        Assert.Equal( emailId, resp.Content.Id );
        Assert.Equal( "<1041@example.org>", resp.Content.MessageId );
        Assert.NotNull( resp.Content.HtmlBody );
        Assert.NotNull( resp.Content.TextBody );
    }


    /// <summary />
    [Fact]
    public async Task InboxThreadEmailReply()
    {
        var resp = await _resend.InboxThreadEmailReplyAsync( Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), new InboxThreadEmailReplyData()
        {
            TextBody = "Refund issued for order 1041.",
            ReplyAll = true,
            Attachments = new List<EmailAttachment>()
            {
                new EmailAttachment() { Filename = "note.txt", Content = "Refund issued." },
            },
        } );

        Assert.NotNull( resp );
        Assert.True( resp.Success );
        Assert.NotEqual( Guid.Empty, resp.Content );
    }


    /// <summary />
    [Fact]
    public async Task InboxThreadEmailReplyRequiresBody()
    {
        var ex = await Assert.ThrowsAsync<ResendException>( () => _resend.InboxThreadEmailReplyAsync( Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), new InboxThreadEmailReplyData()
        {
            TextBody = "  ",
        } ) );

        Assert.Equal( HttpStatusCode.UnprocessableEntity, ex.StatusCode );
        Assert.Equal( ErrorType.ValidationError, ex.ErrorType );
    }


    /// <summary />
    [Fact]
    public async Task InboxThreadEmailForward()
    {
        var resp = await _resend.InboxThreadEmailForwardAsync( Guid.NewGuid(), Guid.NewGuid(), Guid.NewGuid(), new InboxThreadEmailForwardData()
        {
            To = "colleague@example.org",
            TextBody = "Flagging this refund request for you.",
        } );

        Assert.NotNull( resp );
        Assert.True( resp.Success );
        Assert.NotEqual( Guid.Empty, resp.Content );
    }


    /// <summary />
    [Fact]
    public async Task InboxDraftList()
    {
        var resp = await _resend.InboxDraftListAsync( Guid.NewGuid() );

        Assert.NotNull( resp );
        Assert.True( resp.Success );
        Assert.NotNull( resp.Content );

        var draft = Assert.Single( resp.Content.Data );
        Assert.Equal( InboxDraftType.Standalone, draft.DraftType );
        Assert.Equal( "Refund issued for order 1041.", draft.Snippet );
        Assert.Null( draft.ThreadId );
    }


    /// <summary />
    [Fact]
    public async Task InboxDraftCreate()
    {
        var resp = await _resend.InboxDraftCreateAsync( Guid.NewGuid(), new InboxDraftCreateData()
        {
            To = "ada@example.org",
            Subject = "Refund for order 1041",
            TextBody = "Refund issued for order 1041.",
        } );

        Assert.NotNull( resp );
        Assert.True( resp.Success );
        Assert.NotEqual( Guid.Empty, resp.Content );
    }


    /// <summary />
    [Fact]
    public async Task InboxDraftCreateRejectsPathAttachment()
    {
        var ex = await Assert.ThrowsAsync<ResendException>( () => _resend.InboxDraftCreateAsync( Guid.NewGuid(), new InboxDraftCreateData()
        {
            Attachments = new List<EmailAttachment>()
            {
                new EmailAttachment() { Filename = "receipt.pdf", Path = "https://example.com/receipt.pdf" },
            },
        } ) );

        Assert.Equal( HttpStatusCode.UnprocessableEntity, ex.StatusCode );
        Assert.Equal( ErrorType.ValidationError, ex.ErrorType );
    }


    /// <summary />
    [Fact]
    public async Task InboxDraftUpdateRejectsAttachmentWithoutContentType()
    {
        var ex = await Assert.ThrowsAsync<ResendException>( () => _resend.InboxDraftUpdateAsync( Guid.NewGuid(), Guid.NewGuid(), new InboxDraftUpdateData()
        {
            Attachments = new List<EmailAttachment>()
            {
                new EmailAttachment() { Filename = "note.txt", Content = "hello" },
            },
        } ) );

        Assert.Equal( HttpStatusCode.UnprocessableEntity, ex.StatusCode );
        Assert.Equal( ErrorType.ValidationError, ex.ErrorType );
    }


    /// <summary />
    [Fact]
    public async Task InboxDraftUpdateReplacesAttachments()
    {
        var resp = await _resend.InboxDraftUpdateAsync( Guid.NewGuid(), Guid.NewGuid(), new InboxDraftUpdateData()
        {
            Attachments = new List<EmailAttachment>()
            {
                new EmailAttachment() { Filename = "note.txt", Content = "hello", ContentType = "text/plain" },
            },
        } );

        Assert.NotNull( resp );
        Assert.True( resp.Success );
        var attachment = Assert.Single( resp.Content.Attachments );
        Assert.Equal( "note.txt", attachment.Filename );
        Assert.Equal( "text/plain", attachment.ContentType );
    }


    /// <summary />
    [Fact]
    public async Task InboxDraftUpdateEmptyAttachmentsRemovesAll()
    {
        var resp = await _resend.InboxDraftUpdateAsync( Guid.NewGuid(), Guid.NewGuid(), new InboxDraftUpdateData()
        {
            Attachments = new List<EmailAttachment>(),
        } );

        Assert.NotNull( resp );
        Assert.True( resp.Success );
        Assert.Empty( resp.Content.Attachments );
    }


    /// <summary />
    [Fact]
    public async Task InboxDraftCreateReplyNeedsBothIds()
    {
        var ex = await Assert.ThrowsAsync<ResendException>( () => _resend.InboxDraftCreateAsync( Guid.NewGuid(), new InboxDraftCreateData()
        {
            ThreadId = Guid.NewGuid(),
            TextBody = "Refund issued for order 1041.",
        } ) );

        Assert.Equal( HttpStatusCode.UnprocessableEntity, ex.StatusCode );
        Assert.Equal( ErrorType.ValidationError, ex.ErrorType );
    }


    /// <summary />
    [Fact]
    public async Task InboxDraftRetrieve()
    {
        var draftId = Guid.NewGuid();

        var resp = await _resend.InboxDraftRetrieveAsync( Guid.NewGuid(), draftId );

        Assert.NotNull( resp );
        Assert.True( resp.Success );
        Assert.NotNull( resp.Content );
        Assert.Equal( "inbox_draft", resp.Content.Object );
        Assert.Equal( draftId, resp.Content.Id );
        Assert.Null( resp.Content.EmailId );

        var attachment = Assert.Single( resp.Content.Attachments );
        Assert.Equal( "receipt.pdf", attachment.Filename );
        Assert.Equal( "application/pdf", attachment.ContentType );
        Assert.Equal( 20841, attachment.FileSize );
    }


    /// <summary>
    /// A null assignment reaches the API as an explicit null, which clears the field.
    /// </summary>
    [Fact]
    public async Task InboxDraftUpdate()
    {
        var draftId = Guid.NewGuid();

        var resp = await _resend.InboxDraftUpdateAsync( Guid.NewGuid(), draftId, new InboxDraftUpdateData()
        {
            To = null,
            Subject = null,
        } );

        Assert.NotNull( resp );
        Assert.True( resp.Success );
        Assert.NotNull( resp.Content );
        Assert.Equal( draftId, resp.Content.Id );
        Assert.Null( resp.Content.To );
        Assert.Null( resp.Content.Subject );
        Assert.NotNull( resp.Content.TextBody );
    }


    /// <summary />
    [Fact]
    public async Task InboxDraftDelete()
    {
        var draftId = Guid.NewGuid();

        var resp = await _resend.InboxDraftDeleteAsync( Guid.NewGuid(), draftId );

        Assert.NotNull( resp );
        Assert.True( resp.Success );
        Assert.NotNull( resp.Content );
        Assert.Equal( draftId, resp.Content.Id );
        Assert.True( resp.Content.Deleted );
    }


    /// <summary />
    [Fact]
    public async Task InboxDraftSend()
    {
        var draftId = Guid.NewGuid();

        var resp = await _resend.InboxDraftSendAsync( Guid.NewGuid(), draftId );

        Assert.NotNull( resp );
        Assert.True( resp.Success );
        Assert.NotEqual( Guid.Empty, resp.Content );
        Assert.NotEqual( draftId, resp.Content );
    }
}
