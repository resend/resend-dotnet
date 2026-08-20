using System.Net;
using System.Text;

namespace Resend.Tests;

/// <summary>
/// Tests over the requests issued by the api key methods, against a handler which records
/// instead of answering like the API.
/// </summary>
public class ApiKeyRequestTests
{
    private readonly RecordingHandler _handler;
    private readonly IResend _resend;


    /// <summary />
    public ApiKeyRequestTests()
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
    public async Task ApiKeyUpdate_SendsPatchWithName()
    {
        var apiKeyId = Guid.NewGuid();

        await _resend.ApiKeyUpdateAsync( apiKeyId, "renamed key" );

        var req = Assert.Single( _handler.Requests );
        Assert.Equal( HttpMethod.Patch, req.Method );
        Assert.Equal( $"/api-keys/{apiKeyId}", req.RequestUri!.PathAndQuery );

        var body = await req.Content!.ReadAsStringAsync();
        Assert.Contains( "\"name\":\"renamed key\"", body );
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
            resp.Content = new StringContent( $$"""{"object":"api_key","id":"{{Guid.NewGuid()}}"}""", Encoding.UTF8, "application/json" );

            return Task.FromResult( resp );
        }
    }
}
