using Microsoft.Extensions.Options;
using System.Net;

namespace Resend;

/// <summary />
public class ResendMockClient : ResendClient, IResend
{
    private static int _retryCount = 0;


    /// <summary />
    public ResendMockClient( IOptions<ResendClientOptions> options, HttpClient httpClient )
        : base( options, httpClient )
    {
    }


    /// <summary />
    public new async Task<ResendResponse<Guid>> EmailSendAsync( EmailMessage email, CancellationToken cancellationToken = default )
    {
        /*
         * Wait for 3s, so that it's possible to observe the job instance in
         * the 'Processing' status.
         */
        await Task.Delay( 3_000, cancellationToken );


        /*
         * 
         */
        Interlocked.Increment( ref _retryCount );

        if ( _retryCount == 1 )
            throw new ResendException( HttpStatusCode.TooManyRequests, ErrorType.DailyQuotaExceeded, "Daily quota exceeded" );

        if ( _retryCount == 2 )
            throw new ResendException( HttpStatusCode.TooManyRequests, ErrorType.RateLimitExceeded, "Rate limit exceeded" );


        /*
         * 
         */
        var resp = await base.EmailSendAsync( email, cancellationToken );

        Interlocked.Exchange( ref _retryCount, 0 );

        return resp;
    }
}