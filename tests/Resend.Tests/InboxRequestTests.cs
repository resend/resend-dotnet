using System.Net;
using System.Text;
using System.Text.Json;

namespace Resend.Tests;

/// <summary>
/// Tests over the requests issued by the inbox methods, against a handler which records
/// instead of answering like the API.
/// </summary>
public class InboxRequestTests
{
    private static readonly Guid InboxId = Guid.Parse( "b3e2b2b6-3f0e-4c8e-9ad3-2f43a1e2c7f1" );
    private static readonly Guid ThreadId = Guid.Parse( "4d8e2a1c-9b3f-4c6d-8a21-3e5f7c9c0d12" );
    private static readonly Guid EmailId = Guid.Parse( "5b1a9f47-2c8d-4e6f-9a03-1d2e3f4a5b60" );
    private static readonly Guid DraftId = Guid.Parse( "c3a1e8b4-2d5f-4a7c-9e10-6b8d7f5a4c32" );
    private static readonly Guid LabelId = Guid.Parse( "7f9c1d2e-4a6b-4c3d-8e15-9b0a7c6d5e34" );

    private readonly RecordingHandler _handler;
    private readonly IResend _resend;


    /// <summary />
    public InboxRequestTests()
    {
        _handler = new RecordingHandler();

        var opt = new ResendClientOptions()
        {
            ApiToken = "re_test_123",
        };

        _resend = ResendClient.Create( opt, new HttpClient( _handler ) );
    }


    /// <summary />
    [Fact]
    public async Task InboxRetrieve_ByEmail_Escaped()
    {
        await _resend.InboxRetrieveAsync( "support+eu@example.com" );

        var req = Assert.Single( _handler.Requests );
        Assert.Equal( HttpMethod.Get, req.Method );
        Assert.Equal( "/inboxes/support%2Beu%40example.com", req.RequestUri!.AbsolutePath );
    }


    /// <summary />
    [Theory]
    [InlineData( "" )]
    [InlineData( "  " )]
    public async Task InboxRetrieve_Blank_Throws( string inboxIdOrEmail )
    {
        await Assert.ThrowsAsync<ArgumentException>( () => _resend.InboxRetrieveAsync( inboxIdOrEmail ) );

        Assert.Empty( _handler.Requests );
    }


    /// <summary />
    [Fact]
    public async Task InboxList_NoQuery()
    {
        await _resend.InboxListAsync();

        var req = Assert.Single( _handler.Requests );
        Assert.Equal( "/inboxes", req.RequestUri!.PathAndQuery );
    }


    /// <summary />
    [Fact]
    public async Task InboxCreate_Body()
    {
        await _resend.InboxCreateAsync( new InboxCreateData()
        {
            EmailAddress = "support@example.com",
            Forwarding = true,
        } );

        var body = Assert.Single( _handler.Bodies ).RootElement;
        Assert.Equal( "support@example.com", body.GetProperty( "email_address" ).GetString() );
        Assert.True( body.GetProperty( "forwarding" ).GetBoolean() );
        Assert.False( body.TryGetProperty( "name", out _ ) );
        Assert.False( body.TryGetProperty( "from_name", out _ ) );
    }


    /// <summary>
    /// An empty from name removes it, so it must reach the API as an empty string.
    /// </summary>
    [Fact]
    public async Task InboxUpdate_EmptyFromName_Sent()
    {
        await _resend.InboxUpdateAsync( InboxId, new InboxUpdateData()
        {
            FromName = "",
        } );

        var req = Assert.Single( _handler.Requests );
        Assert.Equal( HttpMethod.Patch, req.Method );
        Assert.Equal( $"/inboxes/{InboxId}", req.RequestUri!.AbsolutePath );

        var body = Assert.Single( _handler.Bodies ).RootElement;
        Assert.Equal( "", body.GetProperty( "from_name" ).GetString() );
        Assert.False( body.TryGetProperty( "name", out _ ) );
    }


    /// <summary />
    [Fact]
    public async Task InboxAgentUpdate_NullClears_UnassignedOmitted()
    {
        await _resend.InboxAgentUpdateAsync( InboxId, new InboxAgentUpdateData()
        {
            Instructions = null,
        } );

        var body = Assert.Single( _handler.Bodies ).RootElement;
        Assert.Equal( JsonValueKind.Null, body.GetProperty( "instructions" ).ValueKind );
        Assert.False( body.TryGetProperty( "tone", out _ ) );
        Assert.False( body.TryGetProperty( "enabled_actions", out _ ) );
    }


    /// <summary />
    [Fact]
    public async Task InboxAgentUpdate_Values()
    {
        await _resend.InboxAgentUpdateAsync( InboxId, new InboxAgentUpdateData()
        {
            Tone = "friendly and concise",
            EnabledActions = new List<InboxAgentAction>() { InboxAgentAction.DraftReply, InboxAgentAction.MarkAsSpam },
        } );

        var body = Assert.Single( _handler.Bodies ).RootElement;
        Assert.False( body.TryGetProperty( "instructions", out _ ) );
        Assert.Equal( "friendly and concise", body.GetProperty( "tone" ).GetString() );
        Assert.Equal( new[] { "draft_reply", "mark_as_spam" }, body.GetProperty( "enabled_actions" ).EnumerateArray().Select( x => x.GetString() ) );
    }


    /// <summary>
    /// The API rejects a null action set, so a null assignment is left out of the request.
    /// </summary>
    [Fact]
    public async Task InboxAgentUpdate_NullActions_Omitted()
    {
        await _resend.InboxAgentUpdateAsync( InboxId, new InboxAgentUpdateData()
        {
            Tone = null,
            EnabledActions = null,
        } );

        var body = Assert.Single( _handler.Bodies ).RootElement;
        Assert.Equal( JsonValueKind.Null, body.GetProperty( "tone" ).ValueKind );
        Assert.False( body.TryGetProperty( "enabled_actions", out _ ) );
    }


    /// <summary />
    [Fact]
    public async Task InboxAgentUpdate_EmptyActions_Sent()
    {
        await _resend.InboxAgentUpdateAsync( InboxId, new InboxAgentUpdateData()
        {
            EnabledActions = new List<InboxAgentAction>(),
        } );

        var body = Assert.Single( _handler.Bodies ).RootElement;
        Assert.Equal( 0, body.GetProperty( "enabled_actions" ).GetArrayLength() );
    }


    /// <summary />
    [Fact]
    public async Task InboxLabelUpdate_Path_Body()
    {
        await _resend.InboxLabelUpdateAsync( InboxId, LabelId, new InboxLabelUpdateData()
        {
            Color = "#F76B15",
        } );

        var req = Assert.Single( _handler.Requests );
        Assert.Equal( $"/inboxes/{InboxId}/labels/{LabelId}", req.RequestUri!.AbsolutePath );

        var body = Assert.Single( _handler.Bodies ).RootElement;
        Assert.Equal( "#F76B15", body.GetProperty( "color" ).GetString() );
        Assert.False( body.TryGetProperty( "name", out _ ) );
    }


    /// <summary />
    [Fact]
    public async Task InboxThreadList_Query()
    {
        await _resend.InboxThreadListAsync( InboxId, new InboxThreadListQuery()
        {
            Folders = new List<InboxFolder>() { InboxFolder.Inbox, InboxFolder.Sent },
            Labels = new List<Guid>() { LabelId },
            IsRead = false,
            Limit = 10,
            After = ThreadId.ToString(),
        } );

        var req = Assert.Single( _handler.Requests );
        Assert.Equal( $"/inboxes/{InboxId}/threads", req.RequestUri!.AbsolutePath );

        var qs = Query( req );
        Assert.Equal( "inbox,sent", qs[ "folders" ] );
        Assert.Equal( LabelId.ToString(), qs[ "labels" ] );
        Assert.Equal( "false", qs[ "read" ] );
        Assert.Equal( "10", qs[ "limit" ] );
        Assert.Equal( ThreadId.ToString(), qs[ "after" ] );
    }


    /// <summary />
    [Fact]
    public async Task InboxThreadList_NoQuery()
    {
        await _resend.InboxThreadListAsync( InboxId );

        var req = Assert.Single( _handler.Requests );
        Assert.Equal( $"/inboxes/{InboxId}/threads", req.RequestUri!.PathAndQuery );
    }


    /// <summary />
    [Fact]
    public async Task InboxThreadSearch_Query()
    {
        await _resend.InboxThreadSearchAsync( InboxId, new InboxThreadSearchQuery()
        {
            Query = "invoice -draft",
            From = new List<string>() { "isabella@example.com", "carolina@example.com" },
            To = new List<string>() { "support@example.com" },
            HasAttachment = true,
            StartDate = "2026-09-01",
            EndDate = "2026-09-30",
            IsRead = true,
        } );

        var req = Assert.Single( _handler.Requests );
        Assert.Equal( $"/inboxes/{InboxId}/threads/search", req.RequestUri!.AbsolutePath );

        var qs = Query( req );
        Assert.Equal( "invoice -draft", qs[ "query" ] );
        Assert.Equal( "isabella@example.com,carolina@example.com", qs[ "from" ] );
        Assert.Equal( "support@example.com", qs[ "to" ] );
        Assert.Equal( "true", qs[ "has_attachment" ] );
        Assert.Equal( "2026-09-01", qs[ "start_date" ] );
        Assert.Equal( "2026-09-30", qs[ "end_date" ] );
        Assert.Equal( "true", qs[ "read" ] );
        Assert.False( qs.ContainsKey( "cc" ) );
        Assert.False( qs.ContainsKey( "folders" ) );
    }


    /// <summary />
    [Fact]
    public async Task InboxThreadUpdate_Body()
    {
        await _resend.InboxThreadUpdateAsync( InboxId, ThreadId, new InboxThreadUpdateData()
        {
            Folder = InboxMoveFolder.Archive,
            AddLabelIds = new List<Guid>() { LabelId },
        } );

        var req = Assert.Single( _handler.Requests );
        Assert.Equal( HttpMethod.Patch, req.Method );
        Assert.Equal( $"/inboxes/{InboxId}/threads/{ThreadId}", req.RequestUri!.AbsolutePath );

        var body = Assert.Single( _handler.Bodies ).RootElement;
        Assert.Equal( "archive", body.GetProperty( "folder" ).GetString() );
        Assert.Equal( LabelId, body.GetProperty( "add_label_ids" )[ 0 ].GetGuid() );
        Assert.False( body.TryGetProperty( "read", out _ ) );
        Assert.False( body.TryGetProperty( "label_id", out _ ) );
        Assert.False( body.TryGetProperty( "remove_label_ids", out _ ) );
    }


    /// <summary />
    [Theory]
    [InlineData( InboxMoveFolder.Inbox, "inbox" )]
    [InlineData( InboxMoveFolder.Archive, "archive" )]
    [InlineData( InboxMoveFolder.Spam, "spam" )]
    [InlineData( InboxMoveFolder.Trash, "trash" )]
    public async Task InboxThreadUpdate_Folder( InboxMoveFolder folder, string expected )
    {
        await _resend.InboxThreadUpdateAsync( InboxId, ThreadId, new InboxThreadUpdateData()
        {
            Folder = folder,
        } );

        var body = Assert.Single( _handler.Bodies ).RootElement;
        Assert.Equal( expected, body.GetProperty( "folder" ).GetString() );
    }


    /// <summary />
    [Fact]
    public void InboxMoveFolder_HasNoSent()
    {
        Assert.DoesNotContain( "Sent", Enum.GetNames<InboxMoveFolder>() );
    }


    /// <summary />
    [Fact]
    public async Task InboxThreadEmailReply_Attachments()
    {
        await _resend.InboxThreadEmailReplyAsync( InboxId, ThreadId, EmailId, new InboxThreadEmailReplyData()
        {
            TextBody = "Refund issued.",
            Attachments = new List<EmailAttachment>()
            {
                new EmailAttachment() { Filename = "note.txt", Content = Encoding.UTF8.GetBytes( "hello" ) },
            },
        } );

        var req = Assert.Single( _handler.Requests );
        Assert.Equal( HttpMethod.Post, req.Method );
        Assert.Equal( $"/inboxes/{InboxId}/threads/{ThreadId}/emails/{EmailId}/reply", req.RequestUri!.AbsolutePath );

        var body = Assert.Single( _handler.Bodies ).RootElement;
        Assert.Equal( "Refund issued.", body.GetProperty( "text" ).GetString() );
        Assert.False( body.TryGetProperty( "html", out _ ) );
        Assert.False( body.TryGetProperty( "reply_all", out _ ) );

        var attachment = Assert.Single( body.GetProperty( "attachments" ).EnumerateArray() );
        Assert.Equal( "note.txt", attachment.GetProperty( "filename" ).GetString() );
        Assert.Equal( Convert.ToBase64String( Encoding.UTF8.GetBytes( "hello" ) ), attachment.GetProperty( "content" ).GetString() );
    }


    /// <summary />
    [Fact]
    public async Task InboxThreadEmailForward_Body()
    {
        await _resend.InboxThreadEmailForwardAsync( InboxId, ThreadId, EmailId, new InboxThreadEmailForwardData()
        {
            To = "colleague@example.org",
        } );

        var req = Assert.Single( _handler.Requests );
        Assert.Equal( $"/inboxes/{InboxId}/threads/{ThreadId}/emails/{EmailId}/forward", req.RequestUri!.AbsolutePath );

        var body = Assert.Single( _handler.Bodies ).RootElement;
        Assert.Equal( "colleague@example.org", body.GetProperty( "to" )[ 0 ].GetString() );
        Assert.False( body.TryGetProperty( "cc", out _ ) );
        Assert.False( body.TryGetProperty( "attachments", out _ ) );
    }


    /// <summary />
    [Fact]
    public async Task InboxDraftCreate_Reply_Body()
    {
        await _resend.InboxDraftCreateAsync( InboxId, new InboxDraftCreateData()
        {
            ThreadId = ThreadId,
            ReplyToEmailId = EmailId,
            TextBody = "Refund issued.",
        } );

        var body = Assert.Single( _handler.Bodies ).RootElement;
        Assert.Equal( ThreadId, body.GetProperty( "thread_id" ).GetGuid() );
        Assert.Equal( EmailId, body.GetProperty( "reply_to_email_id" ).GetGuid() );
        Assert.False( body.TryGetProperty( "to", out _ ) );
        Assert.False( body.TryGetProperty( "subject", out _ ) );
    }


    /// <summary />
    [Fact]
    public async Task InboxDraftUpdate_NullClears_UnassignedOmitted()
    {
        await _resend.InboxDraftUpdateAsync( InboxId, DraftId, new InboxDraftUpdateData()
        {
            To = null,
            Cc = new EmailAddressList(),
            HtmlBody = null,
            TextBody = "Refund issued.",
        } );

        var req = Assert.Single( _handler.Requests );
        Assert.Equal( HttpMethod.Patch, req.Method );
        Assert.Equal( $"/inboxes/{InboxId}/drafts/{DraftId}", req.RequestUri!.AbsolutePath );

        var body = Assert.Single( _handler.Bodies ).RootElement;
        Assert.Equal( JsonValueKind.Null, body.GetProperty( "to" ).ValueKind );
        Assert.Equal( 0, body.GetProperty( "cc" ).GetArrayLength() );
        Assert.Equal( JsonValueKind.Null, body.GetProperty( "html" ).ValueKind );
        Assert.Equal( "Refund issued.", body.GetProperty( "text" ).GetString() );
        Assert.False( body.TryGetProperty( "bcc", out _ ) );
        Assert.False( body.TryGetProperty( "subject", out _ ) );
    }


    /// <summary />
    [Fact]
    public async Task InboxDraftUpdate_NothingAssigned_EmptyObject()
    {
        await _resend.InboxDraftUpdateAsync( InboxId, DraftId, new InboxDraftUpdateData() );

        var body = Assert.Single( _handler.Bodies ).RootElement;
        Assert.Empty( body.EnumerateObject() );
    }


    /// <summary />
    [Fact]
    public async Task InboxDraftSend_NoBody()
    {
        await _resend.InboxDraftSendAsync( InboxId, DraftId );

        var req = Assert.Single( _handler.Requests );
        Assert.Equal( HttpMethod.Post, req.Method );
        Assert.Equal( $"/inboxes/{InboxId}/drafts/{DraftId}/send", req.RequestUri!.AbsolutePath );
        Assert.Null( req.Content );
    }


    /// <summary />
    [Fact]
    public void InboxDraftUpdateData_Roundtrip_KeepsAssignment()
    {
        var json = """{"subject":null,"to":["ada@example.org"]}""";

        var data = JsonSerializer.Deserialize<InboxDraftUpdateData>( json )!;
        var again = JsonDocument.Parse( JsonSerializer.Serialize( data ) ).RootElement;

        Assert.Equal( JsonValueKind.Null, again.GetProperty( "subject" ).ValueKind );
        Assert.Equal( "ada@example.org", again.GetProperty( "to" )[ 0 ].GetString() );
        Assert.False( again.TryGetProperty( "html", out _ ) );
    }


    /// <summary />
    private static Dictionary<string, string> Query( HttpRequestMessage req )
    {
        return Microsoft.AspNetCore.WebUtilities.QueryHelpers.ParseQuery( req.RequestUri!.Query )
            .ToDictionary( x => x.Key, x => x.Value.ToString() );
    }


    /// <summary />
    private class RecordingHandler : HttpMessageHandler
    {
        /// <summary />
        public List<HttpRequestMessage> Requests { get; } = new();

        /// <summary />
        public List<JsonDocument> Bodies { get; } = new();


        /// <inheritdoc />
        protected override async Task<HttpResponseMessage> SendAsync( HttpRequestMessage request, CancellationToken cancellationToken )
        {
            Requests.Add( request );

            if ( request.Content != null )
            {
                var json = await request.Content.ReadAsStringAsync( cancellationToken );
                Bodies.Add( JsonDocument.Parse( json ) );
            }

            var resp = new HttpResponseMessage( HttpStatusCode.OK );
            resp.Content = new StringContent( """{"object":"inbox","id":"479e3145-dd38-476b-932c-529ceb705947","has_more":false,"data":[]}""", Encoding.UTF8, "application/json" );

            return resp;
        }
    }
}
