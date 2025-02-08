using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Options;
using Resend.ApiServer;

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

        var opt = Options.Create( new ResendClientOptions()
        {
            ApiUrl = http.BaseAddress!.ToString(),
        } );

        _resend = new ResendClient( opt, http );
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
        Assert.Equal( "from@example.com", resp.Content.From.Email );
        Assert.Single( resp.Content.To );
        Assert.Null( resp.Content.TextBody );
        Assert.NotNull( resp.Content.HtmlBody );
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
    public async Task ApiKeyDelete()
    {
        var resp = await _resend.ApiKeyDeleteAsync( Guid.NewGuid() );

        Assert.NotNull( resp );
    }


    /// <summary/>
    [Fact]
    public async Task AudienceCreate()
    {
        var resp = await _resend.AudienceAddAsync( "audience-test" );

        Assert.NotNull( resp );
        Assert.NotEqual( Guid.Empty, resp.Content );
    }


    /// <summary/>
    [Fact]
    public async Task AudienceRetrieve()
    {
        var resp = await _resend.AudienceRetrieveAsync( Guid.NewGuid() );

        Assert.NotNull( resp );
    }


    /// <summary/>
    [Fact]
    public async Task AudienceList()
    {
        var resp = await _resend.AudienceListAsync();

        Assert.NotNull( resp );
    }


    /// <summary/>
    [Fact]
    public async Task AudienceDelete()
    {
        var resp = await _resend.AudienceDeleteAsync( Guid.NewGuid() );

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

        var resp = await _resend.ContactAddAsync( Guid.NewGuid(), req );

        Assert.NotNull( resp );
        Assert.NotEqual( Guid.Empty, resp.Content );
    }


    /// <summary/>
    [Fact]
    public async Task ContactRetrieve()
    {
        var resp = await _resend.ContactRetrieveAsync( Guid.NewGuid(), Guid.NewGuid() );

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

        var resp = await _resend.ContactUpdateAsync( Guid.NewGuid(), Guid.NewGuid(), req );

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

        var resp = await _resend.ContactUpdateByEmailAsync( Guid.NewGuid(), "test@email.com", req );

        Assert.NotNull( resp );
    }


    /// <summary/>
    [Fact]
    public async Task ContactList()
    {
        var resp = await _resend.ContactListAsync( Guid.NewGuid() );

        Assert.NotNull( resp );
    }


    /// <summary/>
    [Fact]
    public async Task ContactDelete()
    {
        var resp = await _resend.ContactDeleteAsync( Guid.NewGuid(), Guid.NewGuid() );

        Assert.NotNull( resp );
    }


    /// <summary/>
    [Fact]
    public async Task ContactDeleteByEmail()
    {
        var resp = await _resend.ContactDeleteByEmailAsync( Guid.NewGuid(), "test@email.com" );

        Assert.NotNull( resp );
    }


    /// <summary/>
    [Fact]
    public async Task BroadcastCreate()
    {
        var req = new BroadcastData()
        {
            AudienceId = Guid.NewGuid(),
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
    public async Task BroadcastList()
    {
        var resp = await _resend.BroadcastListAsync();

        Assert.NotNull( resp );
        Assert.NotNull( resp.Content );
        Assert.Equal( 3, resp.Content.Count );
    }


    /// <summary/>
    [Fact]
    public async Task BroadcastDelete()
    {
        var resp = await _resend.BroadcastDeleteAsync( Guid.NewGuid() );

        Assert.NotNull( resp );
    }
}
