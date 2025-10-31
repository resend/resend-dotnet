using Microsoft.AspNetCore.WebUtilities;
using System.Net.Http.Json;

namespace Resend;

public partial class ResendClient
{
    /// <inheritdoc />
    public Task<ResendResponse<PaginatedResult<Webhook>>> WebhookListAsync( PaginatedQuery? query = null, CancellationToken cancellationToken = default )
    {
        var baseUrl = "/webhooks";
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

        return Execute<PaginatedResult<Webhook>, PaginatedResult<Webhook>>( req, ( x ) => x, cancellationToken );
    }


    /// <inheritdoc />
    public Task<ResendResponse<WebhookNew>> WebhookCreateAsync( WebhookData webhook, CancellationToken cancellationToken = default )
    {
        var req = new HttpRequestMessage( HttpMethod.Post, "/webhooks" );
        req.Content = JsonContent.Create( webhook );

        return Execute<WebhookNew, WebhookNew>( req, ( x ) => x, cancellationToken );
    }


    /// <inheritdoc />
    public Task<ResendResponse<Webhook>> WebhookRetrieveAsync( Guid webhookId, CancellationToken cancellationToken = default )
    {
        var path = $"/webhooks/{webhookId}";
        var req = new HttpRequestMessage( HttpMethod.Get, path );

        return Execute<Webhook, Webhook>( req, ( x ) => x, cancellationToken );
    }


    /// <inheritdoc />
    public Task<ResendResponse> WebhookUpdateAsync( Guid webhookId, WebhookData webhook, CancellationToken cancellationToken = default )
    {
        var req = new HttpRequestMessage( HttpMethod.Patch, $"/webhooks/{webhookId}" );
        req.Content = JsonContent.Create( webhook );

        return Execute( req, cancellationToken );
    }


    /// <inheritdoc />
    public Task<ResendResponse> WebhookDeleteAsync( Guid webhookId, CancellationToken cancellationToken = default )
    {
        var req = new HttpRequestMessage( HttpMethod.Delete, $"/webhooks/{webhookId}" );

        return Execute( req, cancellationToken );
    }
}