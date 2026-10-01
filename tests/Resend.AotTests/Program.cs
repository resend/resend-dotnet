using FluentEmail.Core;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Resend;
using Resend.FluentEmail;
using Resend.Webhooks;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

/*
 * Native AOT smoke test: exercises ResendClient serialization paths,
 * webhook signing/validation, MapResendWebhook end-to-end and FluentEmail.
 * Prints "AOT smoke OK" and exits 0 on success.
 */
public static class Program
{
    private const string Secret = "whsec_" + "aGVsbG8gd29ybGQgc2lnbmluZyBzZWNyZXQh";

    private static readonly Guid EmailId = Guid.NewGuid();
    private static readonly Guid DomainId = Guid.NewGuid();
    private static readonly Guid BadEmailId = Guid.NewGuid();
    private static readonly Guid ContactPropId = Guid.NewGuid();


    public static async Task<int> Main()
    {
        var failures = 0;

        failures += await Run( "ResendClient", ResendClientChecks );
        failures += await Run( "Webhooks", WebhookChecks );
        failures += await Run( "MapResendWebhook", WebApplicationChecks );
        failures += await Run( "FluentEmail", FluentEmailChecks );

        if ( failures > 0 )
        {
            Console.Error.WriteLine( $"AOT smoke FAILED: {failures} section(s)" );
            return 1;
        }

        Console.WriteLine( "AOT smoke OK" );
        return 0;
    }


    private static async Task<int> Run( string name, Func<Task> check )
    {
        try
        {
            await check();
            return 0;
        }
        catch ( Exception ex )
        {
            Console.Error.WriteLine( $"FAIL {name}: {ex}" );
            return 1;
        }
    }


    private static void Check( bool condition, string message )
    {
        if ( condition == false )
            throw new InvalidOperationException( $"Check failed: {message}" );
    }


    /*
     * ResendClient: request serialization + response/error deserialization.
     */
    private static async Task ResendClientChecks()
    {
        var handler = new StubHandler( Respond );
        var resend = ResendClient.Create(
            new ResendClientOptions { ApiToken = "re_test", ThrowExceptions = false },
            new HttpClient( handler ) );


        /*
         * Email send: request body snake_case keys, response ObjectId.
         */
        var email = new EmailMessage();
        email.From = "from@example.com";
        email.To = "to@example.com";
        email.Subject = "AOT";
        email.HtmlBody = "<b>hi</b>";
        email.Headers = new Dictionary<string, string> { [ "X-Test" ] = "1" };
        email.Tags = new List<EmailTag> { new EmailTag { Name = "t", Value = "v" } };
        email.Attachments = new List<EmailAttachment> { new EmailAttachment { Filename = "a.txt", Content = "hello" } };

        var send = await resend.EmailSendAsync( email );

        Check( send.Success, "EmailSendAsync success" );
        Check( send.Content == EmailId, "EmailSendAsync returns id" );

        var emailBody = handler.LastBody( "POST", "/emails" );
        Check( emailBody.Contains( "\"attachments\"" ), "email body has attachments" );
        Check( emailBody.Contains( "\"tags\"" ), "email body has tags" );
        Check( emailBody.Contains( "\"headers\"" ), "email body has headers" );
        Check( emailBody.Contains( "\"reply_to\"" ) == false, "null props omitted" );


        /*
         * List endpoint: enum wire value + JsonUtcDateTimeConverter.
         */
        var domains = await resend.DomainListAsync();

        Check( domains.Success, "DomainListAsync success" );
        Check( domains.Content!.Count == 1, "DomainListAsync one domain" );
        Check( domains.Content[ 0 ].Id == DomainId, "domain id" );
        Check( domains.Content[ 0 ].Status == DomainStatus.Verified, "domain status enum" );
        Check( domains.Content[ 0 ].Region == DeliveryRegion.EuWest1, "domain region enum" );
        Check( domains.Content[ 0 ].MomentCreated == new DateTime( 2024, 1, 2, 3, 4, 5, DateTimeKind.Utc ), "domain created_at" );


        /*
         * Error response: 422 with Resend error JSON.
         */
        var failed = await resend.EmailRetrieveAsync( BadEmailId );

        Check( failed.Success == false, "error response not success" );
        Check( failed.Exception != null, "error response has exception" );
        Check( failed.Exception!.ErrorType == ErrorType.ValidationError, "error type enum" );


        /*
         * object? property: ContactPropertyData.DefaultValue = 42.
         */
        var prop = await resend.ContactPropCreateAsync( new ContactPropertyData
        {
            Key = "age",
            PropertyType = ContactPropertyType.Number,
            DefaultValue = 42,
        } );

        Check( prop.Success, "ContactPropCreateAsync success" );
        Check( prop.Content == ContactPropId, "ContactPropCreateAsync id" );

        var propBody = handler.LastBody( "POST", "/contact-properties" );
        Check( propBody.Contains( "\"fallback_value\":42" ), "fallback_value serialized as 42" );
        Check( propBody.Contains( "\"type\":\"number\"" ), "property type enum" );
    }


    /*
     * Webhooks: sign, validate, serialize event back.
     */
    private static Task WebhookChecks()
    {
        var payload = """
        {
          "type": "email.sent",
          "created_at": "2024-11-22T23:41:12.126Z",
          "data": {
            "created_at": "2024-11-22T23:41:11.894Z",
            "email_id": "56761188-7520-42d8-8898-ff6fc54ce618",
            "from": "onboarding@resend.dev",
            "to": [ "delivered@resend.dev" ],
            "subject": "AOT smoke"
          }
        }
        """;

        var messageId = "msg_" + Guid.NewGuid().ToString( "N" );
        var ts = DateTimeOffset.UtcNow;
        var signature = new WebhookSigner( Secret ).Sign( messageId, ts, payload );

        var context = new DefaultHttpContext();
        context.Request.Body = new MemoryStream( Encoding.UTF8.GetBytes( payload ) );
        context.Request.Headers[ "svix-id" ] = messageId;
        context.Request.Headers[ "svix-timestamp" ] = ts.ToUnixTimeSeconds().ToString();
        context.Request.Headers[ "svix-signature" ] = signature;

        var validator = new WebhookValidator( Options.Create( new WebhookValidatorOptions { Secret = Secret } ) );
        var result = validator.ValidateAsync( context.Request ).GetAwaiter().GetResult();

        Check( result.IsValid, $"webhook valid ({result.Exception?.ErrorCode} {result.Exception?.Message})" );
        Check( result.Event != null, "webhook event parsed" );
        Check( result.Event!.EventType == WebhookEventType.EmailSent, "event type email.sent" );
        Check( result.Event.DataAs<EmailEventData>().Subject == "AOT smoke", "event data subject" );


        /*
         * Serialize the event back through the converter Write path.
         */
        var json = JsonSerializer.Serialize( result.Event, SmokeJsonContext.Default.WebhookEvent );

        Check( json.Contains( "\"type\":\"email.sent\"" ), "event serialized type" );
        Check( json.Contains( "\"subject\":\"AOT smoke\"" ), "event serialized data" );

        return Task.CompletedTask;
    }


    /*
     * MapResendWebhook end-to-end over Kestrel.
     */
    private static async Task WebApplicationChecks()
    {
        var builder = WebApplication.CreateSlimBuilder();
        builder.Services.AddResendWebhooks( ( o ) => o.Secret = Secret );
        builder.Services.AddSingleton<SmokeHandler>();
        var app = builder.Build();
        app.Urls.Add( "http://127.0.0.1:0" );
        app.MapResendWebhook<SmokeHandler>( "/webhook" );

        await app.StartAsync();

        try
        {
            var server = app.Services.GetRequiredService<IServer>();
            var address = server.Features.Get<IServerAddressesFeature>()!.Addresses.First();

            var payload = """
            {
              "type": "email.delivered",
              "created_at": "2024-11-22T23:41:12.126Z",
              "data": {
                "created_at": "2024-11-22T23:41:11.894Z",
                "email_id": "56761188-7520-42d8-8898-ff6fc54ce618",
                "from": "onboarding@resend.dev",
                "to": [ "delivered@resend.dev" ],
                "subject": "End to end"
              }
            }
            """;

            var messageId = "msg_" + Guid.NewGuid().ToString( "N" );
            var ts = DateTimeOffset.UtcNow;
            var signature = new WebhookSigner( Secret ).Sign( messageId, ts, payload );

            var http = new HttpClient();
            var req = new HttpRequestMessage( HttpMethod.Post, address + "/webhook" );
            req.Content = new StringContent( payload, Encoding.UTF8, "application/json" );
            req.Headers.Add( "svix-id", messageId );
            req.Headers.Add( "svix-timestamp", ts.ToUnixTimeSeconds().ToString() );
            req.Headers.Add( "svix-signature", signature );

            var resp = await http.SendAsync( req );

            Check( resp.StatusCode == HttpStatusCode.OK, $"webhook POST status {resp.StatusCode}" );

            var handler = app.Services.GetRequiredService<SmokeHandler>();
            Check( handler.LastContext != null, "handler invoked" );
            Check( handler.LastContext!.IsValid, $"handler got valid event ({handler.LastContext.Exception?.ErrorCode})" );
            Check( handler.LastContext.Event!.EventType == WebhookEventType.EmailDelivered, "handler event type" );
        }
        finally
        {
            await app.StopAsync();
        }
    }


    /*
     * FluentEmail send over stub handler.
     */
    private static async Task FluentEmailChecks()
    {
        var handler = new StubHandler( Respond );
        var resend = ResendClient.Create(
            new ResendClientOptions { ApiToken = "re_test", ThrowExceptions = false },
            new HttpClient( handler ) );

        var sender = new ResendSender( resend );
        var email = Email
            .From( "from@example.com" )
            .To( "to@example.com" )
            .Subject( "Fluent AOT" )
            .Body( "Html body", true );

        var resp = await sender.SendAsync( email );

        Check( resp.Successful, $"FluentEmail send ({string.Join( ";", resp.ErrorMessages )})" );
        Check( resp.MessageId == EmailId.ToString(), "FluentEmail message id" );
    }


    /*
     * Canned Resend API responses.
     */
    private static (HttpStatusCode, string) Respond( HttpRequestMessage request, string? body )
    {
        var path = request.RequestUri!.AbsolutePath;

        if ( request.Method == HttpMethod.Post && path == "/emails" )
            return (HttpStatusCode.OK, $"{{\"id\":\"{EmailId}\"}}");

        if ( request.Method == HttpMethod.Get && path == "/domains" )
            return (HttpStatusCode.OK, $"{{\"object\":\"list\",\"has_more\":false,\"data\":[{{\"id\":\"{DomainId}\",\"name\":\"example.com\",\"status\":\"verified\",\"created_at\":\"2024-01-02T03:04:05.000Z\",\"region\":\"eu-west-1\"}}]}}");

        if ( request.Method == HttpMethod.Get && path == $"/emails/{BadEmailId}" )
            return (HttpStatusCode.UnprocessableEntity, "{\"statusCode\":422,\"name\":\"validation_error\",\"message\":\"Invalid email id\"}");

        if ( request.Method == HttpMethod.Post && path == "/contact-properties" )
            return (HttpStatusCode.OK, $"{{\"id\":\"{ContactPropId}\"}}");

        return (HttpStatusCode.NotFound, "{\"statusCode\":404,\"name\":\"not_found\",\"message\":\"Not found\"}");
    }
}


/// <summary />
public sealed class SmokeHandler : IWebhookHandler
{
    /// <summary />
    public WebhookContext? LastContext { get; private set; }


    /// <inheritdoc />
    public Task<IResult> HandleValid( WebhookContext context )
    {
        LastContext = context;
        return Task.FromResult( Results.Ok() );
    }


    /// <inheritdoc />
    public Task<IResult> HandleInvalid( WebhookContext context )
    {
        LastContext = context;
        return Task.FromResult( Results.BadRequest() );
    }
}


/// <summary />
internal sealed class StubHandler : HttpMessageHandler
{
    private readonly Func<HttpRequestMessage, string?, (HttpStatusCode, string)> _responder;
    private readonly List<(string Method, string Path, string? Body)> _requests = new();


    /// <summary />
    public StubHandler( Func<HttpRequestMessage, string?, (HttpStatusCode, string)> responder )
    {
        _responder = responder;
    }


    /// <summary />
    public string LastBody( string method, string path )
    {
        return _requests.Last( ( x ) => x.Method == method && x.Path == path ).Body!;
    }


    /// <inheritdoc />
    protected override async Task<HttpResponseMessage> SendAsync( HttpRequestMessage request, CancellationToken cancellationToken )
    {
        var body = request.Content == null ? null : await request.Content.ReadAsStringAsync( cancellationToken );

        _requests.Add( (request.Method.Method, request.RequestUri!.AbsolutePath, body) );

        var (status, json) = _responder( request, body );

        return new HttpResponseMessage( status )
        {
            Content = new StringContent( json, Encoding.UTF8, "application/json" ),
        };
    }
}


/// <summary />
[JsonSerializable( typeof( WebhookEvent ) )]
[JsonSerializable( typeof( EmailEventData ) )]
[JsonSerializable( typeof( ContactEventData ) )]
[JsonSerializable( typeof( DomainEventData ) )]
[JsonSerializable( typeof( SuppressionEventData ) )]
[JsonSerializable( typeof( ContactTopicsEventData ) )]
[JsonSerializable( typeof( TopicEventData ) )]
internal partial class SmokeJsonContext : JsonSerializerContext
{
}
