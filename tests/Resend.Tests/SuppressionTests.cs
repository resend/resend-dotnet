using Resend.Payloads;
using System.Net.Http.Json;
using System.Text.Json;

namespace Resend.Tests;

/// <summary />
public class SuppressionTests
{
    /// <summary />
    [Fact]
    public void Suppression_deserializes_documented_payload()
    {
        const string json = """
            {
              "object": "suppression",
              "id": "e169aa45-1ecf-4183-9955-b1499d5701d3",
              "email": "steve.wozniak@gmail.com",
              "origin": "bounce",
              "source_id": "479e3145-dd38-476b-932c-529ceb705947",
              "created_at": "2023-10-06T23:47:56.678Z"
            }
            """;

        var suppression = JsonSerializer.Deserialize<Suppression>( json );

        Assert.NotNull( suppression );
        Assert.Equal( "suppression", suppression!.Object );
        Assert.Equal( Guid.Parse( "e169aa45-1ecf-4183-9955-b1499d5701d3" ), suppression.Id );
        Assert.Equal( "steve.wozniak@gmail.com", suppression.Email );
        Assert.Equal( SuppressionOrigin.Bounce, suppression.Origin );
        Assert.Equal( "479e3145-dd38-476b-932c-529ceb705947", suppression.SourceId );
    }


    /// <summary>
    /// source_id is a free-text column, so a non-UUID value must not fail deserialization.
    /// </summary>
    [Fact]
    public void Suppression_deserializes_non_uuid_source_id()
    {
        const string json = """
            {
              "object": "suppression",
              "id": "e169aa45-1ecf-4183-9955-b1499d5701d3",
              "email": "steve.wozniak@gmail.com",
              "origin": "bounce",
              "source_id": "backfill-2026-07",
              "created_at": "2023-10-06T23:47:56.678Z"
            }
            """;

        var suppression = JsonSerializer.Deserialize<Suppression>( json );

        Assert.NotNull( suppression );
        Assert.Equal( "backfill-2026-07", suppression!.SourceId );
    }


    /// <summary>
    /// A non-UUID source_id on one list entry would otherwise throw away the whole page.
    /// </summary>
    [Fact]
    public void SuppressionList_deserializes_non_uuid_source_id()
    {
        const string json = """
            {
              "object": "list",
              "has_more": false,
              "data": [
                {
                  "id": "e169aa45-1ecf-4183-9955-b1499d5701d3",
                  "email": "steve.wozniak@gmail.com",
                  "origin": "bounce",
                  "source_id": "backfill-2026-07",
                  "created_at": "2023-10-06T23:47:56.678Z"
                },
                {
                  "id": "8d1f0f4a-2b6e-4c3d-9f1a-0e5b7c9d2a11",
                  "email": "carolina@resend.com",
                  "origin": "manual",
                  "source_id": null,
                  "created_at": "2023-10-06T23:47:56.678Z"
                }
              ]
            }
            """;

        var page = JsonSerializer.Deserialize<PaginatedResult<SuppressionSummary>>( json );

        Assert.NotNull( page );
        Assert.Equal( 2, page!.Data.Count );
        Assert.Equal( "backfill-2026-07", page.Data[ 0 ].SourceId );
        Assert.Null( page.Data[ 1 ].SourceId );
    }


    /// <summary>
    /// source_id is null for manual-origin suppressions.
    /// </summary>
    [Fact]
    public void Suppression_deserializes_null_source_id()
    {
        const string json = """
            {
              "object": "suppression",
              "id": "e169aa45-1ecf-4183-9955-b1499d5701d3",
              "email": "steve.wozniak@gmail.com",
              "origin": "manual",
              "source_id": null,
              "created_at": "2023-10-06T23:47:56.678Z"
            }
            """;

        var suppression = JsonSerializer.Deserialize<Suppression>( json );

        Assert.NotNull( suppression );
        Assert.Equal( SuppressionOrigin.Manual, suppression!.Origin );
        Assert.Null( suppression.SourceId );
    }


    /// <summary>
    /// A missing source_id key must deserialize as cleanly as an explicit null.
    /// </summary>
    [Fact]
    public void Suppression_deserializes_missing_source_id()
    {
        const string json = """
            {
              "object": "suppression",
              "id": "e169aa45-1ecf-4183-9955-b1499d5701d3",
              "email": "steve.wozniak@gmail.com",
              "origin": "complaint",
              "created_at": "2023-10-06T23:47:56.678Z"
            }
            """;

        var suppression = JsonSerializer.Deserialize<Suppression>( json );

        Assert.NotNull( suppression );
        Assert.Null( suppression!.SourceId );
        Assert.Equal( SuppressionOrigin.Complaint, suppression.Origin );
    }


    /// <summary>
    /// created_at is a Postgres timestamptz read in string mode, so the wire value is not
    /// strict ISO 8601 -- space separator and a two-digit offset must still parse.
    /// </summary>
    [Fact]
    public void Suppression_deserializes_postgres_timestamp()
    {
        const string json = """
            {
              "object": "suppression",
              "id": "e169aa45-1ecf-4183-9955-b1499d5701d3",
              "email": "steve.wozniak@gmail.com",
              "origin": "manual",
              "source_id": null,
              "created_at": "2026-07-24 18:30:00.123456+00"
            }
            """;

        var suppression = JsonSerializer.Deserialize<Suppression>( json );

        Assert.NotNull( suppression );
        Assert.Equal( DateTimeKind.Utc, suppression!.MomentCreated.Kind );
        Assert.Equal( new DateOnly( 2026, 7, 24 ), DateOnly.FromDateTime( suppression.MomentCreated ) );
        Assert.Equal( 18, suppression.MomentCreated.Hour );
        Assert.Equal( 30, suppression.MomentCreated.Minute );
    }


    /// <summary>
    /// The same non-ISO wire format arrives on list entries.
    /// </summary>
    [Fact]
    public void SuppressionSummary_deserializes_postgres_timestamp()
    {
        const string json = """
            {
              "id": "e169aa45-1ecf-4183-9955-b1499d5701d3",
              "email": "steve.wozniak@gmail.com",
              "origin": "manual",
              "source_id": null,
              "created_at": "2026-07-24 18:30:00.123456+00"
            }
            """;

        var entry = JsonSerializer.Deserialize<SuppressionSummary>( json );

        Assert.NotNull( entry );
        Assert.Equal( DateTimeKind.Utc, entry!.MomentCreated.Kind );
        Assert.Equal( 2026, entry.MomentCreated.Year );
        Assert.Equal( 18, entry.MomentCreated.Hour );
    }


    /// <summary>
    /// Real payloads do not guarantee property order; deserialization must not depend on it.
    /// </summary>
    [Fact]
    public void Suppression_deserializes_out_of_order()
    {
        const string json = """
            {
              "created_at": "2023-10-06T23:47:56.678Z",
              "source_id": null,
              "origin": "manual",
              "email": "steve.wozniak@gmail.com",
              "id": "e169aa45-1ecf-4183-9955-b1499d5701d3",
              "object": "suppression"
            }
            """;

        var suppression = JsonSerializer.Deserialize<Suppression>( json );

        Assert.NotNull( suppression );
        Assert.Equal( SuppressionOrigin.Manual, suppression!.Origin );
        Assert.Equal( "steve.wozniak@gmail.com", suppression.Email );
        Assert.Equal( Guid.Parse( "e169aa45-1ecf-4183-9955-b1499d5701d3" ), suppression.Id );
    }


    /// <summary>
    /// The list endpoint selects exactly id/email/origin/source_id/created_at -- entries
    /// carry no `object` discriminator, unlike the single-suppression response.
    /// </summary>
    [Fact]
    public void SuppressionList_deserializes_server_payload()
    {
        const string json = """
            {
              "object": "list",
              "has_more": false,
              "data": [
                {
                  "id": "e169aa45-1ecf-4183-9955-b1499d5701d3",
                  "email": "steve.wozniak@gmail.com",
                  "origin": "manual",
                  "source_id": null,
                  "created_at": "2023-10-06T23:47:56.678Z"
                }
              ]
            }
            """;

        var page = JsonSerializer.Deserialize<PaginatedResult<SuppressionSummary>>( json );

        Assert.NotNull( page );
        Assert.False( page!.HasMore );
        Assert.Single( page.Data );
        Assert.Equal( SuppressionOrigin.Manual, page.Data[ 0 ].Origin );
        Assert.Null( page.Data[ 0 ].SourceId );
        Assert.Equal( "steve.wozniak@gmail.com", page.Data[ 0 ].Email );
    }


    /// <summary>
    /// The list entry type must not advertise a property the API never sends.
    /// </summary>
    [Fact]
    public void SuppressionSummary_has_no_object_property()
    {
        Assert.Null( typeof( SuppressionSummary ).GetProperty( "Object" ) );
        Assert.NotNull( typeof( Suppression ).GetProperty( "Object" ) );
    }


    /// <summary />
    [Fact]
    public void SuppressionOrigin_serializes_wire_values()
    {
        Assert.Equal( "\"bounce\"", JsonSerializer.Serialize( SuppressionOrigin.Bounce ) );
        Assert.Equal( "\"complaint\"", JsonSerializer.Serialize( SuppressionOrigin.Complaint ) );
        Assert.Equal( "\"manual\"", JsonSerializer.Serialize( SuppressionOrigin.Manual ) );
    }


    /// <summary />
    [Fact]
    public void SuppressionAddRequest_serializes_email()
    {
        var request = new SuppressionAddRequest()
        {
            Email = "steve.wozniak@gmail.com",
        };

        var json = JsonSerializer.Serialize( request );

        Assert.Equal( """{"email":"steve.wozniak@gmail.com"}""", json );
    }


    /// <summary>
    /// Batch remove takes either emails or ids; the API rejects an explicit null, so the
    /// unset one must be absent from the body.
    /// </summary>
    [Fact]
    public async Task SuppressionBatchRemoveRequest_omits_unset_ids()
    {
        var request = new SuppressionBatchRemoveRequest()
        {
            Emails = new List<string>() { "steve.wozniak@gmail.com" },
        };

        using var content = JsonContent.Create( request );
        var json = await content.ReadAsStringAsync();

        Assert.Contains( "\"emails\":[\"steve.wozniak@gmail.com\"]", json );
        Assert.DoesNotContain( "ids", json );
        Assert.DoesNotContain( "null", json );
    }


    /// <summary />
    [Fact]
    public async Task SuppressionBatchRemoveRequest_omits_unset_emails()
    {
        var request = new SuppressionBatchRemoveRequest()
        {
            Ids = new List<Guid>() { Guid.Parse( "e169aa45-1ecf-4183-9955-b1499d5701d3" ) },
        };

        using var content = JsonContent.Create( request );
        var json = await content.ReadAsStringAsync();

        Assert.Contains( "\"ids\":[\"e169aa45-1ecf-4183-9955-b1499d5701d3\"]", json );
        Assert.DoesNotContain( "emails", json );
        Assert.DoesNotContain( "null", json );
    }


    /// <summary>
    /// Batch remove returns only the rows actually deleted, so a two-identifier request can
    /// come back with one entry -- and never a placeholder for the missing one.
    /// </summary>
    [Fact]
    public void SuppressionBatchRemove_deserializes_shorter_response()
    {
        const string json = """
            {
              "data": [
                {
                  "object": "suppression",
                  "id": "e169aa45-1ecf-4183-9955-b1499d5701d3",
                  "deleted": true
                }
              ]
            }
            """;

        var result = JsonSerializer.Deserialize<ListOf<SuppressionRemoveResult>>( json );

        Assert.NotNull( result );
        Assert.Single( result!.Data );
        Assert.Equal( Guid.Parse( "e169aa45-1ecf-4183-9955-b1499d5701d3" ), result.Data[ 0 ].Id );
        Assert.True( result.Data[ 0 ].Deleted );
    }


    /// <summary>
    /// Batch add dedupes server-side, so the identifier list can be shorter than the
    /// addresses sent.
    /// </summary>
    [Fact]
    public void SuppressionBatchAdd_deserializes_shorter_response()
    {
        const string json = """
            {
              "data": [
                {
                  "object": "suppression",
                  "id": "e169aa45-1ecf-4183-9955-b1499d5701d3"
                }
              ]
            }
            """;

        var result = JsonSerializer.Deserialize<ListOf<ObjectId>>( json );

        Assert.NotNull( result );
        Assert.Single( result!.Data );
        Assert.Equal( Guid.Parse( "e169aa45-1ecf-4183-9955-b1499d5701d3" ), result.Data[ 0 ].Id );
    }
}
