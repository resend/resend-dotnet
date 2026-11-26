using System.Net;
using System.Text;

namespace Resend.Tests;

/// <summary>
/// Tests over the requests issued by the receiving methods, against a handler which records
/// instead of answering like the API.
/// </summary>
public class ReceivingRequestTests
{
    private readonly RecordingHandler _handler;
    private readonly IResend _resend;


    /// <summary />
    public ReceivingRequestTests()
    {
        _handler = new RecordingHandler();

        var opt = new ResendClientOptions()
        {
            ApiToken = "re_test_123",
        };

        _resend = ResendClient.Create( opt, new HttpClient( _handler ) );
    }


    /// <summary />
    [Fact]
    public async Task ReceivedEmailList_Filters()
    {
        await _resend.ReceivedEmailListAsync( new ReceivedEmailListQuery()
        {
            To = new List<string>() { "support@acme.com", "billing@acme.com" },
            StartDate = "2026-11-01",
            EndDate = "2026-11-15T12:00:00Z",
            Limit = 25,
            After = "cursor123",
        } );

        var req = Assert.Single( _handler.Requests );
        Assert.Equal(
            "/emails/receiving?limit=25&after=cursor123&to=support@acme.com,billing@acme.com&start_date=2026-11-01&end_date=2026-11-15T12%3A00%3A00Z",
            req.RequestUri!.PathAndQuery );
    }


    /// <summary />
    [Fact]
    public async Task ReceivedEmailList_UnsetFiltersOmitted()
    {
        await _resend.ReceivedEmailListAsync( new ReceivedEmailListQuery()
        {
            To = new List<string>() { "support@acme.com" },
        } );

        var req = Assert.Single( _handler.Requests );
        Assert.Equal( "/emails/receiving?to=support@acme.com", req.RequestUri!.PathAndQuery );
    }


    /// <summary />
    [Fact]
    public async Task ReceivedEmailList_PaginatedQuery()
    {
        await _resend.ReceivedEmailListAsync( new PaginatedQuery()
        {
            Limit = 10,
        } );

        var req = Assert.Single( _handler.Requests );
        Assert.Equal( "/emails/receiving?limit=10", req.RequestUri!.PathAndQuery );
    }


    /// <summary />
    [Fact]
    public async Task ReceivedEmailList_FiltersTypedAsPaginatedQuery()
    {
        PaginatedQuery query = new ReceivedEmailListQuery()
        {
            StartDate = "2026-11-01",
        };

        await _resend.ReceivedEmailListAsync( query );

        var req = Assert.Single( _handler.Requests );
        Assert.Equal( "/emails/receiving?start_date=2026-11-01", req.RequestUri!.PathAndQuery );
    }


    /// <summary />
    private class RecordingHandler : HttpMessageHandler
    {
        /// <summary />
        public List<HttpRequestMessage> Requests { get; } = new();


        /// <inheritdoc />
        protected override Task<HttpResponseMessage> SendAsync( HttpRequestMessage request, CancellationToken cancellationToken )
        {
            Requests.Add( request );

            var resp = new HttpResponseMessage( HttpStatusCode.OK );
            resp.Content = new StringContent( """{"has_more":false,"data":[]}""", Encoding.UTF8, "application/json" );

            return Task.FromResult( resp );
        }
    }
}
