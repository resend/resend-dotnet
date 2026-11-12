namespace Resend.Tests;

/// <summary />
public partial class ResendClientTests
{
    /// <summary />
    [Fact]
    public async Task DomainForgeEnable()
    {
        var domainId = Guid.NewGuid();

        var resp = await _resend.DomainForgeEnableAsync( domainId );

        Assert.NotNull( resp );
        Assert.True( resp.Success );
        Assert.NotNull( resp.Content );
        Assert.Equal( domainId, resp.Content.Id );
        Assert.NotNull( resp.Content.Records );
        Assert.Equal( "send", resp.Content.Records![ 0 ].Name );
    }


    /// <summary />
    [Fact]
    public async Task DomainForgeEnableWithReturnPath()
    {
        var domainId = Guid.NewGuid();

        var resp = await _resend.DomainForgeEnableAsync( domainId, "outbound" );

        Assert.NotNull( resp );
        Assert.True( resp.Success );
        Assert.NotNull( resp.Content );
        Assert.Equal( domainId, resp.Content.Id );
        Assert.NotNull( resp.Content.Records );
        Assert.Equal( "outbound", resp.Content.Records![ 0 ].Name );
    }


    /// <summary />
    [Fact]
    public async Task DomainForgeUpdate()
    {
        var domainId = Guid.NewGuid();

        var resp = await _resend.DomainForgeUpdateAsync( domainId, "outbound" );

        Assert.NotNull( resp );
        Assert.True( resp.Success );
        Assert.NotNull( resp.Content );
        Assert.Equal( domainId, resp.Content.Id );
        Assert.NotNull( resp.Content.Records );
        Assert.Equal( "outbound", resp.Content.Records![ 0 ].Name );
    }


    /// <summary />
    [Fact]
    public async Task DomainForgeVerify()
    {
        var resp = await _resend.DomainForgeVerifyAsync( Guid.NewGuid() );

        Assert.NotNull( resp );
        Assert.True( resp.Success );
    }
}
