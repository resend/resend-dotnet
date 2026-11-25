using Microsoft.AspNetCore.WebUtilities;

namespace Resend;

public partial class ResendClient
{
    /// <inheritdoc />
    public Task<ResendResponse<PaginatedResult<ReceivedEmail>>> ReceivedEmailListAsync( PaginatedQuery? query = null, CancellationToken cancellationToken = default )
    {
        return ReceivedEmailList( query, cancellationToken );
    }


    /// <inheritdoc />
    public Task<ResendResponse<PaginatedResult<ReceivedEmail>>> ReceivedEmailListAsync( ReceivedEmailListQuery? query, CancellationToken cancellationToken = default )
    {
        return ReceivedEmailList( query, cancellationToken );
    }


    /// <summary />
    private Task<ResendResponse<PaginatedResult<ReceivedEmail>>> ReceivedEmailList( PaginatedQuery? query, CancellationToken cancellationToken )
    {
        var baseUrl = "/emails/receiving";
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

            if ( query is ReceivedEmailListQuery filters )
            {
                // The API rejects a repeated `to`, so the values travel as one comma-joined parameter.
                if ( filters.To != null && filters.To.Count > 0 )
                    qs.Add( "to", string.Join( ",", filters.To ) );

                if ( filters.StartDate != null )
                    qs.Add( "start_date", filters.StartDate );

                if ( filters.EndDate != null )
                    qs.Add( "end_date", filters.EndDate );
            }

            url = QueryHelpers.AddQueryString( baseUrl, qs );
        }

        var req = new HttpRequestMessage( HttpMethod.Get, url );

        return Execute<PaginatedResult<ReceivedEmail>, PaginatedResult<ReceivedEmail>>( req, ( x ) => x, cancellationToken );
    }


    /// <inheritdoc />
    public Task<ResendResponse<ReceivedEmail>> ReceivedEmailRetrieveAsync( Guid emailId, CancellationToken cancellationToken = default )
    {
        var path = $"/emails/receiving/{emailId}";
        var req = new HttpRequestMessage( HttpMethod.Get, path );

        return Execute<ReceivedEmail, ReceivedEmail>( req, ( x ) => x, cancellationToken );
    }


    /// <inheritdoc />
    public Task<ResendResponse<PaginatedResult<ReceivedEmailAttachment>>> ReceivedEmailAttachmentListAsync( Guid emailId, PaginatedQuery? query = null, CancellationToken cancellationToken = default )
    {
        var baseUrl = $"/emails/receiving/{emailId}/attachments";
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

        return Execute<PaginatedResult<ReceivedEmailAttachment>, PaginatedResult<ReceivedEmailAttachment>>( req, ( x ) => x, cancellationToken );
    }


    /// <inheritdoc />
    public Task<ResendResponse<ReceivedEmailAttachment>> ReceivedEmailAttachmentRetrieveAsync( Guid emailId, Guid attachmentId, CancellationToken cancellationToken = default )
    {
        var path = $"/emails/receiving/{emailId}/attachments/{attachmentId}";
        var req = new HttpRequestMessage( HttpMethod.Get, path );

        return Execute<ReceivedEmailAttachment, ReceivedEmailAttachment>( req, ( x ) => x, cancellationToken );
    }
}