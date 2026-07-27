using System.Net;
using System.Text;

namespace Resend.Tests;

/// <summary>
/// Tests over the requests issued by the suppression methods, against a handler which records
/// instead of answering like the API.
/// </summary>
public class SuppressionRequestTests
{
    private readonly RecordingHandler _handler;
    private readonly IResend _resend;


    /// <summary />
    public SuppressionRequestTests()
    {
        _handler = new RecordingHandler();

        var opt = new ResendClientOptions()
        {
            ApiToken = "re_test_123",
        };

        _resend = ResendClient.Create( opt, new HttpClient( _handler ) );
    }


    /// <summary />
    [Theory]
    [InlineData( SuppressionOrigin.Bounce, "bounce" )]
    [InlineData( SuppressionOrigin.Complaint, "complaint" )]
    [InlineData( SuppressionOrigin.Manual, "manual" )]
    public async Task SuppressionList_Origin( SuppressionOrigin origin, string expected )
    {
        await _resend.SuppressionListAsync( new SuppressionListQuery()
        {
            Origin = origin,
        } );

        var req = Assert.Single( _handler.Requests );
        Assert.Equal( $"/suppressions?origin={expected}", req.RequestUri!.PathAndQuery );
    }


    /// <summary />
    [Theory]
    [InlineData( "" )]
    [InlineData( "  " )]
    public async Task SuppressionRetrieve_Blank_Throws( string suppressionIdOrEmail )
    {
        await Assert.ThrowsAsync<ArgumentException>( () => _resend.SuppressionRetrieveAsync( suppressionIdOrEmail ) );

        Assert.Empty( _handler.Requests );
    }


    /// <summary />
    [Theory]
    [InlineData( "" )]
    [InlineData( "  " )]
    public async Task SuppressionRemove_Blank_Throws( string suppressionIdOrEmail )
    {
        await Assert.ThrowsAsync<ArgumentException>( () => _resend.SuppressionRemoveAsync( suppressionIdOrEmail ) );

        Assert.Empty( _handler.Requests );
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
