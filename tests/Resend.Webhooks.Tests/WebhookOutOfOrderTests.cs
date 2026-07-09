using System.Text.Json;

namespace Resend.Webhooks.Tests;

/// <summary />
public class WebhookOutOfOrderTests
{
    /// <summary />
    /// <remarks>
    /// Real Resend payloads serialize <c>type</c> last (after <c>created_at</c>
    /// and <c>data</c>); the converter must not assume <c>type</c> comes first.
    /// </remarks>
    [Fact]
    public void EmailEventTypeLastDeserializes()
    {
        var json = """
        {
          "created_at": "2026-07-09T18:23:37.867Z",
          "data": {
            "created_at": "2026-07-09T18:23:37.236Z",
            "email_id": "c4bd0130-17f1-4e49-90bc-856308df9bb5",
            "failed": { "reason": "domain_not_verified" },
            "from": "Acme <onboarding@resend.dev>",
            "subject": "Test",
            "template_id": "2dc48b66-eef5-4601-b509-ca33f3e10b60",
            "to": [ "to@example.com" ]
          },
          "type": "email.failed"
        }
        """;

        var evt = JsonSerializer.Deserialize<WebhookEvent>( json );

        Assert.NotNull( evt );
        Assert.Equal( WebhookEventType.EmailFailed, evt.EventType );

        var data = evt.DataAs<EmailEventData>();
        Assert.Equal( "domain_not_verified", data.Failed?.Reason );
        Assert.Equal( "2dc48b66-eef5-4601-b509-ca33f3e10b60", data.TemplateId );
    }


    /// <summary />
    /// <remarks>Contact events serialize <c>created_at</c> before <c>type</c>.</remarks>
    [Fact]
    public void ContactEventCreatedAtFirstDeserializes()
    {
        var json = """
        {
          "created_at": "2026-07-09T18:23:37.867Z",
          "type": "contact.created",
          "data": {
            "id": "e169aa45-1ecf-4183-9955-b1499d5701d3",
            "created_at": "2026-07-09T18:23:37.236Z",
            "updated_at": "2026-07-09T18:23:37.236Z",
            "email": "to@example.com",
            "unsubscribed": false
          }
        }
        """;

        var evt = JsonSerializer.Deserialize<WebhookEvent>( json );

        Assert.NotNull( evt );
        Assert.Equal( WebhookEventType.ContactCreated, evt.EventType );
        Assert.Equal( "to@example.com", evt.DataAs<ContactEventData>().Email );
    }


    /// <summary />
    [Theory]
    [InlineData( """{ "type": "email.sent", "type": "email.sent", "created_at": "2026-07-09T18:23:37.867Z", "data": {} }""" )]
    [InlineData( """{ "type": "email.sent", "created_at": "2026-07-09T18:23:37.867Z" }""" )]
    [InlineData( """{ "type": "email.sent", "created_at": "2026-07-09T18:23:37.867Z", "data": {}, "extra": 1 }""" )]
    public void MalformedEnvelopeThrows( string json )
    {
        Assert.ThrowsAny<JsonException>( () => JsonSerializer.Deserialize<WebhookEvent>( json ) );
    }
}
