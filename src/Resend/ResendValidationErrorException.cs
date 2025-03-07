using System.Net;

namespace Resend;

/// <summary>
/// Status code 400. Resend found an error with one or more fields in the request.
/// <summary />
public class ResendValidationErrorException : ResendException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ResendValidationErrorException"/> class.
    /// <summary />
    public ResendValidationErrorException( HttpStatusCode statusCode, string message )
        : base( statusCode, ErrorType.ValidationError, message )
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="ResendValidationErrorException"/> class.
    /// <summary />
    public ResendValidationErrorException( HttpStatusCode statusCode, string message, Exception? innerException )
        : base( statusCode, ErrorType.ValidationError, message, innerException )
    { }
}