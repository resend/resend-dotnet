using System.Net;

namespace Resend;

/// <summary />
public class ResendException : ApplicationException
{
    /// <summary />
    public ResendException( HttpStatusCode? statusCode, ErrorType errorType, string message, ResendRateLimit? limits = null )
        : base( message )
    {
        this.StatusCode = statusCode;
        this.ErrorType = errorType;
        this.Limits = limits;
    }


    /// <summary />
    public ResendException( HttpStatusCode? statusCode, ErrorType errorType, string message, Exception? innerException, ResendRateLimit? limits = null )
        : base( message, innerException )
    {
        this.StatusCode = statusCode;
        this.ErrorType = errorType;
        this.Limits = limits;
    }


    /// <summary>
    /// Gets the HTTP status code, if the error was returned by the Resend API.
    /// </summary>
    public HttpStatusCode? StatusCode { get; }

    /// <summary>
    /// Gets the error.
    /// </summary>
    public ErrorType ErrorType { get; }

    /// <summary>
    /// Rate limit information.
    /// </summary>
    public ResendRateLimit? Limits { get; set; }


    /// <summary>
    /// Gets whether the error is transient, and whether the same request can be done
    /// again.
    /// </summary>
    public bool IsTransient
    {
        get
        {
            return this.ErrorType switch
            {
                ErrorType.MonthlyQuotaExceeded => true,
                ErrorType.DailyQuotaExceeded => true,
                ErrorType.RateLimitExceeded => true,
                ErrorType.HttpSendFailed => true,

                _ => false
            };
        }
    }
}