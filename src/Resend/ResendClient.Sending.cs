using Microsoft.AspNetCore.WebUtilities;

namespace Resend;

public partial class ResendClient
{
    /// <inheritdoc />
    public Task<ResendResponse<PaginatedResult<SentEmailAttachment>>> EmailAttachmentListAsync( Guid emailId, PaginatedQuery? query = null, CancellationToken cancellationToken = default )
    {
        var baseUrl = $"/emails/{emailId}/attachments";
        var url = baseUrl;

        if ( query != null )
        {
            var qs = new Dictionary<string, string?>();

            if ( query.Limit.HasValue == true )
                qs.Add( "limit", query.Limit.Value.ToString() );

            if ( query.Before != null )
                qs.Add( "before", query.Before );

            if ( query.After != null )
                qs.Add( "after", query.After );

            url = QueryHelpers.AddQueryString( baseUrl, qs );
        }

        var req = new HttpRequestMessage( HttpMethod.Get, url );

        return Execute<PaginatedResult<SentEmailAttachment>, PaginatedResult<SentEmailAttachment>>( req, ( x ) => x, cancellationToken );
    }


    /// <inheritdoc />
    public Task<ResendResponse<SentEmailAttachment>> EmailAttachmentRetrieveAsync( Guid emailId, Guid attachmentId, CancellationToken cancellationToken = default )
    {
        var path = $"/emails/{emailId}/attachments/{attachmentId}";
        var req = new HttpRequestMessage( HttpMethod.Get, path );

        return Execute<SentEmailAttachment, SentEmailAttachment>( req, ( x ) => x, cancellationToken );
    }
}