using System.Text.Json;

namespace Resend.Tests;

/// <summary />
public partial class ResendClientTests
{
    /// <summary/>
    [Fact]
    public async Task EventCreate()
    {
        var resp = await _resend.EventCreateAsync( new EventCreateData()
        {
            Name = "user.created",
            Schema = JsonDocument.Parse( "{\"plan\":\"string\"}" ).RootElement,
        } );

        Assert.NotNull( resp );
        Assert.True( resp.Success );
        Assert.NotEqual( Guid.Empty, resp.Content );
    }


    /// <summary/>
    [Fact]
    public async Task EventRetrieve_ByGuid()
    {
        var id = Guid.Parse( "a1b2c3d4-e5f6-7890-abcd-ef1234567890" );

        var resp = await _resend.EventRetrieveAsync( id );

        Assert.NotNull( resp );
        Assert.True( resp.Success );
        Assert.Equal( id, resp.Content.Id );
        Assert.Equal( "user.created", resp.Content.Name );
    }


    /// <summary/>
    [Fact]
    public async Task EventRetrieve_ByName()
    {
        var resp = await _resend.EventRetrieveAsync( "user.created" );

        Assert.NotNull( resp );
        Assert.True( resp.Success );
        Assert.Equal( "user.created", resp.Content.Name );
    }


    /// <summary/>
    [Fact]
    public async Task EventList()
    {
        var resp = await _resend.EventListAsync();

        Assert.NotNull( resp );
        Assert.True( resp.Success );
        Assert.Equal( 2, resp.Content.Data.Count );
        Assert.Equal( "user.created", resp.Content.Data[ 0 ].Name );
        Assert.Null( resp.Content.Data[ 1 ].Schema );
    }


    /// <summary/>
    [Fact]
    public async Task EventList_WithPagination()
    {
        var resp = await _resend.EventListAsync( new PaginatedQuery()
        {
            Limit = 20,
            After = Guid.NewGuid().ToString(),
        } );

        Assert.NotNull( resp );
        Assert.True( resp.Success );
    }


    /// <summary/>
    [Fact]
    public async Task EventUpdate_ByName()
    {
        var resp = await _resend.EventUpdateAsync( "user.created", new EventUpdateData()
        {
            Schema = JsonDocument.Parse( "{\"plan\":\"string\",\"trial\":\"boolean\"}" ).RootElement,
        } );

        Assert.NotNull( resp );
        Assert.True( resp.Success );
        Assert.NotEqual( Guid.Empty, resp.Content );
    }


    /// <summary/>
    [Fact]
    public async Task EventUpdate_ByGuid()
    {
        var id = Guid.NewGuid();

        var resp = await _resend.EventUpdateAsync( id, new EventUpdateData()
        {
            Schema = JsonDocument.Parse( "{\"plan\":\"string\"}" ).RootElement,
        } );

        Assert.NotNull( resp );
        Assert.True( resp.Success );
        Assert.NotEqual( Guid.Empty, resp.Content );
    }


    /// <summary/>
    [Fact]
    public async Task EventDelete_ByName()
    {
        var resp = await _resend.EventDeleteAsync( "user.created" );

        Assert.NotNull( resp );
        Assert.True( resp.Success );
        Assert.True( resp.Content.Deleted );
    }


    /// <summary/>
    [Fact]
    public async Task EventDelete_ByGuid()
    {
        var id = Guid.NewGuid();

        var resp = await _resend.EventDeleteAsync( id );

        Assert.NotNull( resp );
        Assert.True( resp.Success );
        Assert.Equal( id, resp.Content.Id );
        Assert.True( resp.Content.Deleted );
    }


    /// <summary/>
    [Fact]
    public async Task EventSend_WithContactId()
    {
        var contactId = Guid.Parse( "7f2e4a3b-dfbc-4e9a-8b2c-5f3a1d6e7c8b" );

        var resp = await _resend.EventSendAsync( new EventSendData()
        {
            Event = "user.created",
            ContactId = contactId,
            Payload = JsonDocument.Parse( "{\"plan\":\"pro\"}" ).RootElement,
        } );

        Assert.NotNull( resp );
        Assert.True( resp.Success );
        Assert.Equal( "user.created", resp.Content.Event );
        Assert.Equal( "event", resp.Content.Object );
    }
}
