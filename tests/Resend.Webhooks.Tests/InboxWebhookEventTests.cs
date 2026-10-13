using System.Text.Json;

namespace Resend.Webhooks.Tests;

/// <summary />
public class InboxWebhookEventTests
{
    private const string Thread = """
        { "object": "inbox_thread", "id": "7c1f0a2e-5d3b-4e8a-9f61-2b8d4c6e1a90", "subject": "Question about my invoice", "folder": "inbox", "labels": [], "read": false }
        """;

    private const string Email = """
        {
            "id": "4ef9a417-02e9-4d39-ad75-9611e0fcc33c",
            "direction": "inbound",
            "from": "Steve Wozniak <steve@acme.com>",
            "to": [ "support@acme.com" ],
            "cc": [],
            "bcc": [],
            "reply_to": [],
            "subject": "Question about my invoice",
            "message_id": "<CAF7c1f0a2e5d3b@mail.acme.com>",
            "attachments": [ { "id": "0", "filename": "invoice.pdf", "size": 48213 } ],
            "read": false,
            "received_at": "2026-09-29T12:00:00.000Z"
        }
        """;

    private const string Draft = """
        {
            "object": "inbox_draft",
            "id": "9a2b7c4d-1e3f-4a5b-8c6d-0e1f2a3b4c5d",
            "type": "reply",
            "to": [ "steve@acme.com" ],
            "cc": [],
            "bcc": [],
            "subject": "Re: Question about my invoice",
            "thread_id": "7c1f0a2e-5d3b-4e8a-9f61-2b8d4c6e1a90",
            "reply_to_email_id": "4ef9a417-02e9-4d39-ad75-9611e0fcc33c",
            "email_id": null,
            "attachments": [ { "id": "5c1e9d2a-7b3f-4e8a-9c6d-2f0a1b3c4d5e", "filename": "receipt.pdf", "content_type": "application/pdf", "size": 20841 } ],
            "created_at": "2026-09-29T11:58:00.000Z",
            "updated_at": "2026-09-29T12:00:00.000Z"
        }
        """;


    /// <summary />
    [Theory]
    [InlineData( "inbox.created", WebhookEventType.InboxCreated, WebhookEventTypeCategory.Inbox )]
    [InlineData( "inbox.updated", WebhookEventType.InboxUpdated, WebhookEventTypeCategory.Inbox )]
    [InlineData( "inbox.deleted", WebhookEventType.InboxDeleted, WebhookEventTypeCategory.InboxDeleted )]
    [InlineData( "inbox.thread.created", WebhookEventType.InboxThreadCreated, WebhookEventTypeCategory.InboxThreadEmail )]
    [InlineData( "inbox.email.received", WebhookEventType.InboxEmailReceived, WebhookEventTypeCategory.InboxThreadEmail )]
    [InlineData( "inbox.email.sent", WebhookEventType.InboxEmailSent, WebhookEventTypeCategory.InboxThreadEmail )]
    [InlineData( "inbox.thread.folder.updated", WebhookEventType.InboxThreadFolderUpdated, WebhookEventTypeCategory.InboxThreadFolder )]
    [InlineData( "inbox.thread.assigned", WebhookEventType.InboxThreadAssigned, WebhookEventTypeCategory.InboxThreadAssigned )]
    [InlineData( "inbox.thread.unassigned", WebhookEventType.InboxThreadUnassigned, WebhookEventTypeCategory.InboxThreadUnassigned )]
    [InlineData( "inbox.thread.labels.updated", WebhookEventType.InboxThreadLabelsUpdated, WebhookEventTypeCategory.InboxThreadLabels )]
    [InlineData( "inbox.draft.created", WebhookEventType.InboxDraftCreated, WebhookEventTypeCategory.InboxDraft )]
    [InlineData( "inbox.draft.updated", WebhookEventType.InboxDraftUpdated, WebhookEventTypeCategory.InboxDraft )]
    [InlineData( "inbox.draft.sent", WebhookEventType.InboxDraftSent, WebhookEventTypeCategory.InboxDraftSent )]
    [InlineData( "inbox.draft.deleted", WebhookEventType.InboxDraftDeleted, WebhookEventTypeCategory.InboxDraft )]
    public void InboxEvent_DeserializesLiteralWireType( string wireType, WebhookEventType expectedEventType, WebhookEventTypeCategory expectedCategory )
    {
        var json = $$"""
        {
            "type": "{{wireType}}",
            "created_at": "2026-09-29T12:00:00.000Z",
            "data": {{DataFor( expectedCategory )}}
        }
        """;

        var actual = JsonSerializer.Deserialize<WebhookEvent>( json );

        Assert.NotNull( actual );
        Assert.Equal( expectedEventType, actual.EventType );
        Assert.Equal( expectedCategory, actual.EventType.Category() );

        var roundtrip = JsonSerializer.Deserialize<WebhookEvent>( JsonSerializer.Serialize( actual ) );

        Assert.NotNull( roundtrip );
        Assert.Equal( expectedEventType, roundtrip.EventType );
        Assert.Equal( actual.Data.GetType(), roundtrip.Data.GetType() );
    }


    /// <summary />
    [Fact]
    public void InboxCreatedEvent_Fields()
    {
        var actual = Deserialize( "inbox.created", DataFor( WebhookEventTypeCategory.Inbox ) );

        var data = actual.DataAs<InboxEventData>();
        Assert.Equal( InboxEventSource.Dashboard, data.Source );
        Assert.Equal( Guid.Parse( "b3e2b2b6-3f0e-4c8e-9ad3-2f43a1e2c7f1" ), data.InboxId );
        Assert.NotNull( data.Inbox );
        Assert.Equal( "support@acme.com", data.Inbox.EmailAddress );
        Assert.Equal( 2, data.Inbox.DraftCount );
    }


    /// <summary />
    [Fact]
    public void InboxEmailReceivedEvent_Fields()
    {
        var actual = Deserialize( "inbox.email.received", DataFor( WebhookEventTypeCategory.InboxThreadEmail ) );

        var data = actual.DataAs<InboxThreadEmailEventData>();
        Assert.Equal( InboxEventSource.System, data.Source );
        Assert.Equal( Guid.Parse( "4ef9a417-02e9-4d39-ad75-9611e0fcc33c" ), data.EmailId );
        Assert.NotNull( data.Thread );
        Assert.Equal( InboxFolder.Inbox, data.Thread.Folder );
        Assert.NotNull( data.Email );
        Assert.Equal( InboxEmailDirection.Inbound, data.Email.Direction );
        Assert.Equal( "steve@acme.com", data.Email.From.Email );
        Assert.Equal( "0", Assert.Single( data.Email.Attachments ).Id );
    }


    /// <summary />
    [Fact]
    public void InboxEmailSentEvent_Outbound()
    {
        var actual = Deserialize( "inbox.email.sent", DataFor( WebhookEventTypeCategory.InboxThreadEmail ).Replace( "\"inbound\"", "\"outbound\"" ) );

        var data = actual.DataAs<InboxThreadEmailEventData>();
        Assert.NotNull( data.Email );
        Assert.Equal( InboxEmailDirection.Outbound, data.Email.Direction );
    }


    /// <summary />
    [Fact]
    public void InboxThreadFolderUpdatedEvent_Fields()
    {
        var actual = Deserialize( "inbox.thread.folder.updated", DataFor( WebhookEventTypeCategory.InboxThreadFolder ) );

        var data = actual.DataAs<InboxThreadFolderUpdatedEventData>();
        Assert.Equal( InboxFolder.Inbox, data.From );
        Assert.Equal( InboxFolder.Archive, data.To );
        Assert.Equal( InboxEventSource.Api, data.Source );
    }


    /// <summary />
    [Fact]
    public void InboxThreadLabelsUpdatedEvent_Fields()
    {
        var actual = Deserialize( "inbox.thread.labels.updated", DataFor( WebhookEventTypeCategory.InboxThreadLabels ) );

        var data = actual.DataAs<InboxThreadLabelsUpdatedEventData>();
        Assert.Equal( "#E93D82", Assert.Single( data.Added ).Color );
        Assert.Empty( data.Removed );
        Assert.Equal( InboxEventSource.Agent, data.Source );
    }


    /// <summary />
    [Fact]
    public void InboxDraftDeletedEvent_HasNoDraft()
    {
        var actual = Deserialize( "inbox.draft.deleted", """
            { "source": "api", "inbox_id": "b3e2b2b6-3f0e-4c8e-9ad3-2f43a1e2c7f1", "draft_id": "9a2b7c4d-1e3f-4a5b-8c6d-0e1f2a3b4c5d" }
            """ );

        var data = actual.DataAs<InboxDraftEventData>();
        Assert.Equal( Guid.Parse( "9a2b7c4d-1e3f-4a5b-8c6d-0e1f2a3b4c5d" ), data.DraftId );
        Assert.Null( data.ThreadId );
        Assert.Null( data.Thread );
        Assert.Null( data.Draft );
    }


    /// <summary />
    [Fact]
    public void InboxDraftSentEvent_Fields()
    {
        var actual = Deserialize( "inbox.draft.sent", DataFor( WebhookEventTypeCategory.InboxDraftSent ) );

        var data = actual.DataAs<InboxDraftSentEventData>();
        Assert.Equal( Guid.Parse( "9a2b7c4d-1e3f-4a5b-8c6d-0e1f2a3b4c5d" ), data.DraftId );
        Assert.NotNull( data.Draft );
        Assert.Equal( InboxDraftType.Reply, data.Draft.DraftType );
        Assert.Equal( data.EmailId, data.Draft.EmailId );
        Assert.Equal( "application/pdf", Assert.Single( data.Draft.Attachments ).ContentType );
        Assert.Null( data.Email );
    }


    /// <summary />
    private static WebhookEvent Deserialize( string wireType, string data )
    {
        var json = $$"""
        {
            "type": "{{wireType}}",
            "created_at": "2026-09-29T12:00:00.000Z",
            "data": {{data}}
        }
        """;

        var actual = JsonSerializer.Deserialize<WebhookEvent>( json );

        Assert.NotNull( actual );

        return actual;
    }


    /// <summary />
    private static string DataFor( WebhookEventTypeCategory category )
    {
        const string ids = """
            "inbox_id": "b3e2b2b6-3f0e-4c8e-9ad3-2f43a1e2c7f1", "thread_id": "7c1f0a2e-5d3b-4e8a-9f61-2b8d4c6e1a90"
            """;

        return category switch
        {
            WebhookEventTypeCategory.Inbox => """
                {
                    "source": "dashboard",
                    "inbox_id": "b3e2b2b6-3f0e-4c8e-9ad3-2f43a1e2c7f1",
                    "inbox": {
                        "object": "inbox",
                        "id": "b3e2b2b6-3f0e-4c8e-9ad3-2f43a1e2c7f1",
                        "name": "Customer Support",
                        "email_address": "support@acme.com",
                        "domain_id": "d91cd9bd-1176-453e-8fc1-35364d380206",
                        "receiving_address": null,
                        "from_name": null,
                        "unread": 3,
                        "drafts": 2,
                        "last_received": null,
                        "created_at": "2026-08-01T09:12:44.512Z"
                    }
                }
                """,
            WebhookEventTypeCategory.InboxDeleted => """
                { "source": "api", "inbox_id": "b3e2b2b6-3f0e-4c8e-9ad3-2f43a1e2c7f1" }
                """,
            WebhookEventTypeCategory.InboxThreadEmail => $$"""
                { "source": "system", {{ids}}, "email_id": "4ef9a417-02e9-4d39-ad75-9611e0fcc33c", "thread": {{Thread}}, "email": {{Email}} }
                """,
            WebhookEventTypeCategory.InboxThreadFolder => $$"""
                { "from": "inbox", "to": "archive", "source": "api", {{ids}}, "thread": {{Thread}} }
                """,
            WebhookEventTypeCategory.InboxThreadAssigned => $$"""
                { "assignee_email": "ada@acme.com", "assigned_by_email": "grace@acme.com", "previous_assignee_email": null, "source": "dashboard", {{ids}}, "thread": {{Thread}} }
                """,
            WebhookEventTypeCategory.InboxThreadUnassigned => $$"""
                { "previous_assignee_email": "ada@acme.com", "source": "dashboard", {{ids}}, "thread": {{Thread}} }
                """,
            WebhookEventTypeCategory.InboxThreadLabels => $$"""
                { "added": [ { "id": "7f9c1d2e-4a6b-4c3d-8e15-9b0a7c6d5e34", "name": "Urgent", "color": "#E93D82" } ], "removed": [], "source": "agent", {{ids}}, "thread": {{Thread}} }
                """,
            WebhookEventTypeCategory.InboxDraft => $$"""
                { "source": "api", {{ids}}, "draft_id": "9a2b7c4d-1e3f-4a5b-8c6d-0e1f2a3b4c5d", "thread": {{Thread}}, "draft": {{Draft}} }
                """,
            WebhookEventTypeCategory.InboxDraftSent => $$"""
                { "source": "api", {{ids}}, "email_id": "6a0c8e58-3d9e-4f7a-8b14-2e3f4a5b6c71", "draft_id": "9a2b7c4d-1e3f-4a5b-8c6d-0e1f2a3b4c5d", "thread": {{Thread}}, "draft": {{Draft.Replace( "\"email_id\": null", "\"email_id\": \"6a0c8e58-3d9e-4f7a-8b14-2e3f4a5b6c71\"" )}} }
                """,
            _ => throw new NotSupportedException(),
        };
    }
}
