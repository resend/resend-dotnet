using Microsoft.AspNetCore.WebUtilities;
using Resend.Payloads;
using System.Net.Http.Json;

namespace Resend;

public partial class ResendClient
{
    /// <inheritdoc />
    public Task<ResendResponse<PaginatedResult<ContactProperty>>> ContactPropListAsync( PaginatedQuery? query = null, CancellationToken cancellationToken = default )
    {
        var baseUrl = "/contact-properties";
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

        return Execute<PaginatedResult<ContactProperty>, PaginatedResult<ContactProperty>>( req, ( x ) => x, cancellationToken );
    }


    /// <inheritdoc />
    public Task<ResendResponse<Guid>> ContactPropCreateAsync( ContactPropertyData prop, CancellationToken cancellationToken = default )
    {
        var req = new HttpRequestMessage( HttpMethod.Post, "/contact-properties" );
        req.Content = JsonContent.Create( prop );

        return Execute<ObjectId, Guid>( req, ( x ) => x.Id, cancellationToken );
    }


    /// <inheritdoc />
    public Task<ResendResponse<ContactProperty>> ContactPropRetrieveAsync( Guid propId, CancellationToken cancellationToken = default )
    {
        var path = $"/contact-properties/{propId}";
        var req = new HttpRequestMessage( HttpMethod.Get, path );

        return Execute<ContactProperty, ContactProperty>( req, ( x ) => x, cancellationToken );
    }


    /// <inheritdoc />
    public Task<ResendResponse> ContactPropUpdateAsync( Guid propId, ContactPropertyUpdateData prop, CancellationToken cancellationToken = default )
    {
        var req = new HttpRequestMessage( HttpMethod.Patch, $"/contact-properties/{propId}" );
        req.Content = JsonContent.Create( prop );

        return Execute( req, cancellationToken );
    }


    /// <inheritdoc />
    public Task<ResendResponse> ContactPropDeleteAsync( Guid propId, CancellationToken cancellationToken = default )
    {
        var req = new HttpRequestMessage( HttpMethod.Delete, $"/contact-properties/{propId}" );

        return Execute( req, cancellationToken );
    }
}