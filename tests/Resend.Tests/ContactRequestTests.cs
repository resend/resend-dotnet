using System.Net;
using System.Text;
using System.Text.Json;

namespace Resend.Tests;

/// <summary>
/// Tests over the requests issued by the contact methods, against a handler which records
/// instead of answering like the API.
/// </summary>
public class ContactRequestTests
{
    private readonly RecordingHandler _handler;
    private readonly IResend _resend;


    /// <summary />
    public ContactRequestTests()
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
    public async Task ContactAdd_Properties()
    {
        await _resend.ContactAddAsync( new ContactData()
        {
            Email = "test@example.com",
            Properties = new()
            {
                { "tier", "premium" },
                { "age", 42 },
                { "score", 9.5 },
                { "nickname", null },
            },
        } );

        var props = Assert.Single( _handler.Bodies ).RootElement.GetProperty( "properties" );

        Assert.Equal( JsonValueKind.String, props.GetProperty( "tier" ).ValueKind );
        Assert.Equal( "premium", props.GetProperty( "tier" ).GetString() );
        Assert.Equal( JsonValueKind.Number, props.GetProperty( "age" ).ValueKind );
        Assert.Equal( 42, props.GetProperty( "age" ).GetInt32() );
        Assert.Equal( 9.5, props.GetProperty( "score" ).GetDouble() );
        Assert.Equal( JsonValueKind.Null, props.GetProperty( "nickname" ).ValueKind );
    }


    /// <summary />
    [Fact]
    public async Task ContactUpdate_Properties()
    {
        await _resend.ContactUpdateAsync( Guid.NewGuid(), new ContactData()
        {
            Properties = new()
            {
                { "tier", "premium" },
                { "age", 42 },
                { "vip", true },
                { "nickname", null },
            },
        } );

        var props = Assert.Single( _handler.Bodies ).RootElement.GetProperty( "properties" );

        Assert.Equal( "premium", props.GetProperty( "tier" ).GetString() );
        Assert.Equal( 42, props.GetProperty( "age" ).GetInt32() );
        Assert.Equal( JsonValueKind.True, props.GetProperty( "vip" ).ValueKind );
        Assert.Equal( JsonValueKind.Null, props.GetProperty( "nickname" ).ValueKind );
    }


    /// <summary />
    [Fact]
    public async Task ContactUpdateByEmail_Properties()
    {
        await _resend.ContactUpdateByEmailAsync( "test@example.com", new ContactData()
        {
            Properties = new Dictionary<string, object?>()
            {
                { "vip", false },
                { "nickname", null },
            },
        } );

        var props = Assert.Single( _handler.Bodies ).RootElement.GetProperty( "properties" );

        Assert.Equal( JsonValueKind.False, props.GetProperty( "vip" ).ValueKind );
        Assert.Equal( JsonValueKind.Null, props.GetProperty( "nickname" ).ValueKind );
    }


    /// <summary />
    [Fact]
    public async Task ContactUpdate_NoProperties_Omitted()
    {
        await _resend.ContactUpdateAsync( Guid.NewGuid(), new ContactData()
        {
            FirstName = "Carl",
        } );

        var body = Assert.Single( _handler.Bodies ).RootElement;

        Assert.False( body.TryGetProperty( "properties", out _ ) );
    }


    /// <summary />
    private class RecordingHandler : HttpMessageHandler
    {
        /// <summary />
        public List<JsonDocument> Bodies { get; } = new();


        /// <inheritdoc />
        protected override async Task<HttpResponseMessage> SendAsync( HttpRequestMessage request, CancellationToken cancellationToken )
        {
            var json = await request.Content!.ReadAsStringAsync( cancellationToken );
            Bodies.Add( JsonDocument.Parse( json ) );

            var resp = new HttpResponseMessage( HttpStatusCode.OK );
            resp.Content = new StringContent( """{"object":"contact","id":"479e3145-dd38-476b-932c-529ceb705947"}""", Encoding.UTF8, "application/json" );

            return resp;
        }
    }
}
