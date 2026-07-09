namespace Resend.Tests;

/// <summary />
public partial class ResendClientTests
{
    /// <summary />
    [Fact]
    public async Task OAuthGrantList()
    {
        var resp = await _resend.OAuthGrantListAsync();

        Assert.NotNull( resp );
        Assert.NotNull( resp.Content );
        Assert.NotEmpty( resp.Content.Data );

        var grant = resp.Content.Data[ 0 ];

        Assert.NotEqual( Guid.Empty, grant.Id );
        Assert.Equal( "Resend CLI", grant.Client.Name );
        Assert.Contains( "emails:send", grant.Scopes );
    }


    /// <summary />
    [Fact]
    public async Task OAuthGrantRevoke()
    {
        var id = Guid.NewGuid();

        var resp = await _resend.OAuthGrantRevokeAsync( id );

        Assert.NotNull( resp );
        Assert.NotNull( resp.Content );
        Assert.Equal( "oauth_grant", resp.Content.Object );
        Assert.Equal( id, resp.Content.Id );
        Assert.Equal( "revoked_from_api", resp.Content.RevokedReason );
    }
}
