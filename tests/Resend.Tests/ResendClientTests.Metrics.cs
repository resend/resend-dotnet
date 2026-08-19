namespace Resend.Tests;

/// <summary />
public partial class ResendClientTests
{
    /// <summary />
    [Fact]
    public async Task EmailMetricsDefault()
    {
        var resp = await _resend.EmailMetricsAsync();

        Assert.NotNull( resp );
        Assert.True( resp.Success );
        Assert.NotNull( resp.Content );
        Assert.Equal( "metrics", resp.Content.Object );
        Assert.Equal( MetricsGranularity.Daily, resp.Content.Granularity );
        Assert.Empty( resp.Content.Dimensions );
        Assert.Null( resp.Content.Data );
        Assert.Equal( Enum.GetValues<MetricType>().Length, resp.Content.Metrics.Count );
        Assert.Equal( Enum.GetValues<MetricType>().Length, resp.Content.Totals.Count );
    }


    /// <summary />
    [Fact]
    public async Task EmailMetricsByDimensionPeriod()
    {
        var resp = await _resend.EmailMetricsAsync( new EmailMetricsQuery()
        {
            Dimensions = new List<MetricDimension>() { MetricDimension.Period },
        } );

        Assert.NotNull( resp );
        Assert.True( resp.Success );
        Assert.NotNull( resp.Content );
        Assert.Equal( new[] { MetricDimension.Period }, resp.Content.Dimensions );
        Assert.NotNull( resp.Content.Data );
        Assert.Single( resp.Content.Data );
        Assert.NotNull( resp.Content.Data[ 0 ].Period );
        Assert.Null( resp.Content.Data[ 0 ].DomainId );
        Assert.Null( resp.Content.Data[ 0 ].EmailId );
        Assert.Null( resp.Content.Data[ 0 ].BroadcastId );
    }


    /// <summary />
    [Fact]
    public async Task EmailMetricsByDimensionDomain()
    {
        var resp = await _resend.EmailMetricsAsync( new EmailMetricsQuery()
        {
            Dimensions = new List<MetricDimension>() { MetricDimension.Domain },
        } );

        Assert.NotNull( resp );
        Assert.True( resp.Success );
        Assert.NotNull( resp.Content );
        Assert.Equal( new[] { MetricDimension.Domain }, resp.Content.Dimensions );
        Assert.NotNull( resp.Content.Data );
        Assert.Single( resp.Content.Data );
        Assert.NotNull( resp.Content.Data[ 0 ].DomainId );
        Assert.Equal( "example.com", resp.Content.Data[ 0 ].DomainName );
    }


    /// <summary />
    [Fact]
    public async Task EmailMetricsByDimensionEmail()
    {
        var resp = await _resend.EmailMetricsAsync( new EmailMetricsQuery()
        {
            Dimensions = new List<MetricDimension>() { MetricDimension.Email },
        } );

        Assert.NotNull( resp );
        Assert.True( resp.Success );
        Assert.NotNull( resp.Content );
        Assert.Equal( new[] { MetricDimension.Email }, resp.Content.Dimensions );
        Assert.NotNull( resp.Content.Data );
        Assert.Single( resp.Content.Data );
        Assert.NotNull( resp.Content.Data[ 0 ].EmailId );
    }


    /// <summary />
    [Fact]
    public async Task EmailMetricsByDimensionBroadcast()
    {
        var resp = await _resend.EmailMetricsAsync( new EmailMetricsQuery()
        {
            Dimensions = new List<MetricDimension>() { MetricDimension.Broadcast },
        } );

        Assert.NotNull( resp );
        Assert.True( resp.Success );
        Assert.NotNull( resp.Content );
        Assert.Equal( new[] { MetricDimension.Broadcast }, resp.Content.Dimensions );
        Assert.NotNull( resp.Content.Data );
        Assert.Single( resp.Content.Data );
        Assert.NotNull( resp.Content.Data[ 0 ].BroadcastId );
        Assert.Equal( "July Newsletter", resp.Content.Data[ 0 ].BroadcastName );
    }


    /// <summary>
    /// Single-value filters must reach the API in the `domain_id` query parameter.
    /// </summary>
    [Fact]
    public async Task EmailMetricsFilterByDomainIdSingle()
    {
        var domainId = Guid.NewGuid();

        var resp = await _resend.EmailMetricsAsync( new EmailMetricsQuery()
        {
            Dimensions = new List<MetricDimension>() { MetricDimension.Domain },
            DomainId = new List<Guid>() { domainId },
        } );

        Assert.NotNull( resp );
        Assert.True( resp.Success );
        Assert.NotNull( resp.Content );
        Assert.Equal( domainId, resp.Content.Data![ 0 ].DomainId );
    }


    /// <summary>
    /// Multiple filter values must be comma-joined into a single `domain_id` query parameter.
    /// </summary>
    [Fact]
    public async Task EmailMetricsFilterByDomainIdMultiple()
    {
        var domainIds = new List<Guid>() { Guid.NewGuid(), Guid.NewGuid() };

        var resp = await _resend.EmailMetricsAsync( new EmailMetricsQuery()
        {
            Dimensions = new List<MetricDimension>() { MetricDimension.Domain },
            DomainId = domainIds,
        } );

        Assert.NotNull( resp );
        Assert.True( resp.Success );
        Assert.NotNull( resp.Content );
        Assert.Equal( domainIds[ 0 ], resp.Content.Data![ 0 ].DomainId );
    }


    /// <summary>
    /// Single-value filters must reach the API in the `email_id` query parameter.
    /// </summary>
    [Fact]
    public async Task EmailMetricsFilterByEmailIdSingle()
    {
        var emailId = Guid.NewGuid();

        var resp = await _resend.EmailMetricsAsync( new EmailMetricsQuery()
        {
            Dimensions = new List<MetricDimension>() { MetricDimension.Email },
            EmailId = new List<Guid>() { emailId },
        } );

        Assert.NotNull( resp );
        Assert.True( resp.Success );
        Assert.NotNull( resp.Content );
        Assert.Equal( emailId, resp.Content.Data![ 0 ].EmailId );
    }


    /// <summary>
    /// Multiple filter values must be comma-joined into a single `email_id` query parameter.
    /// </summary>
    [Fact]
    public async Task EmailMetricsFilterByEmailIdMultiple()
    {
        var emailIds = new List<Guid>() { Guid.NewGuid(), Guid.NewGuid() };

        var resp = await _resend.EmailMetricsAsync( new EmailMetricsQuery()
        {
            Dimensions = new List<MetricDimension>() { MetricDimension.Email },
            EmailId = emailIds,
        } );

        Assert.NotNull( resp );
        Assert.True( resp.Success );
        Assert.NotNull( resp.Content );
        Assert.Equal( emailIds[ 0 ], resp.Content.Data![ 0 ].EmailId );
    }


    /// <summary>
    /// Single-value filters must reach the API in the `broadcast_id` query parameter.
    /// </summary>
    [Fact]
    public async Task EmailMetricsFilterByBroadcastIdSingle()
    {
        var broadcastId = Guid.NewGuid();

        var resp = await _resend.EmailMetricsAsync( new EmailMetricsQuery()
        {
            Dimensions = new List<MetricDimension>() { MetricDimension.Broadcast },
            BroadcastId = new List<Guid>() { broadcastId },
        } );

        Assert.NotNull( resp );
        Assert.True( resp.Success );
        Assert.NotNull( resp.Content );
        Assert.Equal( broadcastId, resp.Content.Data![ 0 ].BroadcastId );
    }


    /// <summary>
    /// Multiple filter values must be comma-joined into a single `broadcast_id` query parameter.
    /// </summary>
    [Fact]
    public async Task EmailMetricsFilterByBroadcastIdMultiple()
    {
        var broadcastIds = new List<Guid>() { Guid.NewGuid(), Guid.NewGuid() };

        var resp = await _resend.EmailMetricsAsync( new EmailMetricsQuery()
        {
            Dimensions = new List<MetricDimension>() { MetricDimension.Broadcast },
            BroadcastId = broadcastIds,
        } );

        Assert.NotNull( resp );
        Assert.True( resp.Success );
        Assert.NotNull( resp.Content );
        Assert.Equal( broadcastIds[ 0 ], resp.Content.Data![ 0 ].BroadcastId );
    }


    /// <summary>
    /// `metrics` and `granularity` are echoed back by the API and can be asserted on directly;
    /// `timezone` isn't part of the response shape, so this only proves the request round-trips
    /// successfully with it set.
    /// </summary>
    [Fact]
    public async Task EmailMetricsWithMetricsGranularityAndTimezone()
    {
        var resp = await _resend.EmailMetricsAsync( new EmailMetricsQuery()
        {
            Metrics = new List<MetricType>() { MetricType.Delivered, MetricType.Opened },
            Granularity = MetricsGranularity.Hourly,
            Timezone = "America/New_York",
        } );

        Assert.NotNull( resp );
        Assert.True( resp.Success );
        Assert.NotNull( resp.Content );
        Assert.Equal( new[] { MetricType.Delivered, MetricType.Opened }, resp.Content.Metrics );
        Assert.Equal( MetricsGranularity.Hourly, resp.Content.Granularity );
        Assert.Equal( 2, resp.Content.Totals.Count );
        Assert.True( resp.Content.Totals.ContainsKey( "delivered" ) );
        Assert.True( resp.Content.Totals.ContainsKey( "opened" ) );
    }
}
