using System.Net;

namespace Resend;

/// <summary>
/// Status code 401. This API key is restricted to only send emails.
/// <summary />
public class ResendRestrictedApiKeyException : ResendException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ResendRestrictedApiKeyException"/> class.
    /// <summary />
    public ResendRestrictedApiKeyException( HttpStatusCode? statusCode, string message )
        : base( statusCode, ErrorType.RateLimitExceeded, message )
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="ResendRestrictedApiKeyException"/> class.
    /// <summary />
    public ResendRestrictedApiKeyException( HttpStatusCode? statusCode, string message, Exception? innerException )
        : base( statusCode, ErrorType.RateLimitExceeded, message, innerException )
    { }
}