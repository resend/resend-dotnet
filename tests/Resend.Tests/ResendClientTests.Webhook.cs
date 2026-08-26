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


    [Fact]
    public async Task WebhookEventList()
    {
        var resp = await _resend.WebhookEventListAsync( Guid.NewGuid(), new PaginatedQuery()
        {
            Limit = 1,
            After = "msg_1srOrx2ZWZBpBUvZwXKQmoEYga2",
        } );

        Assert.NotNull( resp.Content );
        Assert.Equal( "list", resp.Content.Object );
        Assert.True( resp.Content.HasMore );
        Assert.Single( resp.Content.Data );
        Assert.Equal( "msg_2aQqFEiKYaC8Q35b3e97qyRmaN7", resp.Content.Data[ 0 ].Id );
        Assert.Equal( WebhookEventType.EmailSent, resp.Content.Data[ 0 ].Type );
        Assert.Equal( WebhookEventLogStatus.Success, resp.Content.Data[ 0 ].Status );
    }


    [Fact]
    public async Task WebhookEventRetrieve()
    {
        var eventId = "msg_2aQqFEiKYaC8Q35b3e97qyRmaN7?attempt=1";
        var resp = await _resend.WebhookEventRetrieveAsync( Guid.NewGuid(), eventId );

        Assert.NotNull( resp.Content );
        Assert.Equal( "webhook_event", resp.Content.Object );
        Assert.Equal( eventId, resp.Content.Id );
        Assert.Equal( WebhookEventType.EmailSent, resp.Content.Type );
        Assert.Equal( WebhookEventLogStatus.Failed, resp.Content.Status );
        Assert.Null( resp.Content.MomentNextAttempt );
        Assert.Equal( "email.sent", resp.Content.Payload.GetProperty( "type" ).GetString() );
    }


    [Fact]
    public async Task WebhookEventAttemptList()
    {
        var resp = await _resend.WebhookEventAttemptListAsync(
            Guid.NewGuid(),
            "msg_2aQqFEiKYaC8Q35b3e97qyRmaN7?attempt=1",
            new PaginatedQuery()
            {
                Limit = 1,
                After = "atmpt_2ZbUCwvGmIT4mLIN6d3Yz0Ainbd",
            }
        );

        Assert.NotNull( resp.Content );
        Assert.Equal( "list", resp.Content.Object );
        Assert.True( resp.Content.HasMore );
        Assert.Single( resp.Content.Data );
        Assert.Equal( "atmpt_3ZbUCwvGmIT4mLIN6d3Yz0Ainbe", resp.Content.Data[ 0 ].Id );
        Assert.Equal( 200, resp.Content.Data[ 0 ].HttpStatusCode );
    }
}
