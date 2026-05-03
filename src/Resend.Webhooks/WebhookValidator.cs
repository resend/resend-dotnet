using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Svix.Exceptions;
using System.Net;
using System.Text;
using System.Text.Json;

namespace Resend.Webhooks;

/// <summary />
public class WebhookValidator
{
    private readonly WebhookValidatorOptions _options;


    /// <summary />
    public WebhookValidator( IOptions<WebhookValidatorOptions> options )
    {
        _options = options.Value;
    }


    /// <summary />
    public async Task<WebhookContext> ValidateAsync( HttpRequest request, CancellationToken cancellationToken = default )
    {
        /*
         * 
         */
        var s = new WebhookContext();
        s.IsValid = false;

        var bodyStream = request.Body;

        using ( var reader = new StreamReader( bodyStream, Encoding.UTF8, detectEncodingFromByteOrderMarks: false, leaveOpen: false ) )
        {
            //try
            //{
            //    bodyStream.Position = 0;
            //}
            //catch
            //{
            //    s.Exception = new WebhookException( "RWH001", "Unable to reset position, is EnableBuffering on?" );

            //    return s;
            //}

            try
            {
                s.Payload = await reader.ReadToEndAsync( cancellationToken );
            }
            catch ( Exception e ) when ( e is not OperationCanceledException )
            {
                s.Exception = new WebhookException( "RWH002", "Unable to read raw body" );

                return s;
            }
        }


        return Validate( request, s.Payload );
    }


    /// <summary />
    public WebhookContext Validate( HttpRequest request, string payload )
    {
        var s = new WebhookContext();
        s.IsValid = false;


        /*
         * 
         */
        if ( request.Headers.ContainsKey( "svix-id" ) == false )
        {
            s.Exception = new WebhookException( "RWH101", "Missing required header 'svix-id'" );

            return s;
        }

        if ( request.Headers.ContainsKey( "svix-timestamp" ) == false )
        {
            s.Exception = new WebhookException( "RWH102", "Missing required header 'svix-timestamp'" );

            return s;
        }

        if ( request.Headers.ContainsKey( "svix-signature" ) == false )
        {
            s.Exception = new WebhookException( "RWH103", "Missing required header 'svix-signature'" );

            return s;
        }

        if ( long.TryParse( request.Headers[ "svix-timestamp" ].ToString(), out var ts ) == false )
        {
            s.Exception = new WebhookException( "RWH104", "Invalid value 'svix-timestamp', expected long" );

            return s;
        }

        s.MessageId = request.Headers[ "svix-id" ].ToString();
        s.Timestamp = ts;
        s.Signature = request.Headers[ "svix-signature" ].ToString();
        s.Payload = payload;


        /*
         * 
         */
        var h = new WebHeaderCollection
        {
            { "svix-id", s.MessageId },
            { "svix-timestamp", s.Timestamp.ToString() },
            { "svix-signature", s.Signature }
        };


        /*
         * 
         */
        Svix.Webhook wh;

        try
        {
            wh = new Svix.Webhook( _options.Secret );
        }
        catch
        {
            s.Exception = new WebhookException( "RWH201", "Invalid signing secret" );

            return s;
        }

        try
        {
            wh.Verify( payload, h );
        }
        catch ( WebhookVerificationException )
        {
            s.Exception = new WebhookException( "RWH202", "Signature validation failed" );

            return s;
        }
        catch
        {
            s.Exception = new WebhookException( "RWH203", "Unhandled exception verifying payload" );

            return s;
        }


        /*
         * 
         */
        s.Event = JsonSerializer.Deserialize<WebhookEvent>( s.Payload );


        /*
         *
         */
        s.IsValid = true;

        return s;
    }
}
