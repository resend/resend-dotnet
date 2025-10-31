using Microsoft.AspNetCore.WebUtilities;
using Resend.Payloads;
using System.Net.Http.Json;

namespace Resend;

public partial class ResendClient
{
    /// <inheritdoc />
    public Task<ResendResponse<PaginatedResult<TemplateSummary>>> TemplateListAsync( PaginatedQuery? query = null, CancellationToken cancellationToken = default )
    {
        var baseUrl = "/templates";
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

        return Execute<PaginatedResult<TemplateSummary>, PaginatedResult<TemplateSummary>>( req, ( x ) => x, cancellationToken );
    }


    /// <inheritdoc />
    public Task<ResendResponse<Guid>> TemplateCreateAsync( TemplateData template, CancellationToken cancellationToken = default )
    {
        var req = new HttpRequestMessage( HttpMethod.Post, "/templates" );
        req.Content = JsonContent.Create( template );

        return Execute<ObjectId, Guid>( req, ( x ) => x.Id, cancellationToken );
    }


    /// <inheritdoc />
    public Task<ResendResponse<Template>> TemplateRetrieveAsync( Guid templateId, CancellationToken cancellationToken = default )
    {
        var path = $"/templates/{templateId}";
        var req = new HttpRequestMessage( HttpMethod.Get, path );

        return Execute<Template, Template>( req, ( x ) => x, cancellationToken );
    }


    /// <inheritdoc />
    public Task<ResendResponse<Template>> TemplateRetrieveAsync( string templateAlias, CancellationToken cancellationToken = default )
    {
        var path = $"/templates/{templateAlias}";
        var req = new HttpRequestMessage( HttpMethod.Get, path );

        return Execute<Template, Template>( req, ( x ) => x, cancellationToken );
    }


    /// <inheritdoc />
    public Task<ResendResponse> TemplateUpdateAsync( Guid templateId, TemplateData template, CancellationToken cancellationToken = default )
    {
        var req = new HttpRequestMessage( HttpMethod.Patch, $"/templates/{templateId}" );
        req.Content = JsonContent.Create( template );

        return Execute( req, cancellationToken );
    }


    /// <inheritdoc />
    public Task<ResendResponse> TemplateUpdateAsync( string templateAlias, TemplateData template, CancellationToken cancellationToken = default )
    {
        var req = new HttpRequestMessage( HttpMethod.Patch, $"/templates/{templateAlias}" );
        req.Content = JsonContent.Create( template );

        return Execute( req, cancellationToken );
    }


    /// <inheritdoc />
    public Task<ResendResponse> TemplateDeleteAsync( Guid templateId, CancellationToken cancellationToken = default )
    {
        var req = new HttpRequestMessage( HttpMethod.Delete, $"/templates/{templateId}" );

        return Execute( req, cancellationToken );
    }


    /// <inheritdoc />
    public Task<ResendResponse> TemplateDeleteAsync( string templateAlias, CancellationToken cancellationToken = default )
    {
        var req = new HttpRequestMessage( HttpMethod.Delete, $"/templates/{templateAlias}" );

        return Execute( req, cancellationToken );
    }


    /// <inheritdoc />
    public Task<ResendResponse> TemplatePublishAsync( Guid templateId, CancellationToken cancellationToken = default )
    {
        var req = new HttpRequestMessage( HttpMethod.Post, $"/templates/{templateId}/publish" );

        return Execute( req, cancellationToken );
    }


    /// <inheritdoc />
    public Task<ResendResponse> TemplatePublishAsync( string templateAlias, CancellationToken cancellationToken = default )
    {
        var req = new HttpRequestMessage( HttpMethod.Post, $"/templates/{templateAlias}/publish" );

        return Execute( req, cancellationToken );
    }


    /// <inheritdoc />
    public Task<ResendResponse<Guid>> TemplateDuplicateAsync( Guid templateId, CancellationToken cancellationToken = default )
    {
        var req = new HttpRequestMessage( HttpMethod.Post, $"/templates/{templateId}/duplicate" );

        return Execute<ObjectId, Guid>( req, x => x.Id, cancellationToken );
    }


    /// <inheritdoc />
    public Task<ResendResponse<Guid>> TemplateDuplicateAsync( string templateAlias, CancellationToken cancellationToken = default )
    {
        var req = new HttpRequestMessage( HttpMethod.Post, $"/templates/{templateAlias}/duplicate" );

        return Execute<ObjectId, Guid>( req, x => x.Id, cancellationToken );
    }
}