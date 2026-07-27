using Microsoft.AspNetCore.WebUtilities;
using Resend.Payloads;
using System.Net.Http.Json;

namespace Resend;

public partial class ResendClient
{
    /// <inheritdoc />
    public Task<ResendResponse<Guid>> SuppressionAddAsync( string email, CancellationToken cancellationToken = default )
    {
        var req = new HttpRequestMessage( HttpMethod.Post, "/suppressions" );
        req.Content = JsonContent.Create( new SuppressionAddRequest()
        {
            Email = email,
        } );

        return Execute<ObjectId, Guid>( req, ( x ) => x.Id, cancellationToken );
    }


    /// <inheritdoc />
    public Task<ResendResponse<PaginatedResult<SuppressionSummary>>> SuppressionListAsync( SuppressionListQuery? query = null, CancellationToken cancellationToken = default )
    {
        var baseUrl = "/suppressions";
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

            if ( query.Origin.HasValue == true )
                qs.Add( "origin", JsonStringEnumValue<SuppressionOrigin>.Of( query.Origin.Value ) );

            url = QueryHelpers.AddQueryString( baseUrl, qs );
        }

        var req = new HttpRequestMessage( HttpMethod.Get, url );

        return Execute<PaginatedResult<SuppressionSummary>, PaginatedResult<SuppressionSummary>>( req, ( x ) => x, cancellationToken );
    }


    /// <inheritdoc />
    public Task<ResendResponse<Suppression>> SuppressionRetrieveAsync( string suppressionIdOrEmail, CancellationToken cancellationToken = default )
    {
        /*
         * An empty identifier would target the collection endpoint, which answers with a
         * list that deserializes into an all-default Suppression instead of failing.
         */
        ArgumentException.ThrowIfNullOrWhiteSpace( suppressionIdOrEmail );

        var req = new HttpRequestMessage( HttpMethod.Get, $"/suppressions/{Uri.EscapeDataString( suppressionIdOrEmail )}" );

        return Execute<Suppression, Suppression>( req, ( x ) => x, cancellationToken );
    }


    /// <inheritdoc />
    public Task<ResendResponse<SuppressionRemoveResult>> SuppressionRemoveAsync( string suppressionIdOrEmail, CancellationToken cancellationToken = default )
    {
        ArgumentException.ThrowIfNullOrWhiteSpace( suppressionIdOrEmail );

        var req = new HttpRequestMessage( HttpMethod.Delete, $"/suppressions/{Uri.EscapeDataString( suppressionIdOrEmail )}" );

        return Execute<SuppressionRemoveResult, SuppressionRemoveResult>( req, ( x ) => x, cancellationToken );
    }


    /// <inheritdoc />
    public Task<ResendResponse<List<Guid>>> SuppressionBatchAddAsync( IEnumerable<string> emails, CancellationToken cancellationToken = default )
    {
        var req = new HttpRequestMessage( HttpMethod.Post, "/suppressions/batch/add" );
        req.Content = JsonContent.Create( new SuppressionBatchAddRequest()
        {
            Emails = emails.ToList(),
        } );

        return Execute<ListOf<ObjectId>, List<Guid>>( req, ( x ) => x.Data.Select( y => y.Id ).ToList(), cancellationToken );
    }


    /// <inheritdoc />
    public Task<ResendResponse<List<SuppressionRemoveResult>>> SuppressionBatchRemoveAsync( IEnumerable<string> emails, CancellationToken cancellationToken = default )
    {
        return SuppressionBatchRemoveAsync( new SuppressionBatchRemoveRequest()
        {
            Emails = emails.ToList(),
        }, cancellationToken );
    }


    /// <inheritdoc />
    public Task<ResendResponse<List<SuppressionRemoveResult>>> SuppressionBatchRemoveAsync( IEnumerable<Guid> suppressionIds, CancellationToken cancellationToken = default )
    {
        return SuppressionBatchRemoveAsync( new SuppressionBatchRemoveRequest()
        {
            Ids = suppressionIds.ToList(),
        }, cancellationToken );
    }


    /// <summary />
    private Task<ResendResponse<List<SuppressionRemoveResult>>> SuppressionBatchRemoveAsync( SuppressionBatchRemoveRequest data, CancellationToken cancellationToken )
    {
        var req = new HttpRequestMessage( HttpMethod.Post, "/suppressions/batch/remove" );
        req.Content = JsonContent.Create( data );

        return Execute<ListOf<SuppressionRemoveResult>, List<SuppressionRemoveResult>>( req, ( x ) => x.Data, cancellationToken );
    }
}
