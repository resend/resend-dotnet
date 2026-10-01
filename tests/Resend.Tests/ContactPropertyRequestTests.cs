using System.Net;
using System.Text;
using System.Text.Json;

namespace Resend.Tests;

/// <summary>
/// Regression guard: JIT users can still place arbitrary POCOs into the
/// <c>object</c> properties (serialized via the reflection fallback).
/// </summary>
public class ContactPropertyRequestTests
{
    private readonly RecordingHandler _handler;
    private readonly IResend _resend;


    /// <summary />
    public ContactPropertyRequestTests()
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
    public async Task ContactPropCreate_PocoDefaultValue()
    {
        await _resend.ContactPropCreateAsync( new ContactPropertyData()
        {
            Key = "address",
            PropertyType = ContactPropertyType.String,
            DefaultValue = new CustomValue()
            {
                Street = "Main St",
                ZipCode = 1234,
            },
        } );

        var fallback = Assert.Single( _handler.Bodies ).RootElement.GetProperty( "fallback_value" );

        Assert.Equal( JsonValueKind.Object, fallback.ValueKind );
        Assert.Equal( "Main St", fallback.GetProperty( "street" ).GetString() );
        Assert.Equal( 1234, fallback.GetProperty( "zipCode" ).GetInt32() );
    }


    /// <summary />
    public class CustomValue
    {
        /// <summary />
        public string Street { get; set; } = default!;

        /// <summary />
        public int ZipCode { get; set; }
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
            resp.Content = new StringContent( """{"object":"contact_property","id":"479e3145-dd38-476b-932c-529ceb705947"}""", Encoding.UTF8, "application/json" );

            return resp;
        }
    }
}
