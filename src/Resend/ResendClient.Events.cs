using Microsoft.AspNetCore.WebUtilities;
using Resend.Payloads;
using System.Net.Http.Json;

namespace Resend;

public partial class ResendClient
{
    /// <inheritdoc />
    public Task<ResendResponse<Guid>> EventCreateAsync( EventCreateData data, CancellationToken cancellationToken = default )
    {
        var req = new HttpRequestMessage( HttpMethod.Post, "/events" );
        req.Content = JsonContent.Create( data );

        return Execute<ObjectId, Guid>( req, ( x ) => x.Id, cancellationToken );
    }


    /// <inheritdoc />
    public Task<ResendResponse<EventResource>> EventRetrieveAsync( Guid eventId, CancellationToken cancellationToken = default )
    {
        var req = new HttpRequestMessage( HttpMethod.Get, $"/events/{eventId}" );

        return Execute<EventResource, EventResource>( req, ( x ) => x, cancellationToken );
    }


    /// <inheritdoc />
    public Task<ResendResponse<EventResource>> EventRetrieveAsync( string eventIdOrName, CancellationToken cancellationToken = default )
    {
        var req = new HttpRequestMessage( HttpMethod.Get, $"/events/{Uri.EscapeDataString( eventIdOrName )}" );

        return Execute<EventResource, EventResource>( req, ( x ) => x, cancellationToken );
    }


    /// <inheritdoc />
    public Task<ResendResponse<PaginatedResult<EventResource>>> EventListAsync( PaginatedQuery? query = null, CancellationToken cancellationToken = default )
    {
        var baseUrl = "/events";
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

        return Execute<PaginatedResult<EventResource>, PaginatedResult<EventResource>>( req, ( x ) => x, cancellationToken );
    }


    /// <inheritdoc />
    public Task<ResendResponse<Guid>> EventUpdateAsync( Guid eventId, EventUpdateData data, CancellationToken cancellationToken = default )
    {
        var req = new HttpRequestMessage( HttpMethod.Patch, $"/events/{eventId}" );
        req.Content = JsonContent.Create( data );

        return Execute<ObjectId, Guid>( req, ( x ) => x.Id, cancellationToken );
    }


    /// <inheritdoc />
    public Task<ResendResponse<Guid>> EventUpdateAsync( string eventIdOrName, EventUpdateData data, CancellationToken cancellationToken = default )
    {
        var req = new HttpRequestMessage( HttpMethod.Patch, $"/events/{Uri.EscapeDataString( eventIdOrName )}" );
        req.Content = JsonContent.Create( data );

        return Execute<ObjectId, Guid>( req, ( x ) => x.Id, cancellationToken );
    }


    /// <inheritdoc />
    public Task<ResendResponse<EventDeleteResult>> EventDeleteAsync( Guid eventId, CancellationToken cancellationToken = default )
    {
        var req = new HttpRequestMessage( HttpMethod.Delete, $"/events/{eventId}" );

        return Execute<EventDeleteResult, EventDeleteResult>( req, ( x ) => x, cancellationToken );
    }


    /// <inheritdoc />
    public Task<ResendResponse<EventDeleteResult>> EventDeleteAsync( string eventIdOrName, CancellationToken cancellationToken = default )
    {
        var req = new HttpRequestMessage( HttpMethod.Delete, $"/events/{Uri.EscapeDataString( eventIdOrName )}" );

        return Execute<EventDeleteResult, EventDeleteResult>( req, ( x ) => x, cancellationToken );
    }


    /// <inheritdoc />
    public Task<ResendResponse<EventSendResult>> EventSendAsync( EventSendData data, CancellationToken cancellationToken = default )
    {
        var req = new HttpRequestMessage( HttpMethod.Post, "/events/send" );
        req.Content = JsonContent.Create( data );

        return Execute<EventSendResult, EventSendResult>( req, ( x ) => x, cancellationToken );
    }
}
