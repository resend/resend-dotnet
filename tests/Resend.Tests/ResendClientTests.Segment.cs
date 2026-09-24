namespace Resend.Tests;

/// <summary />
public partial class ResendClientTests
{
    /// <summary/>
    [Fact]
    public async Task SegmentUpdate()
    {
        var segmentId = Guid.NewGuid();

        var resp = await _resend.SegmentUpdateAsync( segmentId, new SegmentData()
        {
            Name = "Renamed segment",
        } );

        Assert.NotNull( resp );
        Assert.NotNull( resp.Content );
        Assert.Equal( segmentId, resp.Content.Id );
    }


    /// <summary/>
    [Fact]
    public async Task SegmentListContacts()
    {
        var resp = await _resend.SegmentListContactsAsync( Guid.NewGuid() );

        Assert.NotNull( resp );
        Assert.NotNull( resp.Content );
        Assert.Single( resp.Content.Data );
        Assert.False( resp.Content.HasMore );
    }
}
