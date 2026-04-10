using System.Net.Http.Json;
using System.Text.Json;

namespace Resend.Tests;

/// <summary />
public class DomainTests
{
    /// <summary />
    [Fact]
    public void DomainAddData_serializes_capabilities_and_tls()
    {
        var data = new DomainAddData()
        {
            DomainName = "example.com",
            TlsMode = TlsMode.Enforced,
            Capabilities = new DomainCapabilities()
            {
                Sending = "enabled",
                Receiving = "disabled",
            },
        };

        var json = JsonSerializer.Serialize( data );
        Assert.Contains( "\"tls\":\"enforced\"", json );
        Assert.Contains( "\"capabilities\"", json );
        Assert.Contains( "\"sending\":\"enabled\"", json );
    }


    /// <summary />
    [Fact]
    public void DomainAddData_serializes_tracking_fields()
    {
        var data = new DomainAddData()
        {
            DomainName = "example.com",
            ClickTracking = true,
            TrackingSubdomain = "links",
        };

        var json = JsonSerializer.Serialize( data );
        Assert.Contains( "\"click_tracking\":true", json );
        Assert.Contains( "\"tracking_subdomain\":\"links\"", json );
        Assert.DoesNotContain( "open_tracking", json );
    }


    /// <summary />
    [Fact]
    public void Domain_deserializes_tracking_and_capabilities()
    {
        const string json = """
            {
              "id": "d91cd9bd-1176-453e-8fc1-35364d380206",
              "name": "example.com",
              "status": "not_started",
              "created_at": "2023-04-26T20:21:26.347412+00:00",
              "region": "us-east-1",
              "open_tracking": true,
              "click_tracking": false,
              "tracking_subdomain": "links",
              "capabilities": {
                "sending": "enabled",
                "receiving": "disabled"
              },
              "records": []
            }
            """;

        var domain = JsonSerializer.Deserialize<Domain>( json );
        Assert.NotNull( domain );
        Assert.Equal( "links", domain!.TrackingSubdomain );
        Assert.True( domain.OpenTracking );
        Assert.False( domain.ClickTracking );
        Assert.NotNull( domain.Capabilities );
        Assert.Equal( "enabled", domain.Capabilities!.Sending );
        Assert.Equal( "disabled", domain.Capabilities.Receiving );
    }


    /// <summary />
    [Fact]
    public void DomainUpdateData_serializes_tracking_subdomain()
    {
        var data = new DomainUpdateData()
        {
            TrackClicks = true,
            TrackOpen = false,
            TrackingSubdomain = "links",
        };

        var json = JsonSerializer.Serialize( data );
        Assert.Contains( "\"tracking_subdomain\":\"links\"", json );
    }


    /// <summary />
    [Fact]
    public async Task JsonContent_Create_omits_null_tracking_subdomain_on_update()
    {
        var data = new DomainUpdateData()
        {
            TrackClicks = true,
            TrackOpen = true,
            TrackingSubdomain = null,
        };

        using var content = JsonContent.Create( data );
        var json = await content.ReadAsStringAsync();
        Assert.DoesNotContain( "tracking_subdomain", json );
    }
}
