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
    public ResendNotFoundException( string message )
        : base( HttpStatusCode.NotFound, ErrorType.NotFound, message )
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="ResendNotFoundException"/> class.
    /// <summary />
    public ResendNotFoundException( string message, Exception? innerException )
        : base( HttpStatusCode.NotFound, ErrorType.NotFound, message, innerException )
    { }
}