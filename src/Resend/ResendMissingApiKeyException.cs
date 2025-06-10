using System.Net;

namespace Resend;

/// <summary>
/// Status code 401. Missing API key in the authorization header.
/// <summary />
public class ResendMissingApiKeyException : ResendException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ResendMissingApiKeyException"/> class.
    /// <summary />
    public ResendMissingApiKeyException( string message )
        : base( HttpStatusCode.Unauthorized, ErrorType.MissingApiKey, message )
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="ResendMissingApiKeyException"/> class.
    /// <summary />
    public ResendMissingApiKeyException( string message, Exception? innerException )
        : base( HttpStatusCode.Unauthorized, ErrorType.MissingApiKey, message, innerException )
    {
    }
}