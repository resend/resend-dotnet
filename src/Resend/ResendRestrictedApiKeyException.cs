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
    public ResendRestrictedApiKeyException( string message )
        : base( HttpStatusCode.Forbidden, ErrorType.RestrictedApiKey, message )
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="ResendRestrictedApiKeyException"/> class.
    /// <summary />
    public ResendRestrictedApiKeyException( string message, Exception? innerException )
        : base( HttpStatusCode.Forbidden, ErrorType.RestrictedApiKey, message, innerException )
    { }
}