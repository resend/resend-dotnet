using System.Net;

namespace Resend;

/// <summary>
/// Status code 422. Attachment must have either a content or path.
/// <summary />
public class ResendInvalidAttachmentException : ResendException
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ResendInvalidAttachmentException"/> class.
    /// <summary />
    public ResendInvalidAttachmentException( string message )
        : base( HttpStatusCode.UnprocessableContent, ErrorType.InvalidAttachment, message )
    { }

    /// <summary>
    /// Initializes a new instance of the <see cref="ResendInvalidAttachmentException"/> class.
    /// <summary />
    public ResendInvalidAttachmentException( string message, Exception? innerException )
        : base( HttpStatusCode.UnprocessableContent, ErrorType.InvalidAttachment, message, innerException )
    { }
}