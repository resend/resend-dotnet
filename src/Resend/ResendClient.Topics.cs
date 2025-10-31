using Microsoft.AspNetCore.WebUtilities;
using Resend.Payloads;
using System.Net.Http.Json;

namespace Resend;

public partial class ResendClient
{
    /// <inheritdoc />
    public Task<ResendResponse<PaginatedResult<Topic>>> TopicListAsync( PaginatedQuery? query = null, CancellationToken cancellationToken = default )
    {
        var baseUrl = "/topics";
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

        return Execute<PaginatedResult<Topic>, PaginatedResult<Topic>>( req, ( x ) => x, cancellationToken );
    }


    /// <inheritdoc />
    public Task<ResendResponse<Guid>> TopicCreateAsync( TopicData topic, CancellationToken cancellationToken = default )
    {
        var req = new HttpRequestMessage( HttpMethod.Post, "/topics" );
        req.Content = JsonContent.Create( topic );

        return Execute<ObjectId, Guid>( req, ( x ) => x.Id, cancellationToken );
    }


    /// <inheritdoc />
    public Task<ResendResponse<Topic>> TopicRetrieveAsync( Guid topicId, CancellationToken cancellationToken = default )
    {
        var path = $"/topics/{topicId}";
        var req = new HttpRequestMessage( HttpMethod.Get, path );

        return Execute<Topic, Topic>( req, ( x ) => x, cancellationToken );
    }


    /// <inheritdoc />
    public Task<ResendResponse> TopicUpdateAsync( Guid topicId, TopicData topic, CancellationToken cancellationToken = default )
    {
        var req = new HttpRequestMessage( HttpMethod.Patch, $"/topics/{topicId}" );
        req.Content = JsonContent.Create( topic );

        return Execute( req, cancellationToken );
    }


    /// <inheritdoc />
    public Task<ResendResponse> TopicDeleteAsync( Guid topicId, CancellationToken cancellationToken = default )
    {
        var req = new HttpRequestMessage( HttpMethod.Delete, $"/topics/{topicId}" );

        return Execute( req, cancellationToken );
    }
}