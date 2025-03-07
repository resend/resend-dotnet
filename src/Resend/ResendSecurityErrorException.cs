using System.Net;

namespace Resend;

/// <summary>
/// Status code 451. Resend may have found a security issue with the request.
/// <summary />
public class ResendSecurityErrorException : ResendException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ResendSecurityErrorException"/> class.
    /// <summary />
    public ResendSecurityErrorException( string message )
        : base( HttpStatusCode.UnavailableForLegalReasons, ErrorType.SecurityError, message )
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="ResendSecurityErrorException"/> class.
    /// <summary />
    public ResendSecurityErrorException( string message, Exception? innerException )
        : base( HttpStatusCode.UnavailableForLegalReasons, ErrorType.SecurityError, message, innerException )
    {
    }
}