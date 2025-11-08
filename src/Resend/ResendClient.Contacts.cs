using Microsoft.AspNetCore.WebUtilities;
using Resend.Payloads;
using System.Net.Http.Json;

namespace Resend;

public partial class ResendClient
{
    /// <inheritdoc/>
    public Task<ResendResponse<Guid>> ContactAddAsync( ContactData data, CancellationToken cancellationToken = default )
    {
        if ( data.Email == null )
            throw new ArgumentException( "Email must be non-null when creating contact", nameof( data ) + ".Email" );

        var path = $"/contacts";
        var req = new HttpRequestMessage( HttpMethod.Post, path );
        req.Content = JsonContent.Create( data );

        return Execute<ObjectId, Guid>( req, ( x ) => x.Id, cancellationToken );
    }


    /// <inheritdoc/>
    public Task<ResendResponse<Contact>> ContactRetrieveAsync( Guid contactId, CancellationToken cancellationToken = default )
    {
        var path = $"/contacts/{contactId}";
        var req = new HttpRequestMessage( HttpMethod.Get, path );

        return Execute<Contact, Contact>( req, ( x ) => x, cancellationToken );
    }


    /// <inheritdoc/>
    public Task<ResendResponse<Contact>> ContactRetrieveByEmailAsync( string email, CancellationToken cancellationToken = default )
    {
        var path = $"/contacts/{email}";
        var req = new HttpRequestMessage( HttpMethod.Get, path );

        return Execute<Contact, Contact>( req, ( x ) => x, cancellationToken );
    }


    /// <inheritdoc/>
    public Task<ResendResponse> ContactUpdateAsync( Guid contactId, ContactData data, CancellationToken cancellationToken = default )
    {
        var path = $"/contacts/{contactId}";
        var req = new HttpRequestMessage( HttpMethod.Patch, path );
        req.Content = JsonContent.Create( data );

        return Execute( req, cancellationToken );
    }


    /// <inheritdoc/>
    public Task<ResendResponse> ContactUpdateByEmailAsync( string email, ContactData data, CancellationToken cancellationToken = default )
    {
        var path = $"/contacts/{email}";
        var req = new HttpRequestMessage( HttpMethod.Patch, path );
        req.Content = JsonContent.Create( data );

        return Execute( req, cancellationToken );
    }


    /// <inheritdoc/>
    public Task<ResendResponse> ContactDeleteAsync( Guid contactId, CancellationToken cancellationToken = default )
    {
        var path = $"/contacts/{contactId}";
        var req = new HttpRequestMessage( HttpMethod.Delete, path );

        return Execute( req, cancellationToken );
    }


    /// <inheritdoc/>
    public Task<ResendResponse> ContactDeleteByEmailAsync( string email, CancellationToken cancellationToken = default )
    {
        var path = $"/contacts/{email}";
        var req = new HttpRequestMessage( HttpMethod.Delete, path );

        return Execute( req, cancellationToken );
    }


    /// <inheritdoc/>
    public Task<ResendResponse<PaginatedResult<Contact>>> ContactListAsync( PaginatedQuery? query = null, CancellationToken cancellationToken = default )
    {
        var baseUrl = $"/contacts";
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

        return Execute<PaginatedResult<Contact>, PaginatedResult<Contact>>( req, ( x ) => x, cancellationToken );
    }


    /// <inheritdoc/>
    public Task<ResendResponse<PaginatedResult<Segment>>> ContactListSegmentsAsync( Guid contactId, PaginatedQuery? query = null, CancellationToken cancellationToken = default )
    {
        var baseUrl = $"/contacts/{contactId}/segments";
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


    /// <inheritdoc/>
    public Task<ResendResponse> ContactAddToSegmentAsync( Guid contactId, Guid segmentId, CancellationToken cancellationToken = default )
    {
        var path = $"/contacts/{contactId}/segments/{segmentId}";
        var req = new HttpRequestMessage( HttpMethod.Post, path );

        return Execute( req, cancellationToken );
    }


    /// <inheritdoc/>
    public Task<ResendResponse> ContactRemoveFromSegmentAsync( Guid contactId, Guid segmentId, CancellationToken cancellationToken = default )
    {
        var path = $"/contacts/{contactId}/segments/{segmentId}";
        var req = new HttpRequestMessage( HttpMethod.Delete, path );

        return Execute( req, cancellationToken );
    }


    /// <inheritdoc/>
    public Task<ResendResponse<PaginatedResult<TopicSubscription>>> ContactListTopicsAsync( Guid contactId, PaginatedQuery? query = null, CancellationToken cancellationToken = default )
    {
        var baseUrl = $"/contacts/{contactId}/topics";
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

        return Execute<PaginatedResult<TopicSubscription>, PaginatedResult<TopicSubscription>>( req, ( x ) => x, cancellationToken );
    }


    /// <inheritdoc/>
    public Task<ResendResponse> ContactUpdateTopicsAsync( Guid contactId, List<TopicSubscription> topics, CancellationToken cancellationToken = default )
    {
        var url = $"/contacts/{contactId}/topics";
        var req = new HttpRequestMessage( HttpMethod.Patch, url );
        req.Content = JsonContent.Create( topics );

        return Execute( req, cancellationToken );
    }
}