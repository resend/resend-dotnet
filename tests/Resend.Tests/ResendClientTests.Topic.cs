namespace Resend.Tests;

/// <summary />
public partial class ResendClientTests
{
    /// <summary/>
    [Fact]
    public async Task TopicList()
    {
        var resp = await _resend.TopicListAsync();

        Assert.NotNull( resp );
        Assert.NotNull( resp.Content );

        Assert.False( resp.Content.HasMore );
        Assert.Equal( 3, resp.Content.Data.Count );
    }


    /// <summary/>
    [Fact]
    public async Task TopicCreate()
    {
        var req = new TopicData()
        {
            Name = "Football",
            Description = "Champions League",
            SubscriptionDefault = SubscriptionType.OptIn,
        };

        var resp = await _resend.TopicCreateAsync( req );

        Assert.NotNull( resp );
        Assert.NotEqual( Guid.Empty, resp.Content );
    }


    /// <summary/>
    [Fact]
    public async Task TopicRetrieve()
    {
        var resp = await _resend.TopicRetrieveAsync( Guid.NewGuid() );

        Assert.NotNull( resp );
    }


    /// <summary/>
    [Fact]
    public async Task TopicUpdate()
    {
        var resp = await _resend.TopicUpdateAsync( Guid.NewGuid(), new TopicData()
        {
            SubscriptionDefault = SubscriptionType.OptOut,
        } );

        Assert.NotNull( resp );
    }


    /// <summary/>
    [Fact]
    public async Task TopicDelete()
    {
        var resp = await _resend.TopicDeleteAsync( Guid.NewGuid() );

        Assert.NotNull( resp );
    }
}