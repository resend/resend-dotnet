using System.Net;

namespace Resend.Tests;

/// <summary />
public partial class ResendClientTests
{
    /// <summary />
    [Fact]
    public async Task SuppressionAdd()
    {
        var resp = await _resend.SuppressionAddAsync( "steve.wozniak@gmail.com" );

        Assert.NotNull( resp );
        Assert.True( resp.Success );
        Assert.NotEqual( Guid.Empty, resp.Content );
    }


    /// <summary />
    [Fact]
    public async Task SuppressionList()
    {
        var resp = await _resend.SuppressionListAsync( new SuppressionListQuery()
        {
            Limit = 20,
            After = Guid.NewGuid().ToString(),
        } );

        Assert.NotNull( resp );
        Assert.True( resp.Success );
        Assert.NotNull( resp.Content );
        Assert.True( resp.Content.HasMore );
        Assert.Single( resp.Content.Data );
        Assert.Equal( "steve.wozniak@gmail.com", resp.Content.Data[ 0 ].Email );
    }


    /// <summary />
    [Fact]
    public async Task SuppressionListByOrigin()
    {
        var resp = await _resend.SuppressionListAsync( new SuppressionListQuery()
        {
            Origin = SuppressionOrigin.Bounce,
        } );

        Assert.NotNull( resp );
        Assert.True( resp.Success );
        Assert.NotNull( resp.Content );
        Assert.Equal( SuppressionOrigin.Bounce, resp.Content.Data[ 0 ].Origin );
        Assert.NotNull( resp.Content.Data[ 0 ].SourceId );
    }


    /// <summary>
    /// Manual-origin suppressions carry no source_id.
    /// </summary>
    [Fact]
    public async Task SuppressionListManualHasNoSourceId()
    {
        var resp = await _resend.SuppressionListAsync( new SuppressionListQuery()
        {
            Origin = SuppressionOrigin.Manual,
        } );

        Assert.NotNull( resp );
        Assert.True( resp.Success );
        Assert.NotNull( resp.Content );
        Assert.Equal( SuppressionOrigin.Manual, resp.Content.Data[ 0 ].Origin );
        Assert.Null( resp.Content.Data[ 0 ].SourceId );
    }


    /// <summary />
    [Fact]
    public async Task SuppressionRetrieveById()
    {
        var suppressionId = Guid.NewGuid();

        var resp = await _resend.SuppressionRetrieveAsync( suppressionId.ToString() );

        Assert.NotNull( resp );
        Assert.True( resp.Success );
        Assert.NotNull( resp.Content );
        Assert.Equal( suppressionId, resp.Content.Id );
        Assert.Equal( "suppression", resp.Content.Object );
    }


    /// <summary>
    /// The path parameter accepts an email address, which must survive URL-encoding.
    /// </summary>
    [Fact]
    public async Task SuppressionRetrieveByEmail()
    {
        var resp = await _resend.SuppressionRetrieveAsync( "steve+woz@example.com" );

        Assert.NotNull( resp );
        Assert.True( resp.Success );
        Assert.NotNull( resp.Content );
        Assert.Equal( "steve+woz@example.com", resp.Content.Email );
    }


    /// <summary />
    [Fact]
    public async Task SuppressionRemove()
    {
        var suppressionId = Guid.NewGuid();

        var resp = await _resend.SuppressionRemoveAsync( suppressionId.ToString() );

        Assert.NotNull( resp );
        Assert.True( resp.Success );
        Assert.NotNull( resp.Content );
        Assert.Equal( suppressionId, resp.Content.Id );
        Assert.True( resp.Content.Deleted );
    }


    /// <summary />
    [Fact]
    public async Task SuppressionBatchAdd()
    {
        var resp = await _resend.SuppressionBatchAddAsync( new[]
        {
            "steve.wozniak@gmail.com",
            "steve+woz@example.com",
        } );

        Assert.NotNull( resp );
        Assert.True( resp.Success );
        Assert.NotNull( resp.Content );
        Assert.Equal( 2, resp.Content.Count );
        Assert.All( resp.Content, ( x ) => Assert.NotEqual( Guid.Empty, x ) );
    }


    /// <summary>
    /// The API lowercases, trims and dedupes addresses, so the response can be shorter than
    /// the request and must not be paired with the input by index.
    /// </summary>
    [Fact]
    public async Task SuppressionBatchAddDedupesCaseVariants()
    {
        var resp = await _resend.SuppressionBatchAddAsync( new[]
        {
            "CAROLINA+suppressed@resend.com",
            "carolina+suppressed@resend.com",
        } );

        Assert.NotNull( resp );
        Assert.True( resp.Success );
        Assert.NotNull( resp.Content );
        Assert.Single( resp.Content );
    }


    /// <summary>
    /// The API validates each entry, so a blank one fails the whole batch.
    /// </summary>
    [Theory]
    [InlineData( null )]
    [InlineData( "   " )]
    public async Task SuppressionBatchAddRejectsBlankEntry( string? email )
    {
        var ex = await Assert.ThrowsAsync<ResendException>( () => _resend.SuppressionBatchAddAsync( new[]
        {
            "steve.wozniak@gmail.com",
            email!,
        } ) );

        Assert.Equal( HttpStatusCode.UnprocessableEntity, ex.StatusCode );
        Assert.Equal( ErrorType.ValidationError, ex.ErrorType );
    }


    /// <summary />
    [Fact]
    public async Task SuppressionBatchRemoveByEmail()
    {
        var resp = await _resend.SuppressionBatchRemoveAsync( new[]
        {
            "steve.wozniak@gmail.com",
        } );

        Assert.NotNull( resp );
        Assert.True( resp.Success );
        Assert.NotNull( resp.Content );
        Assert.Single( resp.Content );
        Assert.True( resp.Content[ 0 ].Deleted );
    }


    /// <summary />
    [Fact]
    public async Task SuppressionBatchRemoveById()
    {
        var suppressionId = Guid.NewGuid();

        var resp = await _resend.SuppressionBatchRemoveAsync( new[] { suppressionId } );

        Assert.NotNull( resp );
        Assert.True( resp.Success );
        Assert.NotNull( resp.Content );
        Assert.Single( resp.Content );
        Assert.Equal( suppressionId, resp.Content[ 0 ].Id );
        Assert.True( resp.Content[ 0 ].Deleted );
    }


    /// <summary />
    [Theory]
    [InlineData( null )]
    [InlineData( "   " )]
    public async Task SuppressionBatchRemoveRejectsBlankEntry( string? email )
    {
        var ex = await Assert.ThrowsAsync<ResendException>( () => _resend.SuppressionBatchRemoveAsync( new[]
        {
            "steve.wozniak@gmail.com",
            email!,
        } ) );

        Assert.Equal( HttpStatusCode.UnprocessableEntity, ex.StatusCode );
        Assert.Equal( ErrorType.ValidationError, ex.ErrorType );
    }
}
