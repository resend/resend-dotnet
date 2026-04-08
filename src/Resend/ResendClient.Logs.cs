using Microsoft.AspNetCore.WebUtilities;

namespace Resend;

public partial class ResendClient
{
    /// <inheritdoc />
    public Task<ResendResponse<PaginatedResult<Log>>> LogListAsync( PaginatedQuery? query = null, CancellationToken cancellationToken = default )
    {
        var baseUrl = "/logs";
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

        return Execute<PaginatedResult<Log>, PaginatedResult<Log>>( req, ( x ) => x, cancellationToken );
    }


    /// <inheritdoc />
    public Task<ResendResponse<Log>> LogRetrieveAsync( Guid logId, CancellationToken cancellationToken = default )
    {
        var path = $"/logs/{logId}";
        var req = new HttpRequestMessage( HttpMethod.Get, path );

        return Execute<Log, Log>( req, ( x ) => x, cancellationToken );
    }
}
