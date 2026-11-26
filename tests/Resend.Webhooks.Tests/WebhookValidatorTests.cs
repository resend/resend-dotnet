using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Resend.Webhooks.Tests;

/// <summary />
public class WebhookValidatorTests
{
    private const string Secret = "whsec_MfKQ9r8GKYqrTwjUPD8ILPZIo2LaLaSw";
    private const string MessageId = "msg_2KWPBgLlAfxdpx2AI54pPJ85f4W";
    private const string Payload = """
    {
      "type": "email.sent",
      "created_at": "2026-07-09T18:23:37.867Z",
      "data": {
        "created_at": "2026-07-09T18:23:37.236Z",
        "email_id": "c4bd0130-17f1-4e49-90bc-856308df9bb5",
        "from": "Acme <onboarding@resend.dev>",
        "subject": "Test",
        "to": [ "to@example.com" ]
      }
    }
    """;


    /// <summary />
    [Fact]
    public void ValidatesRequestWithSvixHeaders()
    {
        var request = SignedRequest();

        var result = Validator().Validate( request, Payload );

        Assert.True( result.IsValid );
        Assert.Null( result.Exception );
        Assert.Equal( MessageId, result.MessageId );
        Assert.Equal( WebhookEventType.EmailSent, result.Event?.EventType );
    }


    /// <summary />
    [Fact]
    public void RejectsTamperedPayload()
    {
        var request = SignedRequest();

        var result = Validator().Validate( request, Payload.Replace( "Test", "Tampered" ) );

        Assert.False( result.IsValid );
        Assert.Equal( "RWH202", result.Exception?.ErrorCode );
    }


    /// <summary />
    [Theory]
    [InlineData( "svix-id", "RWH101" )]
    [InlineData( "svix-timestamp", "RWH102" )]
    [InlineData( "svix-signature", "RWH103" )]
    public void RejectsMissingHeader( string header, string code )
    {
        var request = SignedRequest();
        request.Headers.Remove( header );

        var result = Validator().Validate( request, Payload );

        Assert.False( result.IsValid );
        Assert.Equal( code, result.Exception?.ErrorCode );
    }


    private static WebhookValidator Validator()
    {
        return new WebhookValidator( Options.Create( new WebhookValidatorOptions { Secret = Secret } ) );
    }


    private static HttpRequest SignedRequest()
    {
        var timestamp = DateTimeOffset.UtcNow;
        var signature = new WebhookSigner( Secret ).Sign( MessageId, timestamp, Payload );

        var request = new DefaultHttpContext().Request;
        request.Headers[ "svix-id" ] = MessageId;
        request.Headers[ "svix-timestamp" ] = timestamp.ToUnixTimeSeconds().ToString();
        request.Headers[ "svix-signature" ] = signature;

        return request;
    }
}
