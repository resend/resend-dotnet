using Microsoft.AspNetCore.WebUtilities;
using Resend.Payloads;
using System.Net.Http.Json;

namespace Resend;

public partial class ResendClient
{
    /// <inheritdoc />
    public Task<ResendResponse<PaginatedResult<Segment>>> SegmentListAsync( PaginatedQuery? query = null, CancellationToken cancellationToken = default )
    {
        var baseUrl = "/segments";
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

        return Execute<PaginatedResult<Segment>, PaginatedResult<Segment>>( req, ( x ) => x, cancellationToken );
    }


    /// <inheritdoc />
    public Task<ResendResponse<Guid>> SegmentCreateAsync( SegmentData segment, CancellationToken cancellationToken = default )
    {
        var req = new HttpRequestMessage( HttpMethod.Post, "/segments" );
        req.Content = JsonContent.Create( segment );

        return Execute<ObjectId, Guid>( req, ( x ) => x.Id, cancellationToken );
    }


    /// <inheritdoc />
    public Task<ResendResponse<Segment>> SegmentRetrieveAsync( Guid segmentId, CancellationToken cancellationToken = default )
    {
        var path = $"/segments/{segmentId}";
        var req = new HttpRequestMessage( HttpMethod.Get, path );

        return Execute<Segment, Segment>( req, ( x ) => x, cancellationToken );
    }


    /// <inheritdoc />
    public Task<ResendResponse> SegmentDeleteAsync( Guid segmentId, CancellationToken cancellationToken = default )
    {
        var req = new HttpRequestMessage( HttpMethod.Delete, $"/segments/{segmentId}" );

        return Execute( req, cancellationToken );
    }
}