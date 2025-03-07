using System.Net;

namespace Resend;

/// <summary>
/// Status code 429. User has hit their daily quota and should wait before resending the request.
/// <summary />
public class ResendDailyQuotaExceededException : ResendException, IHasRateLimit
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ResendDailyQuotaExceededException"/> class.
    /// <summary />
    public ResendDailyQuotaExceededException( string message, ResendRateLimit? rateLimit )
        : base( HttpStatusCode.TooManyRequests, ErrorType.DailyQuotaExceeded, message )
    {
        this.Limits = rateLimit;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ResendDailyQuotaExceededException"/> class.
    /// <summary />
    public ResendDailyQuotaExceededException( string message, Exception? innerException, ResendRateLimit? rateLimit )
        : base( HttpStatusCode.TooManyRequests, ErrorType.DailyQuotaExceeded, message, innerException )
    {
        this.Limits = rateLimit;
    }

    /// <summary>
    /// Gets the rate limit information from the response headers.
    /// <summary />
    public ResendRateLimit? Limits { get; }
}