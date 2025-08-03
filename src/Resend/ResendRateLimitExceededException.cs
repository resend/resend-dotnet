using System.Net;

namespace Resend;

/// <summary>
/// Status code 429. User has hit their rate limit and should wait before resending the request.
/// <summary />
public class ResendRateLimitExceededException : ResendException, IHasRateLimit
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ResendRateLimitExceededException"/> class.
    /// <summary />
    public ResendRateLimitExceededException( string message, ResendRateLimit? rateLimit )
        : base( HttpStatusCode.TooManyRequests, ErrorType.RateLimitExceeded, message )
    {
        this.Limits = rateLimit;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ResendRateLimitExceededException"/> class.
    /// <summary />
    public ResendRateLimitExceededException( string message, Exception? innerException, ResendRateLimit? rateLimit )
        : base( HttpStatusCode.TooManyRequests, ErrorType.RateLimitExceeded, message, innerException )
    {
        this.Limits = rateLimit;
    }

    /// <summary>
    /// Gets the rate limit information from the response headers.
    /// <summary />
    public ResendRateLimit? Limits { get; }
}