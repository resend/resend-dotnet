using System.Net;
using System.Text;

namespace Resend.Tests;

/// <summary />
public partial class ResendClientTests
{
    /// <summary/>
    [Fact]
    public async Task SegmentUpdate()
    {
        var segmentId = Guid.NewGuid();

        var resp = await _resend.SegmentUpdateAsync( segmentId, new SegmentData()
        {
            Name = "Renamed segment",
        } );

        Assert.NotNull( resp );
        Assert.NotNull( resp.Content );
        Assert.Equal( segmentId, resp.Content.Id );
    }


    /// <summary/>
    [Fact]
    public async Task SegmentListContacts()
    {
        var resp = await _resend.SegmentListContactsAsync( Guid.NewGuid() );

        Assert.NotNull( resp );
        Assert.NotNull( resp.Content );
        Assert.Single( resp.Content.Data );
        Assert.False( resp.Content.HasMore );
    }


    /// <summary/>
    [Fact]
    public async Task SegmentListContacts_SendsPaginationQuery()
    {
        var handler = new SegmentRecordingHandler();
        var resend = ResendClient.Create( new ResendClientOptions() { ApiToken = "re_test_123" }, new HttpClient( handler ) );
        var segmentId = Guid.NewGuid();

        await resend.SegmentListContactsAsync( segmentId, new PaginatedQuery()
        {
            Limit = 10,
            Before = "b_cursor",
            After = "a_cursor",
        } );

        var req = Assert.Single( handler.Requests );
        Assert.Equal( HttpMethod.Get, req.Method );
        Assert.Equal( $"/segments/{segmentId}/contacts?limit=10&before=b_cursor&after=a_cursor", req.RequestUri!.PathAndQuery );
    }


    private class SegmentRecordingHandler : HttpMessageHandler
    {
        public List<HttpRequestMessage> Requests { get; } = new();


        protected override Task<HttpResponseMessage> SendAsync( HttpRequestMessage request, CancellationToken cancellationToken )
        {
            Requests.Add( request );

            var resp = new HttpResponseMessage( HttpStatusCode.OK );
            resp.Content = new StringContent( """{"object":"list","has_more":false,"data":[]}""", Encoding.UTF8, "application/json" );

            return Task.FromResult( resp );
        }
    }
}
