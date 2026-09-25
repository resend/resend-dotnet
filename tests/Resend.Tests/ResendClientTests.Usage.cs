namespace Resend.Tests;

/// <summary />
public partial class ResendClientTests
{
    /// <summary />
    [Fact]
    public async Task Usage()
    {
        var resp = await _resend.UsageAsync();

        Assert.NotNull( resp );
        Assert.True( resp.Success );
        Assert.NotNull( resp.Content );

        Assert.Equal( "usage", resp.Content.Object );

        Assert.Equal( 258, resp.Content.Emails.Daily.Used );
        Assert.Null( resp.Content.Emails.Daily.Limit );
        Assert.Equal( 57, resp.Content.Emails.Daily.Sent );
        Assert.Equal( 201, resp.Content.Emails.Daily.Received );
        Assert.Equal( DateTime.Parse( "2026-07-17T00:00:00.000Z" ).ToUniversalTime(), resp.Content.Emails.Daily.MomentReset.ToUniversalTime() );

        Assert.Equal( 5442, resp.Content.Emails.Monthly.Used );
        Assert.Equal( 10000, resp.Content.Emails.Monthly.Limit );
        Assert.Equal( 1000, resp.Content.Emails.Monthly.Sent );
        Assert.Equal( 4442, resp.Content.Emails.Monthly.Received );
        Assert.Equal( DateTime.Parse( "2026-08-01T00:00:00.000Z" ).ToUniversalTime(), resp.Content.Emails.Monthly.MomentReset.ToUniversalTime() );

        Assert.Equal( 85000, resp.Content.Contacts.Used );
        Assert.Equal( 150000, resp.Content.Contacts.Limit );

        Assert.Equal( 2, resp.Content.Segments.Used );
        Assert.Equal( 3, resp.Content.Segments.Limit );

        Assert.Equal( 100, resp.Content.Broadcasts.Used );
        Assert.Null( resp.Content.Broadcasts.Limit );

        Assert.Equal( 0, resp.Content.AiCredits.Used );
        Assert.Equal( 500, resp.Content.AiCredits.Limit );
        Assert.NotNull( resp.Content.AiCredits.MomentNextIncrease );

        Assert.Equal( 0, resp.Content.AutomationRuns.Used );
        Assert.Equal( 1000, resp.Content.AutomationRuns.Limit );
        Assert.Equal( DateTime.Parse( "2026-08-01T00:00:00.000Z" ).ToUniversalTime(), resp.Content.AutomationRuns.MomentReset.ToUniversalTime() );

        Assert.Equal( 1, resp.Content.Domains.Used );
        Assert.Equal( 1000, resp.Content.Domains.Limit );

        Assert.Equal( 10, resp.Content.RateLimit.Limit );
        Assert.Equal( "1000ms", resp.Content.RateLimit.Duration );
    }
}
