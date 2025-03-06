using System.Net;

namespace Resend;

/// <summary>
/// Status code 500. An unexpected error occurred.
/// <summary />
public class ResendApplicationErrorException : ResendException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ResendApplicationErrorException"/> class.
    /// <summary />
    public ResendApplicationErrorException( HttpStatusCode? statusCode, string message )
        : base( statusCode, ErrorType.ApplicationError, message )
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="ResendApplicationErrorException"/> class.
    /// <summary />
    public ResendApplicationErrorException( HttpStatusCode? statusCode, string message, Exception? innerException )
        : base( statusCode, ErrorType.ApplicationError, message, innerException )
    {
    }
}