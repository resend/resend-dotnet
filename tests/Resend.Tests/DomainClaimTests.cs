using System.Net.Http.Json;
using System.Text.Json;

namespace Resend.Tests;

/// <summary />
public class DomainClaimTests
{
    /// <summary />
    [Fact]
    public void DomainClaimData_serializes_name_and_region()
    {
        var data = new DomainClaimData()
        {
            DomainName = "example.com",
            Region = DeliveryRegion.UsEast1,
        };

        var json = JsonSerializer.Serialize( data );
        Assert.Contains( "\"name\":\"example.com\"", json );
        Assert.Contains( "\"region\":\"us-east-1\"", json );
    }


    /// <summary />
    [Fact]
    public async Task JsonContent_Create_omits_null_optionals()
    {
        var data = new DomainClaimData()
        {
            DomainName = "example.com",
        };

        using var content = JsonContent.Create( data );
        var json = await content.ReadAsStringAsync();

        Assert.DoesNotContain( "region", json );
        Assert.DoesNotContain( "custom_return_path", json );
        Assert.DoesNotContain( "open_tracking", json );
        Assert.DoesNotContain( "tracking_subdomain", json );
    }


    /// <summary />
    [Fact]
    public void DomainClaim_deserializes_documented_payload()
    {
        const string json = """
            {
              "object": "domain_claim",
              "id": "dacf4072-4119-4d88-932f-6c6126d3a9d1",
              "name": "example.com",
              "status": "pending",
              "domain_id": "d91cd9bd-1176-453e-8fc1-35364d380206",
              "region": "us-east-1",
              "record": {
                "type": "TXT",
                "name": "example.com",
                "value": "resend-domain-verification=3f8a1c2d4e5b6a7f8091a2b3c4d5e6f7",
                "ttl": "Auto"
              },
              "blocked_reason": null,
              "failure_reason": null,
              "created_at": "2026-06-16 17:12:02.059593+00",
              "expires_at": "2026-06-23 17:12:02.059593+00"
            }
            """;

        var claim = JsonSerializer.Deserialize<DomainClaim>( json );

        Assert.NotNull( claim );
        Assert.Equal( "domain_claim", claim!.Object );
        Assert.Equal( Guid.Parse( "dacf4072-4119-4d88-932f-6c6126d3a9d1" ), claim.Id );
        Assert.Equal( "example.com", claim.Name );
        Assert.Equal( DomainClaimStatus.Pending, claim.Status );
        Assert.Equal( Guid.Parse( "d91cd9bd-1176-453e-8fc1-35364d380206" ), claim.DomainId );
        Assert.Equal( DeliveryRegion.UsEast1, claim.Region );
        Assert.Null( claim.BlockedReason );
        Assert.Null( claim.FailureReason );

        Assert.NotNull( claim.Record );
        Assert.Equal( "TXT", claim.Record.RecordType );
        Assert.Equal( "example.com", claim.Record.Name );
        Assert.Equal( "Auto", claim.Record.TimeToLive );
    }


    /// <summary />
    [Fact]
    public void DomainClaim_deserializes_blocked_reason()
    {
        const string json = """
            {
              "object": "domain_claim",
              "id": "dacf4072-4119-4d88-932f-6c6126d3a9d1",
              "name": "example.com",
              "status": "blocked",
              "domain_id": "d91cd9bd-1176-453e-8fc1-35364d380206",
              "region": "us-east-1",
              "record": {
                "type": "TXT",
                "name": "example.com",
                "value": "resend-domain-verification=abc",
                "ttl": "Auto"
              },
              "blocked_reason": "recent_owner_activity",
              "failure_reason": null,
              "created_at": "2026-06-16T17:12:02.059593+00:00",
              "expires_at": "2026-06-23T17:12:02.059593+00:00"
            }
            """;

        var claim = JsonSerializer.Deserialize<DomainClaim>( json );

        Assert.NotNull( claim );
        Assert.Equal( DomainClaimStatus.Blocked, claim!.Status );
        Assert.Equal( "recent_owner_activity", claim.BlockedReason );
    }


    /// <summary>
    /// blocked_reason is free-text on the API, so an unknown value must not break deserialization.
    /// </summary>
    [Fact]
    public void DomainClaim_deserializes_unknown_blocked_reason()
    {
        const string json = """
            {
              "object": "domain_claim",
              "id": "dacf4072-4119-4d88-932f-6c6126d3a9d1",
              "name": "example.com",
              "status": "blocked",
              "domain_id": "d91cd9bd-1176-453e-8fc1-35364d380206",
              "region": "us-east-1",
              "record": {
                "type": "TXT",
                "name": "example.com",
                "value": "resend-domain-verification=abc",
                "ttl": "Auto"
              },
              "blocked_reason": "some_future_reason",
              "failure_reason": null,
              "created_at": "2026-06-16 17:12:02.059593+00",
              "expires_at": "2026-06-23 17:12:02.059593+00"
            }
            """;

        var claim = JsonSerializer.Deserialize<DomainClaim>( json );

        Assert.NotNull( claim );
        Assert.Equal( "some_future_reason", claim!.BlockedReason );
    }


    /// <summary>
    /// Real payloads do not guarantee property order; deserialization must not depend on it.
    /// </summary>
    [Fact]
    public void DomainClaim_deserializes_out_of_order()
    {
        const string json = """
            {
              "expires_at": "2026-06-23T17:12:02.059593+00:00",
              "created_at": "2026-06-16T17:12:02.059593+00:00",
              "record": {
                "ttl": "Auto",
                "value": "resend-domain-verification=abc",
                "name": "example.com",
                "type": "TXT"
              },
              "region": "eu-west-1",
              "status": "verified",
              "name": "example.com",
              "domain_id": "d91cd9bd-1176-453e-8fc1-35364d380206",
              "id": "dacf4072-4119-4d88-932f-6c6126d3a9d1",
              "object": "domain_claim"
            }
            """;

        var claim = JsonSerializer.Deserialize<DomainClaim>( json );

        Assert.NotNull( claim );
        Assert.Equal( DomainClaimStatus.Verified, claim!.Status );
        Assert.Equal( DeliveryRegion.EuWest1, claim.Region );
        Assert.Equal( "example.com", claim.Name );
        Assert.Equal( "TXT", claim.Record.RecordType );
    }
}
