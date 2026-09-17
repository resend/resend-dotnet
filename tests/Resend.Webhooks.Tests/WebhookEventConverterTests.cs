using System.Text.Json;

namespace Resend.Webhooks.Tests;

/// <summary />
public class WebhookEventConverterTests
{
    /// <summary />
    [Fact]
    public void EmailEventRoundtrip()
    {
        var utcNow = DateTime.UtcNow;
        utcNow = new DateTime(
            utcNow.Ticks - ( utcNow.Ticks % TimeSpan.TicksPerSecond ),
            utcNow.Kind
        );


        /*
         * 
         */
        var expectedEmail = new EmailEventData();
        expectedEmail.Subject = "Test Roundtrip";
        expectedEmail.MomentCreated = utcNow;

        var expected = new WebhookEvent();
        expected.EventType = WebhookEventType.EmailSent;
        expected.MomentCreated = utcNow;
        expected.Data = expectedEmail;

        var json = JsonSerializer.Serialize( expected );
        var actual = JsonSerializer.Deserialize<WebhookEvent>( json );


        /*
         *
         */
        Assert.NotNull( actual );
        Assert.Equal( expected.EventType, actual.EventType );
        Assert.Equal( expected.MomentCreated, actual.MomentCreated );
        Assert.Equal( expected.Data.GetType(), actual.Data.GetType() );

        var actualEmail = expected.DataAs<EmailEventData>();

        Assert.Equal( expectedEmail.Subject, actualEmail.Subject );
        Assert.Equal( expectedEmail.MomentCreated, actualEmail.MomentCreated );
    }


    /// <summary />
    [Fact]
    public void ContactEventRoundtrip()
    {
        var utcNow = DateTime.UtcNow;
        utcNow = new DateTime(
            utcNow.Ticks - ( utcNow.Ticks % TimeSpan.TicksPerSecond ),
            utcNow.Kind
        );


        /*
         * 
         */
        var expectedContact = new ContactEventData();
        expectedContact.ContactId = Guid.NewGuid();
        expectedContact.Email = "test@example.com";
        expectedContact.MomentCreated = utcNow;

        var expected = new WebhookEvent();
        expected.EventType = WebhookEventType.ContactCreated;
        expected.MomentCreated = utcNow;
        expected.Data = expectedContact;

        var json = JsonSerializer.Serialize( expected );
        var actual = JsonSerializer.Deserialize<WebhookEvent>( json );


        /*
         *
         */
        Assert.NotNull( actual );
        Assert.Equal( expected.EventType, actual.EventType );
        Assert.Equal( expected.MomentCreated, actual.MomentCreated );
        Assert.Equal( expected.Data.GetType(), actual.Data.GetType() );

        var actualContact = expected.DataAs<ContactEventData>();

        Assert.Equal( expectedContact.ContactId, actualContact.ContactId );
        Assert.Equal( expectedContact.Email, actualContact.Email );
        Assert.Equal( expectedContact.MomentCreated, actualContact.MomentCreated );
    }


    /// <summary />
    [Fact]
    public void DomainEventRoundtrip()
    {
        var utcNow = DateTime.UtcNow;
        utcNow = new DateTime(
            utcNow.Ticks - ( utcNow.Ticks % TimeSpan.TicksPerSecond ),
            utcNow.Kind
        );


        /*
         * 
         */
        var expectedDomain = new DomainEventData();
        expectedDomain.Id = Guid.NewGuid();
        expectedDomain.Name = "example.com";
        expectedDomain.Region = DeliveryRegion.UsEast1;
        expectedDomain.Status = ValidationStatus.Verified;
        expectedDomain.MomentCreated = utcNow;

        var expected = new WebhookEvent();
        expected.EventType = WebhookEventType.DomainCreated;
        expected.MomentCreated = utcNow;
        expected.Data = expectedDomain;

        var json = JsonSerializer.Serialize( expected );
        var actual = JsonSerializer.Deserialize<WebhookEvent>( json );


        /*
         *
         */
        Assert.NotNull( actual );
        Assert.Equal( expected.EventType, actual.EventType );
        Assert.Equal( expected.MomentCreated, actual.MomentCreated );
        Assert.Equal( expected.Data.GetType(), actual.Data.GetType() );

        var actualDomain = expected.DataAs<DomainEventData>();

        Assert.Equal( expectedDomain.Id, actualDomain.Id );
        Assert.Equal( expectedDomain.Name, actualDomain.Name );
        Assert.Equal( expectedDomain.Region, actualDomain.Region );
        Assert.Equal( expectedDomain.Status, actualDomain.Status );
        Assert.Equal( expectedDomain.MomentCreated, actualDomain.MomentCreated );
    }


    /// <summary />
    [Theory]
    [InlineData( WebhookEventType.SuppressionAdded )]
    [InlineData( WebhookEventType.SuppressionRemoved )]
    public void SuppressionEventRoundtrip( WebhookEventType eventType )
    {
        // Reproduces https://github.com/resend/resend-dotnet/issues/161
        var utcNow = DateTime.UtcNow;
        utcNow = new DateTime(
            utcNow.Ticks - ( utcNow.Ticks % TimeSpan.TicksPerSecond ),
            utcNow.Kind
        );


        /*
         *
         */
        var expectedSuppression = new SuppressionEventData();
        expectedSuppression.Id = Guid.NewGuid();
        expectedSuppression.Email = "test@example.com";
        expectedSuppression.Origin = SuppressionOrigin.Bounce;
        expectedSuppression.SourceId = Guid.NewGuid().ToString();
        expectedSuppression.MomentCreated = utcNow;

        var expected = new WebhookEvent();
        expected.EventType = eventType;
        expected.MomentCreated = utcNow;
        expected.Data = expectedSuppression;

        var json = JsonSerializer.Serialize( expected );
        var actual = JsonSerializer.Deserialize<WebhookEvent>( json );


        /*
         *
         */
        Assert.NotNull( actual );
        Assert.Equal( expected.EventType, actual.EventType );
        Assert.Equal( expected.MomentCreated, actual.MomentCreated );
        Assert.Equal( expected.Data.GetType(), actual.Data.GetType() );

        var actualSuppression = actual.DataAs<SuppressionEventData>();

        Assert.Equal( expectedSuppression.Id, actualSuppression.Id );
        Assert.Equal( expectedSuppression.Email, actualSuppression.Email );
        Assert.Equal( expectedSuppression.Origin, actualSuppression.Origin );
        Assert.Equal( expectedSuppression.SourceId, actualSuppression.SourceId );
        Assert.Equal( expectedSuppression.MomentCreated, actualSuppression.MomentCreated );
    }


    /// <summary />
    /// <remarks>
    /// Unlike <see cref="SuppressionEventRoundtrip"/>, this deserializes a fixed JSON literal
    /// so a typo in the <see cref="JsonStringValueAttribute"/> on <see cref="WebhookEventType"/>
    /// (which both serialization and deserialization share) can't hide a mismatch with the
    /// wire format Resend actually sends.
    /// </remarks>
    [Theory]
    [InlineData( "suppression.added", WebhookEventType.SuppressionAdded )]
    [InlineData( "suppression.removed", WebhookEventType.SuppressionRemoved )]
    public void SuppressionEvent_DeserializesLiteralWireType( string wireType, WebhookEventType expectedEventType )
    {
        var json = $$"""
        {
            "type": "{{wireType}}",
            "created_at": "2024-01-01T00:00:00.000Z",
            "data": {
                "id": "e169aa45-1ecf-4183-9955-b1499d5701d3",
                "email": "test@example.com",
                "origin": "bounce",
                "source_id": null,
                "created_at": "2024-01-01T00:00:00.000Z"
            }
        }
        """;

        var actual = JsonSerializer.Deserialize<WebhookEvent>( json );

        Assert.NotNull( actual );
        Assert.Equal( expectedEventType, actual.EventType );

        var data = actual.DataAs<SuppressionEventData>();
        Assert.Equal( "test@example.com", data.Email );
        Assert.Equal( SuppressionOrigin.Bounce, data.Origin );
    }
}
