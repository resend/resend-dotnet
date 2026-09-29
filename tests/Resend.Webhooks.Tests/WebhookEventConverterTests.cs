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


    /// <summary />
    [Fact]
    public void ContactTopicsEventRoundtrip()
    {
        var utcNow = DateTime.UtcNow;
        utcNow = new DateTime(
            utcNow.Ticks - ( utcNow.Ticks % TimeSpan.TicksPerSecond ),
            utcNow.Kind
        );


        /*
         *
         */
        var expectedContactTopics = new ContactTopicsEventData();
        expectedContactTopics.Email = "test@example.com";
        expectedContactTopics.Topics = new List<TopicSubscription>()
        {
            new TopicSubscription() { Id = Guid.NewGuid(), Subscription = SubscriptionType.OptIn },
            new TopicSubscription() { Id = Guid.NewGuid(), Subscription = SubscriptionType.OptOut },
        };

        var expected = new WebhookEvent();
        expected.EventType = WebhookEventType.ContactTopicsUpdated;
        expected.MomentCreated = utcNow;
        expected.Data = expectedContactTopics;

        var json = JsonSerializer.Serialize( expected );
        var actual = JsonSerializer.Deserialize<WebhookEvent>( json );


        /*
         *
         */
        Assert.NotNull( actual );
        Assert.Equal( expected.EventType, actual.EventType );
        Assert.Equal( expected.MomentCreated, actual.MomentCreated );
        Assert.Equal( expected.Data.GetType(), actual.Data.GetType() );

        var actualContactTopics = actual.DataAs<ContactTopicsEventData>();

        Assert.Equal( expectedContactTopics.Email, actualContactTopics.Email );
        Assert.Equal( expectedContactTopics.Topics.Count, actualContactTopics.Topics.Count );

        for ( var i = 0; i < expectedContactTopics.Topics.Count; i++ )
        {
            Assert.Equal( expectedContactTopics.Topics[ i ].Id, actualContactTopics.Topics[ i ].Id );
            Assert.Equal( expectedContactTopics.Topics[ i ].Subscription, actualContactTopics.Topics[ i ].Subscription );
        }
    }


    /// <summary />
    [Fact]
    public void ContactTopicsEvent_DeserializesLiteralWireType()
    {
        var json = """
        {
            "type": "contact.topics.updated",
            "created_at": "2026-02-12T10:00:00.000Z",
            "data": {
                "email": "steve.wozniak@gmail.com",
                "topics": [
                    { "id": "b6d24b8e-af0b-4c3c-be0c-359bbd97381e", "subscription": "opt_in" },
                    { "id": "07d84122-7224-4881-9c31-1c048e204602", "subscription": "opt_out" }
                ]
            }
        }
        """;

        var actual = JsonSerializer.Deserialize<WebhookEvent>( json );

        Assert.NotNull( actual );
        Assert.Equal( WebhookEventType.ContactTopicsUpdated, actual.EventType );

        var data = actual.DataAs<ContactTopicsEventData>();
        Assert.Equal( "steve.wozniak@gmail.com", data.Email );
        Assert.Equal( 2, data.Topics.Count );
        Assert.Equal( Guid.Parse( "b6d24b8e-af0b-4c3c-be0c-359bbd97381e" ), data.Topics[ 0 ].Id );
        Assert.Equal( SubscriptionType.OptIn, data.Topics[ 0 ].Subscription );
        Assert.Equal( Guid.Parse( "07d84122-7224-4881-9c31-1c048e204602" ), data.Topics[ 1 ].Id );
        Assert.Equal( SubscriptionType.OptOut, data.Topics[ 1 ].Subscription );
    }


    /// <summary />
    [Theory]
    [InlineData( WebhookEventType.TopicCreated )]
    [InlineData( WebhookEventType.TopicUpdated )]
    [InlineData( WebhookEventType.TopicDeleted )]
    public void TopicEventRoundtrip( WebhookEventType eventType )
    {
        var utcNow = DateTime.UtcNow;
        utcNow = new DateTime(
            utcNow.Ticks - ( utcNow.Ticks % TimeSpan.TicksPerSecond ),
            utcNow.Kind
        );


        /*
         *
         */
        var expectedTopic = new TopicEventData();
        expectedTopic.Id = Guid.NewGuid();
        expectedTopic.Name = "Product Updates";
        expectedTopic.Description = "New features and improvements";
        expectedTopic.SubscriptionDefault = SubscriptionType.OptIn;
        expectedTopic.IsDeleted = eventType == WebhookEventType.TopicDeleted;
        expectedTopic.MomentCreated = utcNow;
        expectedTopic.MomentUpdated = utcNow;

        var expected = new WebhookEvent();
        expected.EventType = eventType;
        expected.MomentCreated = utcNow;
        expected.Data = expectedTopic;

        var json = JsonSerializer.Serialize( expected );
        var actual = JsonSerializer.Deserialize<WebhookEvent>( json );


        /*
         *
         */
        Assert.NotNull( actual );
        Assert.Equal( expected.EventType, actual.EventType );
        Assert.Equal( expected.MomentCreated, actual.MomentCreated );
        Assert.Equal( expected.Data.GetType(), actual.Data.GetType() );

        var actualTopic = actual.DataAs<TopicEventData>();

        Assert.Equal( expectedTopic.Id, actualTopic.Id );
        Assert.Equal( expectedTopic.Name, actualTopic.Name );
        Assert.Equal( expectedTopic.Description, actualTopic.Description );
        Assert.Equal( expectedTopic.SubscriptionDefault, actualTopic.SubscriptionDefault );
        Assert.Equal( expectedTopic.IsDeleted, actualTopic.IsDeleted );
        Assert.Equal( expectedTopic.MomentCreated, actualTopic.MomentCreated );
        Assert.Equal( expectedTopic.MomentUpdated, actualTopic.MomentUpdated );
    }


    /// <summary />
    [Theory]
    [InlineData( "topic.created", WebhookEventType.TopicCreated, false )]
    [InlineData( "topic.updated", WebhookEventType.TopicUpdated, false )]
    [InlineData( "topic.deleted", WebhookEventType.TopicDeleted, true )]
    public void TopicEvent_DeserializesLiteralWireType( string wireType, WebhookEventType expectedEventType, bool deleted )
    {
        var json = $$"""
        {
            "type": "{{wireType}}",
            "created_at": "2026-02-12T10:00:00.000Z",
            "data": {
                "id": "b6d24b8e-af0b-4c3c-be0c-359bbd97381e",
                "name": "Product Updates",
                "description": null,
                "default_subscription": "opt_out",
                "deleted": {{( deleted ? "true" : "false" )}},
                "created_at": "2026-02-12T10:00:00.000Z",
                "updated_at": "2026-02-12T10:00:00.000Z"
            }
        }
        """;

        var actual = JsonSerializer.Deserialize<WebhookEvent>( json );

        Assert.NotNull( actual );
        Assert.Equal( expectedEventType, actual.EventType );

        var data = actual.DataAs<TopicEventData>();
        Assert.Equal( Guid.Parse( "b6d24b8e-af0b-4c3c-be0c-359bbd97381e" ), data.Id );
        Assert.Equal( "Product Updates", data.Name );
        Assert.Null( data.Description );
        Assert.Equal( SubscriptionType.OptOut, data.SubscriptionDefault );
        Assert.Equal( deleted, data.IsDeleted );
    }
}
