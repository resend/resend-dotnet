using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;

namespace Resend.Tests;

/// <summary>
/// Regression guard: under JIT, values in <c>object</c>-typed properties keep
/// serializing exactly like default <see cref="JsonSerializer"/> (Web defaults).
/// </summary>
public class ObjectValueSerializationTests
{
    private static readonly JsonSerializerOptions Web = new( JsonSerializerDefaults.Web );

    private readonly RecordingHandler _handler;
    private readonly IResend _resend;


    /// <summary />
    public ObjectValueSerializationTests()
    {
        _handler = new RecordingHandler();

        var opt = new ResendClientOptions()
        {
            ApiToken = "re_test_123",
        };

        _resend = ResendClient.Create( opt, new HttpClient( _handler ) );
    }


    /// <summary />
    public static IEnumerable<object?[]> Values()
    {
        yield return new object?[] { (uint) 42 };
        yield return new object?[] { TextEnum.Beta };
        yield return new object?[] { PlainEnum.Two };
        yield return new object?[] { new CustomValue() { Street = "Main St", ZipCode = 1234 } };
        yield return new object?[] { new List<CustomValue>() { new CustomValue() { Street = "A", ZipCode = 1 } } };
        yield return new object?[] { new byte[] { 1, 2, 3 } };
        yield return new object?[] { new object() };
        yield return new object?[] { JsonDocument.Parse( "{\"a\":1}" ).RootElement };
        yield return new object?[] { new JsonObject { [ "a" ] = 1 } };
        yield return new object?[] { new Dictionary<string, string> { [ "k" ] = "v" } };
        yield return new object?[] { new DateOnly( 2026, 10, 1 ) };
        yield return new object?[] { TimeSpan.FromMinutes( 90 ) };
    }


    /// <summary />
    [Theory]
    [MemberData( nameof( Values ) )]
    public async Task ContactPropCreate_ObjectValue_MatchesDefaultSerializer( object? value )
    {
        await _resend.ContactPropCreateAsync( new ContactPropertyData()
        {
            Key = "k",
            PropertyType = ContactPropertyType.String,
            DefaultValue = value,
        } );

        var fallback = _handler.Bodies.Last().RootElement.GetProperty( "fallback_value" );

        var expected = JsonDocument.Parse( JsonSerializer.Serialize( value, Web ) ).RootElement;

        Assert.Equal( expected.GetRawText(), fallback.GetRawText() );
    }


    /// <summary />
    [Fact]
    public async Task ContactPropRetrieve_ObjectValue_ReadsJsonElement()
    {
        var resp = await _resend.ContactPropRetrieveAsync( Guid.NewGuid() );

        Assert.True( resp.Success );
        Assert.NotNull( resp.Content );
        Assert.IsType<JsonElement>( resp.Content.DefaultValue );
        Assert.Equal( 42, ( (JsonElement) resp.Content.DefaultValue! ).GetInt32() );
    }


    /// <summary />
    [JsonConverter( typeof( JsonStringEnumConverter ) )]
    public enum TextEnum
    {
        /// <summary />
        Beta,
    }


    /// <summary />
    public enum PlainEnum
    {
        /// <summary />
        Two = 2,
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
            if ( request.Content != null )
            {
                var json = await request.Content.ReadAsStringAsync( cancellationToken );
                Bodies.Add( JsonDocument.Parse( json ) );
            }

            var resp = new HttpResponseMessage( HttpStatusCode.OK );
            resp.Content = new StringContent( """{"object":"contact_property","id":"479e3145-dd38-476b-932c-529ceb705947","key":"k","type":"number","fallback_value":42,"created_at":"2024-01-02T03:04:05.000Z"}""", Encoding.UTF8, "application/json" );

            return resp;
        }
    }
}
