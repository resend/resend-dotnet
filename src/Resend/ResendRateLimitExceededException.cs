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
    public ResendRateLimitExceededException( HttpStatusCode? statusCode, string message, ResendRateLimit? rateLimit )
        : base( statusCode, ErrorType.RateLimitExceeded, message )
    {
        this.Limits = rateLimit;
        string retryMessage = "Rate limit exceeded: please wait {0}s before retrying";
        retryMessage.replace("{0}", rateLimit.RetryAfter.ToString());
        this.Message = retryMessage;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ResendRateLimitExceededException"/> class.
    /// <summary />
    public ResendRateLimitExceededException( HttpStatusCode? statusCode, string message, Exception? innerException, ResendRateLimit? rateLimit )
        : base( statusCode, ErrorType.RateLimitExceeded, message, innerException )
    {
        this.Limits = rateLimit;
        string retryMessage = "Rate limit exceeded: please wait {0}s before retrying";
        retryMessage.replace("{0}", rateLimit.RetryAfter.ToString());
        this.Message = retryMessage;
    }

    /// <summary>
    /// Gets the rate limit information from the response headers.
    /// <summary />
    public ResendRateLimit? Limits { get; }
}