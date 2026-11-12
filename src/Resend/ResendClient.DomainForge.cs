using System.Net.Http.Json;

namespace Resend;

public partial class ResendClient
{
    /// <inheritdoc />
    public Task<ResendResponse<Domain>> DomainForgeEnableAsync( Guid domainId, string? returnPath = null, CancellationToken cancellationToken = default )
    {
        var path = $"/domains/{domainId}/forge";
        var req = new HttpRequestMessage( HttpMethod.Post, path );
        req.Content = JsonContent.Create( new DomainForgeData()
        {
            ReturnPath = returnPath,
        } );

        return Execute<Domain, Domain>( req, ( x ) => x, cancellationToken );
    }


    /// <inheritdoc />
    public Task<ResendResponse<Domain>> DomainForgeUpdateAsync( Guid domainId, string returnPath, CancellationToken cancellationToken = default )
    {
        var path = $"/domains/{domainId}/forge";
        var req = new HttpRequestMessage( HttpMethod.Patch, path );
        req.Content = JsonContent.Create( new DomainForgeData()
        {
            ReturnPath = returnPath,
        } );

        return Execute<Domain, Domain>( req, ( x ) => x, cancellationToken );
    }


    /// <inheritdoc />
    public Task<ResendResponse> DomainForgeVerifyAsync( Guid domainId, CancellationToken cancellationToken = default )
    {
        var path = $"/domains/{domainId}/forge/verify";
        var req = new HttpRequestMessage( HttpMethod.Post, path );

        return Execute( req, cancellationToken );
    }
}
