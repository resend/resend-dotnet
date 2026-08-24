using Microsoft.AspNetCore.Mvc.Testing;
using Resend.ApiServer;
using System.Net;

namespace Resend.Tests;

/// <summary />
public partial class ResendClientTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly IResend _resend;


    /// <summary />
    public ResendClientTests( WebApplicationFactory<Program> factory )
    {
        _factory = factory;

        var http = _factory.CreateClient();

        var opt = new ResendClientOptions()
        {
            ApiUrl = http.BaseAddress!.ToString(),
        };

        _resend = ResendClient.Create( opt, http );
    }


    /// <summary />
    [Fact]
    public async Task EmailSend()
    {
        var email = new EmailMessage();
        email.Subject = "Unit testing";
        email.From = "from@example.com";
        email.To = "to@example.com";
        email.HtmlBody = "From unit test!";

        var resp = await _resend.EmailSendAsync( email );

        Assert.NotNull( resp );
        Assert.True( resp.Success );
        Assert.NotEqual( Guid.Empty, resp.Content );
    }


    /// <summary />
    [Fact]
    public async Task EmailRetrieve()
    {
        var anyId = Guid.NewGuid();

        var resp = await _resend.EmailRetrieveAsync( anyId );

        Assert.NotNull( resp );
        Assert.True( resp.Success );
        Assert.NotNull( resp.Content );
        Assert.Equal( anyId, resp.Content.Id );
        Assert.Equal( "onboarding@resend.dev", resp.Content.From.Email );
        Assert.Single( resp.Content.To );
        Assert.Null( resp.Content.TextBody );
        Assert.NotNull( resp.Content.HtmlBody );
    }


    /// <summary />
    [Fact]
    public async Task EmailList()
    {
        var resp = await _resend.EmailListAsync( new PaginatedQuery()
        {
            Limit = 20,
            After = Guid.NewGuid().ToString(),
        } );

        Assert.NotNull( resp );
        Assert.True( resp.Success );
        Assert.NotNull( resp.Content );
        Assert.True( resp.Content.HasMore );
    }


    /// <summary />
    [Fact]
    public async Task EmailBatch()
    {
        var email = new EmailMessage();
        email.Subject = "Unit testing";
        email.From = "from@example.com";
        email.To = "to@example.com";
        email.HtmlBody = "From unit test!";

        var list = new List<EmailMessage>() { email, email };

        var resp = await _resend.EmailBatchAsync( list );

        Assert.NotNull( resp );
        Assert.True( resp.Success );
        Assert.Equal( 2, resp.Content.Count );
    }


    /// <summary />
    [Fact]
    public async Task EmailReschedule()
    {
        var emailId = Guid.NewGuid();
        var rescheduleFor = DateTime.UtcNow.AddDays( 1 );

        var resp = await _resend.EmailRescheduleAsync( emailId, rescheduleFor );

        Assert.NotNull( resp );
        Assert.True( resp.Success );
    }


    /// <summary />
    [Fact]
    public async Task EmailCancel()
    {
        var emailId = Guid.NewGuid();

        var resp = await _resend.EmailCancelAsync( emailId );

        Assert.NotNull( resp );
        Assert.True( resp.Success );
    }


    /// <summary />
    [Fact]
    public async Task EmailShareDefaultExpiresIn()
    {
        var emailId = Guid.NewGuid();

        var resp = await _resend.EmailShareAsync( emailId );

        Assert.NotNull( resp );
        Assert.True( resp.Success );
        Assert.NotNull( resp.Content );
        Assert.Equal( emailId, resp.Content.Id );
        Assert.False( string.IsNullOrWhiteSpace( resp.Content.Url ) );
    }


    /// <summary />
    [Theory]
    [InlineData( "10m" )]
    [InlineData( "2 hours" )]
    [InlineData( "1 day" )]
    [InlineData( "1h 30m" )]
    [InlineData( "48h" )]
    public async Task EmailShareCustomExpiresIn( string expiresIn )
    {
        var emailId = Guid.NewGuid();

        var resp = await _resend.EmailShareAsync( emailId, expiresIn );

        Assert.NotNull( resp );
        Assert.True( resp.Success );
        Assert.NotNull( resp.Content );
        Assert.Equal( emailId, resp.Content.Id );
        Assert.False( string.IsNullOrWhiteSpace( resp.Content.Url ) );
    }


    /// <summary />
    [Theory]
    [InlineData( "banana" )]
    [InlineData( "72h" )]
    [InlineData( "3 days" )]
    [InlineData( "99999999999999999999h" )]
    public async Task EmailShareRejectsInvalidExpiresIn( string expiresIn )
    {
        var emailId = Guid.NewGuid();

        var ex = await Assert.ThrowsAsync<ResendException>( () => _resend.EmailShareAsync( emailId, expiresIn ) );

        Assert.Equal( HttpStatusCode.UnprocessableEntity, ex.StatusCode );
        Assert.Equal( ErrorType.ValidationError, ex.ErrorType );
    }


    /// <summary />
    [Fact]
    public async Task EmailShareNotFound()
    {
        var ex = await Assert.ThrowsAsync<ResendException>( () => _resend.EmailShareAsync( Guid.Empty ) );

        Assert.Equal( HttpStatusCode.NotFound, ex.StatusCode );
        Assert.Equal( ErrorType.NotFound, ex.ErrorType );
    }


    /// <summary />
    [Fact]
    public async Task DomainList()
    {
        var resp = await _resend.DomainListAsync();

        Assert.NotNull( resp );
    }


    /// <summary />
    [Fact]
    public async Task DomainAdd()
    {
        var resp = await _resend.DomainAddAsync( "example.com", DeliveryRegion.UsEast1 );

        Assert.NotNull( resp );
        Assert.NotEqual( Guid.Empty, resp.Content.Id );
    }


    /// <summary />
    [Fact]
    public async Task DomainDelete()
    {
        var resp = await _resend.DomainDeleteAsync( Guid.NewGuid() );

        Assert.NotNull( resp );
    }


    /// <summary />
    [Fact]
    public async Task DomainUpdate()
    {
        var resp = await _resend.DomainUpdateAsync( Guid.NewGuid(), new DomainUpdateData()
        {
            TrackClicks = true,
            TrackOpen = true,
        } );

        Assert.NotNull( resp );
    }


    /// <summary />
    [Fact]
    public async Task DomainVerify()
    {
        var resp = await _resend.DomainVerifyAsync( Guid.NewGuid() );

        Assert.NotNull( resp );
    }


    /// <summary />
    [Fact]
    public async Task DomainRetrieve()
    {
        var resp = await _resend.DomainRetrieveAsync( Guid.NewGuid() );

        Assert.NotNull( resp );
        Assert.NotEqual( Guid.Empty, resp.Content.Id );
    }


    /// <summary />
    [Fact]
    public async Task ApiKeyList()
    {
        var resp = await _resend.ApiKeyListAsync();

        Assert.NotNull( resp );
    }


    /// <summary />
    [Fact]
    public async Task ApiKeyCreate()
    {
        var resp = await _resend.ApiKeyCreateAsync( "resend-me", Permission.FullAccess );

        Assert.NotNull( resp );
        Assert.NotEqual( Guid.Empty, resp.Content.Id );
    }


    /// <summary />
    [Fact]
    public async Task ApiKeyUpdate()
    {
        var resp = await _resend.ApiKeyUpdateAsync( Guid.NewGuid(), "renamed-key" );

        Assert.NotNull( resp );
        Assert.True( resp.Success );
        Assert.NotEqual( Guid.Empty, resp.Content );
    }


    /// <summary />
    [Fact]
    public async Task ApiKeyDelete()
    {
        var resp = await _resend.ApiKeyDeleteAsync( Guid.NewGuid() );

        Assert.NotNull( resp );
    }


    /// <summary/>
    [Fact]
    public async Task ContactCreate()
    {
        var req = new ContactData()
        {
            Email = "test@example.com",
            FirstName = "Bob",
            LastName = "Test",
            IsUnsubscribed = true,
        };

        var resp = await _resend.ContactAddAsync( req );

        Assert.NotNull( resp );
        Assert.NotEqual( Guid.Empty, resp.Content );
    }


    /// <summary/>
    [Fact]
    public async Task ContactRetrieve()
    {
        var resp = await _resend.ContactRetrieveAsync( Guid.NewGuid() );

        Assert.NotNull( resp );
    }


    /// <summary/>
    [Fact]
    public async Task ContactRetrieveByEmail()
    {
        var resp = await _resend.ContactRetrieveByEmailAsync( "test@email.com" );

        Assert.NotNull( resp );
    }


    /// <summary/>
    [Fact]
    public async Task ContactUpdate()
    {
        var req = new ContactData()
        {
            Email = "test@email.com",
            FirstName = "Carl",
            LastName = "Test",
            IsUnsubscribed = true,
        };

        var resp = await _resend.ContactUpdateAsync( Guid.NewGuid(), req );

        Assert.NotNull( resp );
    }


    /// <summary/>
    [Fact]
    public async Task ContactUpdateByEmail()
    {
        var req = new ContactData()
        {
            FirstName = "Carl",
            LastName = "Test",
            IsUnsubscribed = true,
        };

        var resp = await _resend.ContactUpdateByEmailAsync( "test@email.com", req );

        Assert.NotNull( resp );
    }


    /// <summary/>
    [Fact]
    public async Task ContactList()
    {
        var resp = await _resend.ContactListAsync();

        Assert.NotNull( resp );
    }


    /// <summary/>
    [Fact]
    public async Task ContactDelete()
    {
        var resp = await _resend.ContactDeleteAsync( Guid.NewGuid() );

        Assert.NotNull( resp );
    }


    /// <summary/>
    [Fact]
    public async Task ContactDeleteByEmail()
    {
        var resp = await _resend.ContactDeleteByEmailAsync( "test@email.com" );

        Assert.NotNull( resp );
    }


    /// <summary/>
    [Fact]
    public async Task BroadcastCreate()
    {
        var req = new BroadcastData()
        {
            SegmentId = Guid.NewGuid(),
            DisplayName = "Display Name",
            Subject = "Unit testing",
            From = "from@example.com",
            HtmlBody = "From unit test!",
        };

        var resp = await _resend.BroadcastAddAsync( req );

        Assert.NotNull( resp );
        Assert.NotEqual( Guid.Empty, resp.Content );
    }


    /// <summary/>
    [Fact]
    public async Task BroadcastRetrieve()
    {
        var resp = await _resend.BroadcastRetrieveAsync( Guid.NewGuid() );

        Assert.NotNull( resp );
    }


    /// <summary/>
    [Fact]
    public async Task BroadcastUpdate()
    {
        var resp = await _resend.BroadcastUpdateAsync( Guid.NewGuid(), new BroadcastUpdateData()
        {
            HtmlBody = "From unit test!",
        } );

        Assert.NotNull( resp );
    }


    /// <summary/>
    [Fact]
    public async Task BroadcastSend()
    {
        var resp = await _resend.BroadcastSendAsync( Guid.NewGuid() );

        Assert.NotNull( resp );
    }


    /// <summary/>
    [Fact]
    public async Task BroadcastSchedule()
    {
        var resp = await _resend.BroadcastScheduleAsync( Guid.NewGuid(), DateTime.UtcNow );

        Assert.NotNull( resp );
    }


    /// <summary/>
    [Fact]
    public async Task BroadcastCancel()
    {
        var resp = await _resend.BroadcastCancelAsync( Guid.NewGuid() );

        Assert.NotNull( resp );
    }


    /// <summary/>
    [Fact]
    public async Task BroadcastList()
    {
        var resp = await _resend.BroadcastListAsync();

        Assert.NotNull( resp );
        Assert.NotNull( resp.Content );
        Assert.Equal( 3, resp.Content.Count );
    }


    /// <summary/>
    [Fact]
    public async Task BroadcastListRecipients()
    {
        var resp = await _resend.BroadcastListRecipientsAsync( Guid.NewGuid(), BroadcastRecipientEventType.Delivered );

        Assert.NotNull( resp );
        Assert.True( resp.Success );
        Assert.NotNull( resp.Content );
        Assert.True( resp.Content.HasMore );
        Assert.Single( resp.Content.Data );

        var recipient = resp.Content.Data[ 0 ];
        Assert.NotEqual( "", recipient.Id );
        Assert.NotNull( recipient.ContactId );
        Assert.NotEqual( "", recipient.Email );
        Assert.Null( recipient.Count );
        Assert.Null( recipient.BounceType );
        Assert.Null( recipient.ClickedLinks );
    }


    /// <summary/>
    [Fact]
    public async Task BroadcastListRecipientsOpened()
    {
        var resp = await _resend.BroadcastListRecipientsAsync( Guid.NewGuid(), BroadcastRecipientEventType.Opened );

        Assert.NotNull( resp );
        Assert.NotNull( resp.Content );

        var recipient = resp.Content.Data[ 0 ];
        Assert.Equal( 3, recipient.Count );
        Assert.Null( recipient.BounceType );
        Assert.Null( recipient.ClickedLinks );
    }


    /// <summary/>
    [Fact]
    public async Task BroadcastListRecipientsClicked()
    {
        var resp = await _resend.BroadcastListRecipientsAsync( Guid.NewGuid(), BroadcastRecipientEventType.Clicked );

        Assert.NotNull( resp );
        Assert.NotNull( resp.Content );

        var recipient = resp.Content.Data[ 0 ];
        Assert.Equal( 3, recipient.Count );
        Assert.NotNull( recipient.ClickedLinks );
        Assert.Single( recipient.ClickedLinks );
        Assert.Equal( "https://resend.com/pricing", recipient.ClickedLinks[ 0 ].Url );
        Assert.Equal( 2, recipient.ClickedLinks[ 0 ].Clicks );
    }


    /// <summary/>
    [Fact]
    public async Task BroadcastListRecipientsBounced()
    {
        var resp = await _resend.BroadcastListRecipientsAsync(
            Guid.NewGuid(),
            BroadcastRecipientEventType.Bounced,
            new BroadcastListRecipientsQuery() { BounceType = BroadcastRecipientBounceType.Transient } );

        Assert.NotNull( resp );
        Assert.NotNull( resp.Content );

        var recipient = resp.Content.Data[ 0 ];
        Assert.Equal( BroadcastRecipientBounceType.Transient, recipient.BounceType );
    }


    /// <summary/>
    [Fact]
    public async Task BroadcastListRecipientsWithQuery()
    {
        var resp = await _resend.BroadcastListRecipientsAsync(
            Guid.NewGuid(),
            BroadcastRecipientEventType.Delivered,
            new BroadcastListRecipientsQuery()
            {
                Email = "steve.wozniak@gmail.com",
                Limit = 10,
                After = Guid.NewGuid().ToString(),
            } );

        Assert.NotNull( resp );
        Assert.NotNull( resp.Content );
        Assert.Equal( "steve.wozniak@gmail.com", resp.Content.Data[ 0 ].Email );
    }


    /// <summary/>
    [Fact]
    public async Task BroadcastListRecipientsBounceTypeRequiresBouncedType()
    {
        await Assert.ThrowsAsync<ArgumentException>( () => _resend.BroadcastListRecipientsAsync(
            Guid.NewGuid(),
            BroadcastRecipientEventType.Delivered,
            new BroadcastListRecipientsQuery() { BounceType = BroadcastRecipientBounceType.Permanent } ) );
    }


    /// <summary/>
    [Fact]
    public async Task BroadcastListRecipientsNotFound()
    {
        var ex = await Assert.ThrowsAsync<ResendException>( () => _resend.BroadcastListRecipientsAsync( Guid.Empty, BroadcastRecipientEventType.Delivered ) );

        Assert.Equal( HttpStatusCode.NotFound, ex.StatusCode );
        Assert.Equal( ErrorType.NotFound, ex.ErrorType );
    }


    /// <summary/>
    [Fact]
    public async Task BroadcastDelete()
    {
        var resp = await _resend.BroadcastDeleteAsync( Guid.NewGuid() );

        Assert.NotNull( resp );
    }


    /// <summary/>
    [Fact]
    public async Task BroadcastClickedLinks()
    {
        var broadcastId = Guid.NewGuid();

        var resp = await _resend.BroadcastClickedLinksAsync( broadcastId );

        Assert.NotNull( resp );
        Assert.True( resp.Success );
        Assert.False( resp.Content.HasMore );
        Assert.Equal( 2, resp.Content.Data.Count );
        Assert.Equal( "https://resend.com/pricing", resp.Content.Data[ 0 ].Url );
        Assert.Equal( 42, resp.Content.Data[ 0 ].Clicks );
        Assert.Equal( 30, resp.Content.Data[ 0 ].UniqueClicks );
    }


    /// <summary/>
    [Fact]
    public async Task BroadcastClickedLinks_WithQuery()
    {
        var broadcastId = Guid.NewGuid();

        var resp = await _resend.BroadcastClickedLinksAsync( broadcastId, new PaginatedQuery()
        {
            Limit = 10,
            After = "cursor-value",
        } );

        Assert.NotNull( resp );
        Assert.True( resp.Success );
    }
}
