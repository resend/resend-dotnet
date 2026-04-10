using Microsoft.AspNetCore.WebUtilities;
using Resend.Payloads;
using System.Net.Http.Json;

namespace Resend;

public partial class ResendClient
{
    /// <inheritdoc />
    public Task<ResendResponse<Guid>> AutomationCreateAsync( AutomationCreateData data, CancellationToken cancellationToken = default )
    {
        var req = new HttpRequestMessage( HttpMethod.Post, "/automations" );
        req.Content = JsonContent.Create( data );

        return Execute<ObjectId, Guid>( req, ( x ) => x.Id, cancellationToken );
    }


    /// <inheritdoc />
    public Task<ResendResponse<Guid>> AutomationUpdateAsync( Guid automationId, AutomationUpdateData data, CancellationToken cancellationToken = default )
    {
        var req = new HttpRequestMessage( HttpMethod.Patch, $"/automations/{automationId}" );
        req.Content = JsonContent.Create( data );

        return Execute<ObjectId, Guid>( req, ( x ) => x.Id, cancellationToken );
    }


    /// <inheritdoc />
    public Task<ResendResponse<Automation>> AutomationRetrieveAsync( Guid automationId, CancellationToken cancellationToken = default )
    {
        var req = new HttpRequestMessage( HttpMethod.Get, $"/automations/{automationId}" );

        return Execute<Automation, Automation>( req, ( x ) => x, cancellationToken );
    }


    /// <inheritdoc />
    public Task<ResendResponse<PaginatedResult<AutomationSummary>>> AutomationListAsync( AutomationListQuery? query = null, CancellationToken cancellationToken = default )
    {
        var baseUrl = "/automations";
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

            if ( query.Status != null )
                qs.Add( "status", query.Status );

            url = QueryHelpers.AddQueryString( baseUrl, qs );
        }

        var req = new HttpRequestMessage( HttpMethod.Get, url );

        return Execute<PaginatedResult<AutomationSummary>, PaginatedResult<AutomationSummary>>( req, ( x ) => x, cancellationToken );
    }


    /// <inheritdoc />
    public Task<ResendResponse<AutomationStopResult>> AutomationStopAsync( Guid automationId, CancellationToken cancellationToken = default )
    {
        var req = new HttpRequestMessage( HttpMethod.Post, $"/automations/{automationId}/stop" );

        return Execute<AutomationStopResult, AutomationStopResult>( req, ( x ) => x, cancellationToken );
    }


    /// <inheritdoc />
    public Task<ResendResponse<AutomationDeleteResult>> AutomationDeleteAsync( Guid automationId, CancellationToken cancellationToken = default )
    {
        var req = new HttpRequestMessage( HttpMethod.Delete, $"/automations/{automationId}" );

        return Execute<AutomationDeleteResult, AutomationDeleteResult>( req, ( x ) => x, cancellationToken );
    }


    /// <inheritdoc />
    public Task<ResendResponse<PaginatedResult<AutomationRunSummary>>> AutomationRunListAsync( Guid automationId, AutomationRunListQuery? query = null, CancellationToken cancellationToken = default )
    {
        var baseUrl = $"/automations/{automationId}/runs";
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

            if ( query.Status != null )
                qs.Add( "status", query.Status );

            url = QueryHelpers.AddQueryString( baseUrl, qs );
        }

        var req = new HttpRequestMessage( HttpMethod.Get, url );

        return Execute<PaginatedResult<AutomationRunSummary>, PaginatedResult<AutomationRunSummary>>( req, ( x ) => x, cancellationToken );
    }


    /// <inheritdoc />
    public Task<ResendResponse<AutomationRun>> AutomationRunRetrieveAsync( Guid automationId, Guid runId, CancellationToken cancellationToken = default )
    {
        var req = new HttpRequestMessage( HttpMethod.Get, $"/automations/{automationId}/runs/{runId}" );

        return Execute<AutomationRun, AutomationRun>( req, ( x ) => x, cancellationToken );
    }
}
