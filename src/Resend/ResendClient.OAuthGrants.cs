using Microsoft.AspNetCore.WebUtilities;
using Resend.Payloads;

namespace Resend;

public partial class ResendClient
{
    /// <inheritdoc />
    public Task<ResendResponse<PaginatedResult<OAuthGrant>>> OAuthGrantListAsync( PaginatedQuery? query = null, CancellationToken cancellationToken = default )
    {
        var baseUrl = "/oauth/grants";
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

        return Execute<PaginatedResult<OAuthGrant>, PaginatedResult<OAuthGrant>>( req, ( x ) => x, cancellationToken );
    }


    /// <inheritdoc />
    public Task<ResendResponse<OAuthGrantRevoked>> OAuthGrantRevokeAsync( Guid oauthGrantId, CancellationToken cancellationToken = default )
    {
        var path = $"/oauth/grants/{oauthGrantId}";
        var req = new HttpRequestMessage( HttpMethod.Delete, path );

        return Execute<OAuthGrantRevoked, OAuthGrantRevoked>( req, ( x ) => x, cancellationToken );
    }
}
