using System.Net;

namespace Resend;

/// <summary>
/// Status code 422. The request body is missing one or more required fields.
/// <summary />
public class ResendMissingRequiredFieldException : ResendException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ResendMissingRequiredFieldException"/> class.
    /// <summary />
    public ResendMissingRequiredFieldException( HttpStatusCode? statusCode, string message )
        : base( statusCode, ErrorType.MissingRequiredField, message )
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="ResendMissingRequiredFieldException"/> class.
    /// <summary />
    public ResendMissingRequiredFieldException( HttpStatusCode? statusCode, string message, Exception? innerException )
        : base( statusCode, ErrorType.MissingRequiredField, message, innerException )
    {
    }
}