using System.Net.Http.Json;

namespace Resend;

public partial class ResendClient
{
    /// <inheritdoc />
    public Task<ResendResponse<DomainClaim>> DomainClaimAsync( string domainName, DeliveryRegion? region = null, CancellationToken cancellationToken = default )
    {
        var path = $"/domains/claim";
        var req = new HttpRequestMessage( HttpMethod.Post, path );
        req.Content = JsonContent.Create( new DomainClaimData()
        {
            DomainName = domainName,
            Region = region,
        } );

        return Execute<DomainClaim, DomainClaim>( req, ( x ) => x, cancellationToken );
    }


    /// <inheritdoc />
    public Task<ResendResponse<DomainClaim>> DomainClaimAsync( DomainClaimData data, CancellationToken cancellationToken = default )
    {
        var path = $"/domains/claim";
        var req = new HttpRequestMessage( HttpMethod.Post, path );
        req.Content = JsonContent.Create( data );

        return Execute<DomainClaim, DomainClaim>( req, ( x ) => x, cancellationToken );
    }


    /// <inheritdoc />
    public Task<ResendResponse<DomainClaim>> DomainClaimRetrieveAsync( Guid domainId, CancellationToken cancellationToken = default )
    {
        var path = $"/domains/{domainId}/claim";
        var req = new HttpRequestMessage( HttpMethod.Get, path );

        return Execute<DomainClaim, DomainClaim>( req, ( x ) => x, cancellationToken );
    }


    /// <inheritdoc />
    public Task<ResendResponse<DomainClaim>> DomainClaimVerifyAsync( Guid domainId, CancellationToken cancellationToken = default )
    {
        var path = $"/domains/{domainId}/claim/verify";
        var req = new HttpRequestMessage( HttpMethod.Post, path );

        return Execute<DomainClaim, DomainClaim>( req, ( x ) => x, cancellationToken );
    }
}
