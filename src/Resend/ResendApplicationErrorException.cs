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
    public ResendApplicationErrorException( string message )
        : base( HttpStatusCode.InternalServerError, ErrorType.ApplicationError, message )
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="ResendApplicationErrorException"/> class.
    /// <summary />
    public ResendApplicationErrorException( string message, Exception? innerException )
        : base( HttpStatusCode.InternalServerError, ErrorType.ApplicationError, message, innerException )
    {
    }
}