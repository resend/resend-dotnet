using System.Net;
using System.Text;

namespace Resend.Tests;

/// <summary>
/// Tests over the requests issued by the domain verify and Forge methods, against a
/// handler which records instead of answering like the API.
/// </summary>
public class DomainRequestTests
{
    private readonly RecordingHandler _handler;
    private readonly IResend _resend;


    /// <summary />
    public DomainRequestTests()
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
    public async Task DomainVerify_WithoutScope_SendsNoBody()
    {
        var domainId = Guid.NewGuid();
        _handler.ResponseBody = $$"""{"object":"domain","id":"{{domainId}}"}""";

        await _resend.DomainVerifyAsync( domainId );

        var req = Assert.Single( _handler.Requests );
        Assert.Equal( HttpMethod.Post, req.Method );
        Assert.Equal( $"/domains/{domainId}/verify", req.RequestUri!.PathAndQuery );
        Assert.Null( req.Content );
    }


    /// <summary />
    [Fact]
    public async Task DomainVerify_WithForgeScope_SendsScope()
    {
        var domainId = Guid.NewGuid();
        _handler.ResponseBody = $$"""{"object":"domain","id":"{{domainId}}"}""";

        await _resend.DomainVerifyAsync( domainId, new[] { DomainVerificationScope.Forge } );

        var req = Assert.Single( _handler.Requests );
        Assert.Equal( HttpMethod.Post, req.Method );
        Assert.Equal( $"/domains/{domainId}/verify", req.RequestUri!.PathAndQuery );
        Assert.Equal( """{"scope":["forge"]}""", await req.Content!.ReadAsStringAsync() );
    }


    /// <summary />
    [Fact]
    public async Task DomainForgeEnable_WithCustomReturnPath_SendsIt()
    {
        var domainId = Guid.NewGuid();
        _handler.ResponseBody = DomainJson( domainId );

        await _resend.DomainForgeEnableAsync( domainId, "forge" );

        var req = Assert.Single( _handler.Requests );
        Assert.Equal( HttpMethod.Post, req.Method );
        Assert.Equal( $"/domains/{domainId}/forge", req.RequestUri!.PathAndQuery );
        Assert.Equal( """{"custom_return_path":"forge"}""", await req.Content!.ReadAsStringAsync() );
    }


    /// <summary />
    [Fact]
    public async Task DomainForgeEnable_WithoutCustomReturnPath_SendsEmptyObject()
    {
        var domainId = Guid.NewGuid();
        _handler.ResponseBody = DomainJson( domainId );

        await _resend.DomainForgeEnableAsync( domainId );

        var req = Assert.Single( _handler.Requests );
        Assert.Equal( "{}", await req.Content!.ReadAsStringAsync() );
    }


    /// <summary />
    [Fact]
    public async Task DomainVerify_ForgeNotEnabled_SurfacesTheApiError()
    {
        var domainId = Guid.NewGuid();
        _handler.StatusCode = HttpStatusCode.Conflict;
        _handler.ResponseBody = """{"statusCode":409,"name":"forge_not_enabled","message":"Forge is not enabled on example.com."}""";

        var ex = await Assert.ThrowsAsync<ResendException>( () => _resend.DomainVerifyAsync( domainId, new[] { DomainVerificationScope.Forge } ) );

        Assert.Equal( ErrorType.ForgeNotEnabled, ex.ErrorType );
        Assert.Equal( "Forge is not enabled on example.com.", ex.Message );
    }


    private static string DomainJson( Guid domainId )
    {
        return $$"""
        {
            "object": "domain",
            "id": "{{domainId}}",
            "name": "example.com",
            "status": "pending",
            "created_at": "2026-11-19T10:00:00.000Z",
            "region": "us-east-1",
            "records": []
        }
        """;
    }


    /// <summary />
    private class RecordingHandler : HttpMessageHandler
    {
        /// <summary />
        public List<HttpRequestMessage> Requests { get; } = new();

        /// <summary />
        public string ResponseBody { get; set; } = "{}";

        /// <summary />
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.OK;


        /// <inheritdoc />
        protected override Task<HttpResponseMessage> SendAsync( HttpRequestMessage request, CancellationToken cancellationToken )
        {
            Requests.Add( request );

            var resp = new HttpResponseMessage( StatusCode );
            resp.Content = new StringContent( ResponseBody, Encoding.UTF8, "application/json" );

            return Task.FromResult( resp );
        }
    }
}
