namespace Resend.Tests;

/// <summary />
public partial class ResendClientTests
{
    /// <summary/>
    [Fact]
    public async Task WebhookList()
    {
        var resp = await _resend.WebhookListAsync();

        Assert.NotNull( resp );
        Assert.NotNull( resp.Content );

        Assert.False( resp.Content.HasMore );
        Assert.Equal( 3, resp.Content.Data.Count );
    }


    /// <summary/>
    [Fact]
    public async Task WebhookCreate()
    {
        var req = new WebhookData()
        {
            EndpointUrl = "https://domain.name/",
            Events = [ WebhookEventType.EmailDelivered ],
            Status = WebhookStatus.Enabled,
        };

        var resp = await _resend.WebhookCreateAsync( req );

        Assert.NotNull( resp );
        Assert.NotEqual( Guid.Empty, resp.Content.Id );
    }


    /// <summary/>
    [Fact]
    public async Task WebhookRetrieve()
    {
        var resp = await _resend.WebhookRetrieveAsync( Guid.NewGuid() );

        Assert.NotNull( resp );
    }


    /// <summary/>
    [Fact]
    public async Task WebhookUpdate()
    {
        var resp = await _resend.WebhookUpdateAsync( Guid.NewGuid(), new WebhookData()
        {
            EndpointUrl = "https://domain.name/",
            Events = [ WebhookEventType.EmailDelivered ],
            Status = WebhookStatus.Enabled,
        } );

        Assert.NotNull( resp );
    }


    /// <summary/>
    [Fact]
    public async Task WebhookDelete()
    {
        var resp = await _resend.WebhookDeleteAsync( Guid.NewGuid() );

        Assert.NotNull( resp );
    }
}