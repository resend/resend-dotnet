using Resend.Payloads;

namespace Resend;

public partial class ResendClient
{
    /// <inheritdoc />
    public Task<ResendResponse<List<OAuthGrant>>> OAuthGrantListAsync( CancellationToken cancellationToken = default )
    {
        var path = $"/oauth/grants";
        var req = new HttpRequestMessage( HttpMethod.Get, path );

        return Execute<ListOf<OAuthGrant>, List<OAuthGrant>>( req, ( x ) => x.Data, cancellationToken );
    }


    /// <inheritdoc />
    public Task<ResendResponse<OAuthGrantRevoked>> OAuthGrantRevokeAsync( Guid oauthGrantId, CancellationToken cancellationToken = default )
    {
        var path = $"/oauth/grants/{oauthGrantId}";
        var req = new HttpRequestMessage( HttpMethod.Delete, path );

        return Execute<OAuthGrantRevoked, OAuthGrantRevoked>( req, ( x ) => x, cancellationToken );
    }
}
