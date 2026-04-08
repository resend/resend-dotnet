namespace Resend.Tests;

/// <summary />
public partial class ResendClientTests
{
    /// <summary/>
    [Fact]
    public async Task LogList()
    {
        var resp = await _resend.LogListAsync();

        Assert.NotNull( resp );
        Assert.NotNull( resp.Content );
        Assert.False( resp.Content.HasMore );
        Assert.Equal( 2, resp.Content.Data.Count );
        Assert.Equal( "/emails", resp.Content.Data[ 0 ].Endpoint );
        Assert.Equal( "POST", resp.Content.Data[ 0 ].HttpMethod );
        Assert.Equal( 200, resp.Content.Data[ 0 ].ResponseStatus );
    }


    /// <summary/>
    [Fact]
    public async Task LogList_WithPaginationQuery()
    {
        var resp = await _resend.LogListAsync( new PaginatedQuery()
        {
            Limit = 20,
            After = Guid.NewGuid().ToString(),
        } );

        Assert.NotNull( resp );
        Assert.NotNull( resp.Content );
    }
}
