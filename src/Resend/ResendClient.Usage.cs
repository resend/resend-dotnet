namespace Resend;

public partial class ResendClient
{
    /// <inheritdoc />
    public Task<ResendResponse<Usage>> UsageAsync( CancellationToken cancellationToken = default )
    {
        var req = new HttpRequestMessage( HttpMethod.Get, "/usage" );

        return Execute<Usage, Usage>( req, ( x ) => x, cancellationToken );
    }
}
