using Microsoft.Extensions.Options;
using Resend.Payloads;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Reflection;

namespace Resend;

/// <summary>
/// Resend client implementation.
/// </summary>
public class ResendClient : IResend
{
    private readonly bool _throw;
    private readonly HttpClient _http;

    /// <summary>
    /// HTTP header name, for idempotency key.
    /// </summary>
    private const string IdempotencyKey = "Idempotency-Key";


    /// <summary>
    /// Initializes a new instance of ResendClient client.
    /// </summary>
    /// <param name="options">
    /// Configuration options.
    /// </param>
    /// <param name="httpClient">
    /// HTTP client instance.
    /// </param>
    public ResendClient( IOptionsSnapshot<ResendClientOptions> options, HttpClient httpClient )
    {
        /*
         * Authentication
         */
        var opt = options.Value;

        httpClient.BaseAddress = new Uri( opt.ApiUrl );
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue( "Bearer", opt.ApiToken );


        /*
         * Ask for JSON responses. Not necessary atm, since Resend always/only
         * answers in JSON -- but good for future proofing.
         */
        httpClient.DefaultRequestHeaders.Accept.Add( new MediaTypeWithQualityHeaderValue( "application/json" ) );


        /*
         * Identification
         */
        var productValue = new ProductInfoHeaderValue( "resend-sdk", Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "0.0.0" );
        var dotnetValue = new ProductInfoHeaderValue( "dotnet", Environment.Version.ToString() );

        httpClient.DefaultRequestHeaders.UserAgent.Add( productValue );
        httpClient.DefaultRequestHeaders.UserAgent.Add( dotnetValue );

        _http = httpClient;
        _throw = options.Value.ThrowExceptions;
    }


    /// <inheritdoc />
    public async Task<ResendResponse<Guid>> EmailSendAsync( EmailMessage email, CancellationToken cancellationToken = default )
    {
        var req = new HttpRequestMessage( HttpMethod.Post, "/emails" );
        req.Content = JsonContent.Create( email );

        return await Execute<ObjectId, Guid>( req, ( x ) => x.Id, cancellationToken );
    }


    /// <inheritdoc />
    public Task<ResendResponse<Guid>> EmailSendAsync( string idempotencyKey, EmailMessage email, CancellationToken cancellationToken = default )
    {
        var req = new HttpRequestMessage( HttpMethod.Post, "/emails" );
        req.Content = JsonContent.Create( email );
        req.Headers.Add( IdempotencyKey, idempotencyKey );

        return Execute<ObjectId, Guid>( req, ( x ) => x.Id, cancellationToken );
    }


    /// <inheritdoc />
    public async Task<ResendResponse<EmailReceipt>> EmailRetrieveAsync( Guid emailId, CancellationToken cancellationToken = default )
    {
        var path = $"/emails/{emailId}";
        var req = new HttpRequestMessage( HttpMethod.Get, path );

        return await Execute<EmailReceipt, EmailReceipt>( req, ( x ) => x, cancellationToken );
    }


    /// <inheritdoc />
    public async Task<ResendResponse<List<Guid>>> EmailBatchAsync( IEnumerable<EmailMessage> emails, CancellationToken cancellationToken = default )
    {
        var path = $"/emails/batch";
        var req = new HttpRequestMessage( HttpMethod.Post, path );
        req.Content = JsonContent.Create( emails );

        return await Execute<ListOf<ObjectId>, List<Guid>>( req, ( x ) => x.Data.Select( y => y.Id ).ToList(), cancellationToken );
    }


    /// <inheritdoc />
    public Task<ResendResponse<List<Guid>>> EmailBatchAsync( string idempotencyKey, IEnumerable<EmailMessage> emails, CancellationToken cancellationToken = default )
    {
        var path = $"/emails/batch";
        var req = new HttpRequestMessage( HttpMethod.Post, path );
        req.Content = JsonContent.Create( emails );
        req.Headers.Add( IdempotencyKey, idempotencyKey );

        return Execute<ListOf<ObjectId>, List<Guid>>( req, ( x ) => x.Data.Select( y => y.Id ).ToList(), cancellationToken );
    }


    /// <inheritdoc />
    public async Task<ResendResponse> EmailRescheduleAsync( Guid emailId, DateTimeOrHuman rescheduleFor, CancellationToken cancellationToken = default )
    {
        var path = $"/emails/{emailId}";
        var req = new HttpRequestMessage( HttpMethod.Patch, path );
        req.Content = JsonContent.Create( new EmailRescheduleRequest()
        {
            MomentSchedule = rescheduleFor,
        } );

        return await Execute( req, cancellationToken );
    }


    /// <inheritdoc />
    public async Task<ResendResponse> EmailCancelAsync( Guid emailId, CancellationToken cancellationToken = default )
    {
        var path = $"emails/{emailId}/cancel";
        var req = new HttpRequestMessage( HttpMethod.Post, path );

        return await Execute( req, cancellationToken );
    }


    /// <inheritdoc />
    public async Task<ResendResponse<Domain>> DomainAddAsync( string domainName, DeliveryRegion? region = null, CancellationToken cancellationToken = default )
    {
        var path = $"/domains";
        var req = new HttpRequestMessage( HttpMethod.Post, path );
        req.Content = JsonContent.Create( new DomainAddData()
        {
            DomainName = domainName,
            Region = region,
        } );

        return await Execute<Domain, Domain>( req, ( x ) => x, cancellationToken );
    }


    /// <inheritdoc />
    public Task<ResendResponse<Domain>> DomainAddAsync( DomainAddData data, CancellationToken cancellationToken = default )
    {
        var path = $"/domains";
        var req = new HttpRequestMessage( HttpMethod.Post, path );
        req.Content = JsonContent.Create( data );

        return Execute<Domain, Domain>( req, ( x ) => x, cancellationToken );
    }


    /// <inheritdoc />
    public async Task<ResendResponse<Domain>> DomainRetrieveAsync( Guid domainId, CancellationToken cancellationToken = default )
    {
        var path = $"/domains/{domainId}";
        var req = new HttpRequestMessage( HttpMethod.Get, path );

        return await Execute<Domain, Domain>( req, ( x ) => x, cancellationToken );
    }


    /// <inheritdoc />
    public async Task<ResendResponse> DomainUpdateAsync( Guid domainId, DomainUpdateData data, CancellationToken cancellationToken = default )
    {
        var path = $"/domains/{domainId}";
        var req = new HttpRequestMessage( HttpMethod.Patch, path );
        req.Content = JsonContent.Create( data );

        return await Execute( req, cancellationToken );
    }


    /// <inheritdoc />
    public async Task<ResendResponse> DomainVerifyAsync( Guid domainId, CancellationToken cancellationToken = default )
    {
        var path = $"/domains/{domainId}/verify";
        var req = new HttpRequestMessage( HttpMethod.Post, path );

        return await Execute( req, cancellationToken );
    }


    /// <inheritdoc />
    public async Task<ResendResponse<List<Domain>>> DomainListAsync( CancellationToken cancellationToken = default )
    {
        var path = $"/domains";
        var req = new HttpRequestMessage( HttpMethod.Get, path );

        return await Execute<ListOf<Domain>, List<Domain>>( req, ( x ) => x.Data, cancellationToken );
    }


    /// <inheritdoc />
    public async Task<ResendResponse> DomainDeleteAsync( Guid domainId, CancellationToken cancellationToken = default )
    {
        var path = $"/domains/{domainId}";
        var req = new HttpRequestMessage( HttpMethod.Delete, path );

        return await Execute( req, cancellationToken );
    }


    /// <inheritdoc />
    public async Task<ResendResponse<ApiKeyData>> ApiKeyCreateAsync( string keyName, Permission? permission = null, Guid? domainId = null, CancellationToken cancellationToken = default )
    {
        var path = $"/api-keys";
        var req = new HttpRequestMessage( HttpMethod.Post, path );
        req.Content = JsonContent.Create( new ApiKeyCreateRequest()
        {
            Name = keyName,
            Permission = permission,
            DomainId = domainId,
        } );

        return await Execute<ApiKeyData, ApiKeyData>( req, ( x ) => x, cancellationToken );
    }


    /// <inheritdoc />
    public async Task<ResendResponse<List<ApiKey>>> ApiKeyListAsync( CancellationToken cancellationToken = default )
    {
        var path = $"/api-keys";
        var req = new HttpRequestMessage( HttpMethod.Get, path );

        return await Execute<ListOf<ApiKey>, List<ApiKey>>( req, ( x ) => x.Data, cancellationToken );
    }


    /// <inheritdoc />
    public async Task<ResendResponse> ApiKeyDeleteAsync( Guid apiKeyId, CancellationToken cancellationToken = default )
    {
        var path = $"/api-keys/{apiKeyId}";
        var req = new HttpRequestMessage( HttpMethod.Delete, path );

        return await Execute( req, cancellationToken );
    }


    /// <inheritdoc />
    public async Task<ResendResponse<List<Webhook>>> WebhookListAsync( CancellationToken cancellationToken = default )
    {
        var path = $"/webhooks";
        var req = new HttpRequestMessage( HttpMethod.Get, path );

        return await Execute<ListOf<Webhook>, List<Webhook>>( req, ( x ) => x.Data, cancellationToken );
    }


    /// <inheritdoc/>
    public async Task<ResendResponse<Guid>> AudienceAddAsync( string name, CancellationToken cancellationToken = default )
    {
        var path = $"/audiences";
        var req = new HttpRequestMessage( HttpMethod.Post, path );
        req.Content = JsonContent.Create( new AudienceAddRequest()
        {
            Name = name
        } );

        return await Execute<ObjectId, Guid>( req, ( x ) => x.Id, cancellationToken );
    }


    /// <inheritdoc/>
    public async Task<ResendResponse<Audience>> AudienceRetrieveAsync( Guid audienceId, CancellationToken cancellationToken = default )
    {
        var path = $"/audiences/{audienceId}";
        var req = new HttpRequestMessage( HttpMethod.Get, path );

        return await Execute<Audience, Audience>( req, ( x ) => x, cancellationToken );
    }


    /// <inheritdoc/>
    public async Task<ResendResponse> AudienceDeleteAsync( Guid audienceId, CancellationToken cancellationToken = default )
    {
        var path = $"/audiences/{audienceId}";
        var req = new HttpRequestMessage( HttpMethod.Delete, path );

        return await Execute( req, cancellationToken );
    }


    /// <inheritdoc/>
    public async Task<ResendResponse<List<Audience>>> AudienceListAsync( CancellationToken cancellationToken = default )
    {
        var path = $"/audiences";
        var req = new HttpRequestMessage( HttpMethod.Get, path );

        return await Execute<ListOf<Audience>, List<Audience>>( req, ( x ) => x.Data, cancellationToken );
    }


    /// <inheritdoc/>
    public async Task<ResendResponse<Guid>> ContactAddAsync( Guid audienceId, ContactData data, CancellationToken cancellationToken = default )
    {
        if ( data.Email == null )
            throw new ArgumentException( "Email must be non-null when creating contact", nameof( data ) + ".Email" );

        var path = $"/audiences/{audienceId}/contacts";
        var req = new HttpRequestMessage( HttpMethod.Post, path );
        req.Content = JsonContent.Create( data );

        return await Execute<ObjectId, Guid>( req, ( x ) => x.Id, cancellationToken );
    }


    /// <inheritdoc/>
    public async Task<ResendResponse<Contact>> ContactRetrieveAsync( Guid audienceId, Guid contactId, CancellationToken cancellationToken = default )
    {
        var path = $"/audiences/{audienceId}/contacts/{contactId}";
        var req = new HttpRequestMessage( HttpMethod.Get, path );

        return await Execute<Contact, Contact>( req, ( x ) => x, cancellationToken );
    }


    /// <inheritdoc/>
    public Task<ResendResponse<Contact>> ContactRetrieveByEmailAsync( Guid audienceId, string email, CancellationToken cancellationToken = default )
    {
        var path = $"/audiences/{audienceId}/contacts/{email}";
        var req = new HttpRequestMessage( HttpMethod.Get, path );

        return Execute<Contact, Contact>( req, ( x ) => x, cancellationToken );
    }


    /// <inheritdoc/>
    public async Task<ResendResponse> ContactUpdateAsync( Guid audienceId, Guid contactId, ContactData data, CancellationToken cancellationToken = default )
    {
        var path = $"/audiences/{audienceId}/contacts/{contactId}";
        var req = new HttpRequestMessage( HttpMethod.Patch, path );
        req.Content = JsonContent.Create( data );

        return await Execute( req, cancellationToken );
    }


    /// <inheritdoc/>
    public async Task<ResendResponse> ContactUpdateByEmailAsync( Guid audienceId, string email, ContactData data, CancellationToken cancellationToken = default )
    {
        var path = $"/audiences/{audienceId}/contacts/{email}";
        var req = new HttpRequestMessage( HttpMethod.Patch, path );
        req.Content = JsonContent.Create( data );

        return await Execute( req, cancellationToken );
    }


    /// <inheritdoc/>
    public async Task<ResendResponse> ContactDeleteAsync( Guid audienceId, Guid contactId, CancellationToken cancellationToken = default )
    {
        var path = $"/audiences/{audienceId}/contacts/{contactId}";
        var req = new HttpRequestMessage( HttpMethod.Delete, path );

        return await Execute( req, cancellationToken );
    }


    /// <inheritdoc/>
    public async Task<ResendResponse> ContactDeleteByEmailAsync( Guid audienceId, string email, CancellationToken cancellationToken = default )
    {
        var path = $"/audiences/{audienceId}/contacts/{email}";
        var req = new HttpRequestMessage( HttpMethod.Delete, path );

        return await Execute( req, cancellationToken );
    }


    /// <inheritdoc/>
    public async Task<ResendResponse<List<Contact>>> ContactListAsync( Guid audienceId, CancellationToken cancellationToken = default )
    {
        var path = $"/audiences/{audienceId}/contacts";
        var req = new HttpRequestMessage( HttpMethod.Get, path );

        return await Execute<ListOf<Contact>, List<Contact>>( req, ( x ) => x.Data, cancellationToken );
    }


    /// <inheritdoc/>
    public async Task<ResendResponse<Guid>> BroadcastAddAsync( BroadcastData data, CancellationToken cancellationToken = default )
    {
        var path = $"/broadcasts";
        var req = new HttpRequestMessage( HttpMethod.Post, path );
        req.Content = JsonContent.Create( data );

        return await Execute<ObjectId, Guid>( req, ( x ) => x.Id, cancellationToken );
    }


    /// <inheritdoc/>
    public async Task<ResendResponse<Broadcast>> BroadcastRetrieveAsync( Guid broadcastId, CancellationToken cancellationToken = default )
    {
        var path = $"/broadcasts/{broadcastId}";
        var req = new HttpRequestMessage( HttpMethod.Get, path );

        return await Execute<Broadcast, Broadcast>( req, ( x ) => x, cancellationToken );
    }


    /// <inheritdoc/>
    public async Task<ResendResponse> BroadcastUpdateAsync( Guid broadcastId, BroadcastUpdateData data, CancellationToken cancellationToken = default )
    {
        var path = $"/broadcasts/{broadcastId}";
        var req = new HttpRequestMessage( HttpMethod.Patch, path );
        req.Content = JsonContent.Create( data );

        return await Execute( req, cancellationToken );
    }


    /// <inheritdoc/>
    public async Task<ResendResponse> BroadcastSendAsync( Guid broadcastId, CancellationToken cancellationToken = default )
    {
        var path = $"/broadcasts/{broadcastId}/send";
        var req = new HttpRequestMessage( HttpMethod.Post, path );
        req.Content = JsonContent.Create( new BroadcastScheduleRequest()
        {
        } );

        return await Execute( req, cancellationToken );
    }


    /// <inheritdoc/>
    public async Task<ResendResponse> BroadcastScheduleAsync( Guid broadcastId, DateTime scheduleFor, CancellationToken cancellationToken = default )
    {
        var path = $"/broadcasts/{broadcastId}/send";
        var req = new HttpRequestMessage( HttpMethod.Post, path );
        req.Content = JsonContent.Create( new BroadcastScheduleRequest()
        {
            MomentSchedule = scheduleFor,
        } );

        return await Execute( req, cancellationToken );
    }


    /// <inheritdoc/>
    public async Task<ResendResponse<List<Broadcast>>> BroadcastListAsync( CancellationToken cancellationToken = default )
    {
        var path = $"/broadcasts";
        var req = new HttpRequestMessage( HttpMethod.Get, path );

        return await Execute<ListOf<Broadcast>, List<Broadcast>>( req, ( x ) => x.Data, cancellationToken );
    }


    /// <inheritdoc/>
    public async Task<ResendResponse> BroadcastDeleteAsync( Guid broadcastId, CancellationToken cancellationToken = default )
    {
        var path = $"/broadcasts/{broadcastId}";
        var req = new HttpRequestMessage( HttpMethod.Delete, path );

        return await Execute( req, cancellationToken );
    }


    /// <summary />
    private async Task<ResendResponse> Execute( HttpRequestMessage req, CancellationToken cancellationToken )
    {
        /*
         *
         */
        HttpResponseMessage resp;

        try
        {
            resp = await _http.SendAsync( req, HttpCompletionOption.ResponseContentRead, cancellationToken );
        }
        catch ( TaskCanceledException )
        {
            throw;
        }
        catch ( Exception ex )
        {
            ResendException oex = new ResendException( null, ErrorType.HttpSendFailed, ex.Message, ex );

            if ( _throw == true )
                throw oex;

            return new ResendResponse( oex, null );
        }


        /*
         *
         */
        var rrl = FromHeaders( resp.Headers );


        /*
         * Following block is the same as Habdle<T1, T2>, but the response isn't <T2>.
         */
        if ( resp.IsSuccessStatusCode == false )
        {
            ErrorResponse? err;

            try
            {
                err = await resp.Content.ReadFromJsonAsync<ErrorResponse>( cancellationToken );
            }
            catch ( Exception iex )
            {
                ResendException oex = new ResendException( resp.StatusCode, ErrorType.Deserialization, "Failed deserializing error response", iex );

                if ( _throw == true )
                    throw oex;

                return new ResendResponse( oex, rrl );
            }

            ResendException ex;

            if ( err == null )
                ex = new ResendException( resp.StatusCode, ErrorType.MissingResponse, "Missing error response" );
            else if ( err.ErrorType == ErrorType.ValidationError )
            {
                ex = new ResendValidationErrorException( HttpStatusCode.BadRequest, err.Message );
            }
            else if ( err.ErrorType == ErrorType.MissingApiKey )
            {
                ex = new ResendMissingApiKeyException( HttpStatusCode.Unauthorized, err.Message );
            }
            else if ( err.ErrorType == ErrorType.RestrictedApiKey )
            {
                ex = new ResendException( HttpStatusCode.Forbidden, err.ErrorType, err.Message );
            }
            else if ( err.ErrorType == ErrorType.NotFound )
            {
                ex = new ResendException( HttpStatusCode.NotFound, err.ErrorType, err.Message );
            }
            else if ( err.ErrorType == ErrorType.DailyQuotaExceeded )
            {
                ex = new ResendDailyQuotaExceededException( HttpStatusCode.TooManyRequests, err.Message, rrl );
            }
            else if ( err.ErrorType == ErrorType.RateLimitExceeded )
            {
                ex = new ResendRateLimitExceededException( HttpStatusCode.TooManyRequests, err.Message, rrl );
            }
            else
                ex = new ResendException( (HttpStatusCode) err.StatusCode, err.ErrorType, err.Message );

            if ( _throw == true )
                throw ex;

            return new ResendResponse( ex, rrl );
        }


        /*
         *
         */
        return new ResendResponse( rrl );
    }


    /// <summary />
    private async Task<ResendResponse<T2>> Execute<T1, T2>( HttpRequestMessage req,
        Func<T1, T2> map,
        CancellationToken cancellationToken )
    {
        /*
         *
         */
        HttpResponseMessage resp;

        try
        {
            resp = await _http.SendAsync( req, HttpCompletionOption.ResponseContentRead, cancellationToken );
        }
        catch ( TaskCanceledException )
        {
            throw;
        }
        catch ( Exception ex )
        {
            ResendException oex = new ResendException( null, ErrorType.HttpSendFailed, ex.Message, ex );

            if ( _throw == true )
                throw oex;

            return new ResendResponse<T2>( oex, null );
        }


        /*
         *
         */
        var rrl = FromHeaders( resp.Headers );


        /*
         *
         */
        if ( resp.IsSuccessStatusCode == false )
        {
            ErrorResponse? err;

            try
            {
                err = await resp.Content.ReadFromJsonAsync<ErrorResponse>( cancellationToken );
            }
            catch ( TaskCanceledException )
            {
                throw;
            }
            catch ( Exception iex )
            {
                ResendException oex = new ResendException( resp.StatusCode, ErrorType.Deserialization, "Failed deserializing error response", iex );

                if ( _throw == true )
                    throw oex;

                return new ResendResponse<T2>( oex, rrl );
            }

            ResendException ex;

            if ( err == null )
                ex = new ResendException( resp.StatusCode, ErrorType.MissingResponse, "Missing error response" );
            else if ( err.ErrorType == ErrorType.ValidationError )
            {
                ex = new ResendValidationErrorException( HttpStatusCode.BadRequest, err.Message );
            }
            else if ( err.ErrorType == ErrorType.MissingApiKey )
            {
                ex = new ResendMissingApiKeyException( HttpStatusCode.Unauthorized, err.Message );
            }
            else if ( err.ErrorType == ErrorType.RestrictedApiKey )
            {
                ex = new ResendException( HttpStatusCode.Forbidden, err.ErrorType, err.Message );
            }
            else if ( err.ErrorType == ErrorType.NotFound )
            {
                ex = new ResendException( HttpStatusCode.NotFound, err.ErrorType, err.Message );
            }
            else if ( err.ErrorType == ErrorType.DailyQuotaExceeded )
            {
                ex = new ResendDailyQuotaExceededException( HttpStatusCode.TooManyRequests, err.Message, rrl );
            }
            else if ( err.ErrorType == ErrorType.RateLimitExceeded )
            {
                ex = new ResendRateLimitExceededException( HttpStatusCode.TooManyRequests, err.Message, rrl );
            }
            else
                ex = new ResendException( (HttpStatusCode) err.StatusCode, err.ErrorType, err.Message );

            if ( _throw == true )
                throw ex;

            if ( _throw == true )
                throw ex;

            return new ResendResponse<T2>( ex, rrl );
        }


        /*
         *
         */
        T1? obj;

        try
        {
            obj = await resp.Content.ReadFromJsonAsync<T1>( cancellationToken );
        }
        catch ( TaskCanceledException )
        {
            throw;
        }
        catch ( Exception ex )
        {
            ResendException oex = new ResendException( HttpStatusCode.UnprocessableContent, ErrorType.Deserialization, "Failed deserializing response", ex );

            if ( _throw == true )
                throw oex;

            return new ResendResponse<T2>( oex, rrl );
        }


        /*
         *
         */
        if ( obj == null )
        {
            var ex = new ResendException( resp.StatusCode, ErrorType.MissingResponse, "Missing response" );

            if ( _throw )
                throw ex;

            return new ResendResponse<T2>( ex, rrl );
        }


        /*
         * Map T1 --> T2
         */
        T2 res;

        try
        {
            res = map( obj );
        }
        catch ( Exception ex )
        {
            var oex = new ResendException( resp.StatusCode, ErrorType.MissingResponse, "Failed to map response", ex );

            if ( _throw )
                throw oex;

            return new ResendResponse<T2>( oex, rrl );
        }


        /*
         * Ok
         */
        return new ResendResponse<T2>( res, rrl );
    }

    /// <summary>
    /// Rate limit info from the HTTP response headers.
    /// <summary />
    private ResendRateLimit FromHeaders( HttpResponseHeaders headers )
    {
        var rrl = new ResendRateLimit();
        rrl.Policy = Header2<string>( headers, "ratelimit-policy" );
        rrl.Limit = Header<int>( headers, "ratelimit-limit" );
        rrl.Remaining = Header<int>( headers, "ratelimit-remaining" );
        rrl.Reset = Header<int>( headers, "ratelimit-reset" );
        rrl.RetryAfter = Header<int>( headers, "retry-after" );

        return rrl;
    }


    /// <summary />
    private T? Header<T>( HttpResponseHeaders headers, string name )
        where T : struct
    {
        if ( headers.Contains( name ) == false )
            return default;

        var v = headers.GetValues( name ).First();

        return (T) Convert.ChangeType( v, typeof( T ) );
    }


    /// <summary />
    private T? Header2<T>( HttpResponseHeaders headers, string name )
        where T : class
    {
        if ( headers.Contains( name ) == false )
            return null;

        var v = headers.GetValues( name ).First();

        return (T) Convert.ChangeType( v, typeof( T ) );
    }


    /// <summary>
    /// Creates an instance of Resend client with the given client
    /// options.
    /// </summary>
    /// <param name="options">Resend client options.</param>
    /// <param name="http">HTTP client instance.</param>
    /// <returns>Instance of Resend client.</returns>
    /// <remarks>
    /// Utility method for examples/one-off apps. For most use-cases it is
    /// preferable to use dependency injection to configure/inject `IResend`
    /// instances.
    /// </remarks>
    public static IResend Create( ResendClientOptions options, HttpClient? http = null )
    {
        var snap = new OptionsSnapshot<ResendClientOptions>( options );

        return new ResendClient( snap, http ?? new HttpClient() );
    }


    /// <summary>
    /// Creates an instance of Resend with the given API token.
    /// </summary>
    /// <param name="apiToken">API token</param>
    /// <returns>Instance of Resend client.</returns>
    /// <remarks>
    /// Utility method for examples/one-off apps. For most use-cases it is
    /// preferable to use dependency injection to configure/inject `IResend`
    /// instances.
    /// </remarks>
    public static IResend Create( string apiToken )
    {
        var opt = new ResendClientOptions();
        opt.ApiToken = apiToken;

        return Create( opt );
    }
}
