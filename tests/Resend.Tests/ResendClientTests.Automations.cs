using System.Text.Json;

namespace Resend.Tests;

/// <summary />
public partial class ResendClientTests
{
    /// <summary/>
    [Fact]
    public async Task AutomationCreate()
    {
        var resp = await _resend.AutomationCreateAsync( new AutomationCreateData()
        {
            Name = "Welcome series",
            Steps =
            [
                new AutomationStepData()
                {
                    Ref = "trigger",
                    Type = "trigger",
                    Config = JsonDocument.Parse( "{\"event_name\":\"user.created\"}" ).RootElement,
                },
            ],
            Connections = [],
        } );

        Assert.NotNull( resp );
        Assert.True( resp.Success );
        Assert.NotEqual( Guid.Empty, resp.Content );
    }


    /// <summary/>
    [Fact]
    public async Task AutomationUpdate()
    {
        var id = Guid.NewGuid();

        var resp = await _resend.AutomationUpdateAsync( id, new AutomationUpdateData()
        {
            Status = "enabled",
        } );

        Assert.NotNull( resp );
        Assert.True( resp.Success );
        Assert.Equal( id, resp.Content );
    }


    /// <summary/>
    [Fact]
    public async Task AutomationRetrieve()
    {
        var id = Guid.Parse( "c9b16d4f-ba6c-4e2e-b044-6bf4404e57fd" );

        var resp = await _resend.AutomationRetrieveAsync( id );

        Assert.NotNull( resp );
        Assert.True( resp.Success );
        Assert.Equal( id, resp.Content.Id );
        Assert.Equal( "Welcome series", resp.Content.Name );
        Assert.Equal( "trigger", resp.Content.Steps[ 0 ].Type );
    }


    /// <summary/>
    [Fact]
    public async Task AutomationList()
    {
        var resp = await _resend.AutomationListAsync();

        Assert.NotNull( resp );
        Assert.True( resp.Success );
        Assert.NotNull( resp.Content );
        Assert.False( resp.Content.HasMore );
        Assert.Equal( 2, resp.Content.Data.Count );
        Assert.Equal( "Welcome series", resp.Content.Data[ 0 ].Name );
    }


    /// <summary/>
    [Fact]
    public async Task AutomationList_WithQuery()
    {
        var resp = await _resend.AutomationListAsync( new AutomationListQuery()
        {
            Limit = 20,
            Status = "enabled",
            After = Guid.NewGuid().ToString(),
        } );

        Assert.NotNull( resp );
        Assert.True( resp.Success );
        Assert.NotNull( resp.Content );
    }


    /// <summary/>
    [Fact]
    public async Task AutomationStop()
    {
        var id = Guid.NewGuid();

        var resp = await _resend.AutomationStopAsync( id );

        Assert.NotNull( resp );
        Assert.True( resp.Success );
        Assert.Equal( id, resp.Content.Id );
        Assert.Equal( "disabled", resp.Content.Status );
    }


    /// <summary/>
    [Fact]
    public async Task AutomationDelete()
    {
        var id = Guid.NewGuid();

        var resp = await _resend.AutomationDeleteAsync( id );

        Assert.NotNull( resp );
        Assert.True( resp.Success );
        Assert.True( resp.Content.Deleted );
    }


    /// <summary/>
    [Fact]
    public async Task AutomationRunList()
    {
        var automationId = Guid.NewGuid();

        var resp = await _resend.AutomationRunListAsync( automationId );

        Assert.NotNull( resp );
        Assert.True( resp.Success );
        Assert.Equal( 2, resp.Content.Data.Count );
        Assert.Equal( "completed", resp.Content.Data[ 0 ].Status );
        Assert.Null( resp.Content.Data[ 1 ].MomentCompleted );
    }


    /// <summary/>
    [Fact]
    public async Task AutomationRunList_WithQuery()
    {
        var automationId = Guid.NewGuid();

        var resp = await _resend.AutomationRunListAsync( automationId, new AutomationRunListQuery()
        {
            Limit = 10,
            Status = "running,completed",
        } );

        Assert.NotNull( resp );
        Assert.True( resp.Success );
    }


    /// <summary/>
    [Fact]
    public async Task AutomationRunRetrieve()
    {
        var automationId = Guid.NewGuid();
        var runId = Guid.Parse( "a1b2c3d4-e5f6-7890-abcd-ef1234567890" );

        var resp = await _resend.AutomationRunRetrieveAsync( automationId, runId );

        Assert.NotNull( resp );
        Assert.True( resp.Success );
        Assert.Equal( runId, resp.Content.Id );
        Assert.Equal( 2, resp.Content.Steps.Count );
        Assert.Equal( "trigger", resp.Content.Steps[ 0 ].Type );
    }
}
