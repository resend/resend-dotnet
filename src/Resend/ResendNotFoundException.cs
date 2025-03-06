using System.Net;

namespace Resend;

/// <summary>
/// Status code 404. The requested endpoint does not exist.
/// <summary />
public class ResendNotFoundException : ResendException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ResendNotFoundException"/> class.
    /// <summary />
    public ResendNotFoundException( HttpStatusCode? statusCode, string message )
        : base( statusCode, ErrorType.RateLimitExceeded, message )
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="ResendNotFoundException"/> class.
    /// <summary />
    public ResendNotFoundException( HttpStatusCode? statusCode, string message, Exception? innerException )
        : base( statusCode, ErrorType.RateLimitExceeded, message, innerException )
    { }
}