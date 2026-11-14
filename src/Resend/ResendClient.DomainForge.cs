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
}
