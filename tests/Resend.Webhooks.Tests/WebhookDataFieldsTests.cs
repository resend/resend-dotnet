using System.Text.Json;

namespace Resend.Webhooks.Tests;

/// <summary />
public class WebhookDataFieldsTests
{
    /// <summary />
    [Fact]
    public void EmailOpenedDeserializesOpenAndHeaders()
    {
        var json = """
        {
          "type": "email.opened",
          "created_at": "2024-11-22T23:41:12.126Z",
          "data": {
            "created_at": "2024-11-22T23:41:11.894Z",
            "email_id": "56761188-7520-42d8-8898-ff6fc54ce618",
            "from": "onboarding@resend.dev",
            "to": [ "delivered@resend.dev" ],
            "subject": "Sending this example",
            "headers": [ { "name": "X-Entity-Ref-ID", "value": "abc-123" } ],
            "open": { "ipAddress": "1.2.3.4", "timestamp": "2024-11-22T23:41:12.126Z", "userAgent": "Mozilla/5.0" }
          }
        }
        """;

        var evt = JsonSerializer.Deserialize<WebhookEvent>( json );

        Assert.NotNull( evt );
        Assert.Equal( WebhookEventType.EmailOpened, evt.EventType );

        var data = evt.DataAs<EmailEventData>();

        Assert.NotNull( data.Open );
        Assert.Equal( "1.2.3.4", data.Open!.IpAddress );
        Assert.Equal( "Mozilla/5.0", data.Open.UserAgent );

        Assert.NotNull( data.Headers );
        Assert.Single( data.Headers );
        Assert.Equal( "X-Entity-Ref-ID", data.Headers![ 0 ].Name );
        Assert.Equal( "abc-123", data.Headers[ 0 ].Value );
    }


    /// <summary />
    [Fact]
    public void EmailReceivedDeserializesInboundFields()
    {
        var json = """
        {
          "type": "email.received",
          "created_at": "2024-11-22T23:41:12.126Z",
          "data": {
            "created_at": "2024-11-22T23:41:11.894Z",
            "email_id": "56761188-7520-42d8-8898-ff6fc54ce618",
            "from": "sender@example.com",
            "to": [ "inbox@example.com" ],
            "subject": "Inbound",
            "message_id": "<abc@mail.example.com>",
            "bcc": [],
            "cc": [ "copy@example.com" ],
            "received_for": [ "inbox@example.com" ],
            "attachments": [ {
              "id": "att_123",
              "content_type": "image/png",
              "content_disposition": "inline",
              "filename": "avatar.png",
              "content_id": "img001"
            } ]
          }
        }
        """;

        var evt = JsonSerializer.Deserialize<WebhookEvent>( json );

        Assert.NotNull( evt );
        Assert.Equal( WebhookEventType.EmailReceived, evt.EventType );

        var data = evt.DataAs<EmailEventData>();

        Assert.Equal( "<abc@mail.example.com>", data.MessageId );
        Assert.NotNull( data.ReceivedFor );
        Assert.Single( data.ReceivedFor! );
        Assert.NotNull( data.Attachments );
        Assert.Single( data.Attachments! );
        Assert.Equal( "att_123", data.Attachments![ 0 ].Id );
        Assert.Equal( "avatar.png", data.Attachments[ 0 ].Filename );
    }


    /// <summary />
    [Fact]
    public void EmailSuppressedDeserializesAllFields()
    {
        var json = """
        {
          "type": "email.suppressed",
          "created_at": "2024-11-22T23:41:12.126Z",
          "data": {
            "created_at": "2024-11-22T23:41:11.894Z",
            "email_id": "56761188-7520-42d8-8898-ff6fc54ce618",
            "from": "sender@example.com",
            "to": [ "suppressed@example.com" ],
            "subject": "Suppressed",
            "suppressed": {
              "message": "recipient is on the account suppression list",
              "type": "OnAccountSuppressionList",
              "reason": "previous_bounce",
              "diagnosticCode": [ "smtp; 550 5.1.1 user unknown" ]
            }
          }
        }
        """;

        var evt = JsonSerializer.Deserialize<WebhookEvent>( json );

        Assert.NotNull( evt );
        Assert.Equal( WebhookEventType.EmailSuppressed, evt.EventType );

        var data = evt.DataAs<EmailEventData>();

        Assert.NotNull( data.Suppressed );
        Assert.Equal( "OnAccountSuppressionList", data.Suppressed!.Type );
        Assert.Equal( "previous_bounce", data.Suppressed.Reason );
        Assert.NotNull( data.Suppressed.DiagnosticCode );
        Assert.Single( data.Suppressed.DiagnosticCode! );
    }
}
