namespace Resend.Tests;

/// <summary />
public partial class ResendClientTests
{
    /// <summary />
    [Fact]
    public async Task DomainClaim()
    {
        var resp = await _resend.DomainClaimAsync( "example.com" );

        Assert.NotNull( resp );
        Assert.True( resp.Success );
        Assert.NotNull( resp.Content );
        Assert.Equal( "domain_claim", resp.Content.Object );
        Assert.Equal( "example.com", resp.Content.Name );
        Assert.Equal( DomainClaimStatus.Pending, resp.Content.Status );
        Assert.NotNull( resp.Content.DomainId );
        Assert.Equal( "TXT", resp.Content.Record.RecordType );
        Assert.Equal( "Auto", resp.Content.Record.TimeToLive );
    }


    /// <summary />
    [Fact]
    public async Task DomainClaimWithData()
    {
        var resp = await _resend.DomainClaimAsync( new DomainClaimData()
        {
            DomainName = "example.com",
            Region = DeliveryRegion.EuWest1,
        } );

        Assert.NotNull( resp );
        Assert.True( resp.Success );
        Assert.NotNull( resp.Content );
        Assert.Equal( "example.com", resp.Content.Name );
        Assert.Equal( DeliveryRegion.EuWest1, resp.Content.Region );
    }


    /// <summary />
    [Fact]
    public async Task DomainClaimRetrieve()
    {
        var domainId = Guid.NewGuid();

        var resp = await _resend.DomainClaimRetrieveAsync( domainId );

        Assert.NotNull( resp );
        Assert.True( resp.Success );
        Assert.NotNull( resp.Content );
        Assert.Equal( domainId, resp.Content.DomainId );
        Assert.Equal( DomainClaimStatus.Pending, resp.Content.Status );
    }


    /// <summary />
    [Fact]
    public async Task DomainClaimVerify()
    {
        var domainId = Guid.NewGuid();

        var resp = await _resend.DomainClaimVerifyAsync( domainId );

        Assert.NotNull( resp );
        Assert.True( resp.Success );
        Assert.NotNull( resp.Content );
        Assert.Equal( domainId, resp.Content.DomainId );
    }
}
