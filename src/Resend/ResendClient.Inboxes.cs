using Microsoft.AspNetCore.WebUtilities;
using Resend.Payloads;
using System.Net.Http.Json;

namespace Resend;

public partial class ResendClient
{
    /// <inheritdoc />
    public Task<ResendResponse<Guid>> InboxCreateAsync( InboxCreateData data, CancellationToken cancellationToken = default )
    {
        var req = new HttpRequestMessage( HttpMethod.Post, "/inboxes" );
        req.Content = JsonContent.Create( data );

        return Execute<ObjectId, Guid>( req, ( x ) => x.Id, cancellationToken );
    }


    /// <inheritdoc />
    public Task<ResendResponse<PaginatedResult<InboxSummary>>> InboxListAsync( PaginatedQuery? query = null, CancellationToken cancellationToken = default )
    {
        var url = InboxQueryUrl( "/inboxes", query, null );
        var req = new HttpRequestMessage( HttpMethod.Get, url );

        return Execute<PaginatedResult<InboxSummary>, PaginatedResult<InboxSummary>>( req, ( x ) => x, cancellationToken );
    }


    /// <inheritdoc />
    public Task<ResendResponse<Inbox>> InboxRetrieveAsync( Guid inboxId, CancellationToken cancellationToken = default )
    {
        var req = new HttpRequestMessage( HttpMethod.Get, $"/inboxes/{inboxId}" );

        return Execute<Inbox, Inbox>( req, ( x ) => x, cancellationToken );
    }


    /// <inheritdoc />
    public Task<ResendResponse<Inbox>> InboxRetrieveAsync( string inboxIdOrEmail, CancellationToken cancellationToken = default )
    {
        /*
         * An empty identifier would target the collection endpoint, which answers with a
         * list that deserializes into an all-default Inbox instead of failing.
         */
        ArgumentException.ThrowIfNullOrWhiteSpace( inboxIdOrEmail );

        var req = new HttpRequestMessage( HttpMethod.Get, $"/inboxes/{Uri.EscapeDataString( inboxIdOrEmail )}" );

        return Execute<Inbox, Inbox>( req, ( x ) => x, cancellationToken );
    }


    /// <inheritdoc />
    public Task<ResendResponse<Guid>> InboxUpdateAsync( Guid inboxId, InboxUpdateData data, CancellationToken cancellationToken = default )
    {
        var req = new HttpRequestMessage( HttpMethod.Patch, $"/inboxes/{inboxId}" );
        req.Content = JsonContent.Create( data );

        return Execute<ObjectId, Guid>( req, ( x ) => x.Id, cancellationToken );
    }


    /// <inheritdoc />
    public Task<ResendResponse<InboxDeleteResult>> InboxDeleteAsync( Guid inboxId, CancellationToken cancellationToken = default )
    {
        var req = new HttpRequestMessage( HttpMethod.Delete, $"/inboxes/{inboxId}" );

        return Execute<InboxDeleteResult, InboxDeleteResult>( req, ( x ) => x, cancellationToken );
    }


    /// <inheritdoc />
    public Task<ResendResponse<InboxAgent>> InboxAgentRetrieveAsync( Guid inboxId, CancellationToken cancellationToken = default )
    {
        var req = new HttpRequestMessage( HttpMethod.Get, $"/inboxes/{inboxId}/agent" );

        return Execute<InboxAgent, InboxAgent>( req, ( x ) => x, cancellationToken );
    }


    /// <inheritdoc />
    public Task<ResendResponse<Guid>> InboxAgentUpdateAsync( Guid inboxId, InboxAgentUpdateData data, CancellationToken cancellationToken = default )
    {
        var req = new HttpRequestMessage( HttpMethod.Patch, $"/inboxes/{inboxId}/agent" );
        req.Content = JsonContent.Create( data );

        return Execute<ObjectId, Guid>( req, ( x ) => x.Id, cancellationToken );
    }


    /// <inheritdoc />
    public Task<ResendResponse<List<InboxLabel>>> InboxLabelListAsync( Guid inboxId, CancellationToken cancellationToken = default )
    {
        var req = new HttpRequestMessage( HttpMethod.Get, $"/inboxes/{inboxId}/labels" );

        return Execute<ListOf<InboxLabel>, List<InboxLabel>>( req, ( x ) => x.Data, cancellationToken );
    }


    /// <inheritdoc />
    public Task<ResendResponse<Guid>> InboxLabelCreateAsync( Guid inboxId, InboxLabelCreateData data, CancellationToken cancellationToken = default )
    {
        var req = new HttpRequestMessage( HttpMethod.Post, $"/inboxes/{inboxId}/labels" );
        req.Content = JsonContent.Create( data );

        return Execute<ObjectId, Guid>( req, ( x ) => x.Id, cancellationToken );
    }


    /// <inheritdoc />
    public Task<ResendResponse<Guid>> InboxLabelUpdateAsync( Guid inboxId, Guid labelId, InboxLabelUpdateData data, CancellationToken cancellationToken = default )
    {
        var req = new HttpRequestMessage( HttpMethod.Patch, $"/inboxes/{inboxId}/labels/{labelId}" );
        req.Content = JsonContent.Create( data );

        return Execute<ObjectId, Guid>( req, ( x ) => x.Id, cancellationToken );
    }


    /// <inheritdoc />
    public Task<ResendResponse<InboxLabelDeleteResult>> InboxLabelDeleteAsync( Guid inboxId, Guid labelId, CancellationToken cancellationToken = default )
    {
        var req = new HttpRequestMessage( HttpMethod.Delete, $"/inboxes/{inboxId}/labels/{labelId}" );

        return Execute<InboxLabelDeleteResult, InboxLabelDeleteResult>( req, ( x ) => x, cancellationToken );
    }


    /// <inheritdoc />
    public Task<ResendResponse<PaginatedResult<InboxThread>>> InboxThreadListAsync( Guid inboxId, InboxThreadListQuery? query = null, CancellationToken cancellationToken = default )
    {
        var qs = new Dictionary<string, string?>();

        if ( query != null )
            AddThreadFilters( qs, query );

        var url = InboxQueryUrl( $"/inboxes/{inboxId}/threads", query, qs );
        var req = new HttpRequestMessage( HttpMethod.Get, url );

        return Execute<PaginatedResult<InboxThread>, PaginatedResult<InboxThread>>( req, ( x ) => x, cancellationToken );
    }


    /// <inheritdoc />
    public Task<ResendResponse<PaginatedResult<InboxThreadSearchResult>>> InboxThreadSearchAsync( Guid inboxId, InboxThreadSearchQuery? query = null, CancellationToken cancellationToken = default )
    {
        var qs = new Dictionary<string, string?>();

        if ( query != null )
        {
            if ( query.Query != null )
                qs.Add( "query", query.Query );

            if ( query.From != null )
                qs.Add( "from", string.Join( ",", query.From ) );

            if ( query.To != null )
                qs.Add( "to", string.Join( ",", query.To ) );

            if ( query.Cc != null )
                qs.Add( "cc", string.Join( ",", query.Cc ) );

            if ( query.Bcc != null )
                qs.Add( "bcc", string.Join( ",", query.Bcc ) );

            if ( query.HasAttachment.HasValue == true )
                qs.Add( "has_attachment", ToQueryValue( query.HasAttachment.Value ) );

            if ( query.StartDate != null )
                qs.Add( "start_date", query.StartDate );

            if ( query.EndDate != null )
                qs.Add( "end_date", query.EndDate );

            AddThreadFilters( qs, query );
        }

        var url = InboxQueryUrl( $"/inboxes/{inboxId}/threads/search", query, qs );
        var req = new HttpRequestMessage( HttpMethod.Get, url );

        return Execute<PaginatedResult<InboxThreadSearchResult>, PaginatedResult<InboxThreadSearchResult>>( req, ( x ) => x, cancellationToken );
    }


    /// <inheritdoc />
    public Task<ResendResponse<InboxThreadSummary>> InboxThreadRetrieveAsync( Guid inboxId, Guid threadId, CancellationToken cancellationToken = default )
    {
        var req = new HttpRequestMessage( HttpMethod.Get, $"/inboxes/{inboxId}/threads/{threadId}" );

        return Execute<InboxThreadSummary, InboxThreadSummary>( req, ( x ) => x, cancellationToken );
    }


    /// <inheritdoc />
    public Task<ResendResponse<InboxThreadSummary>> InboxThreadUpdateAsync( Guid inboxId, Guid threadId, InboxThreadUpdateData data, CancellationToken cancellationToken = default )
    {
        var req = new HttpRequestMessage( HttpMethod.Patch, $"/inboxes/{inboxId}/threads/{threadId}" );
        req.Content = JsonContent.Create( data );

        return Execute<InboxThreadSummary, InboxThreadSummary>( req, ( x ) => x, cancellationToken );
    }


    /// <inheritdoc />
    public Task<ResendResponse<InboxThreadDeleteResult>> InboxThreadDeleteAsync( Guid inboxId, Guid threadId, CancellationToken cancellationToken = default )
    {
        var req = new HttpRequestMessage( HttpMethod.Delete, $"/inboxes/{inboxId}/threads/{threadId}" );

        return Execute<InboxThreadDeleteResult, InboxThreadDeleteResult>( req, ( x ) => x, cancellationToken );
    }


    /// <inheritdoc />
    public Task<ResendResponse<PaginatedResult<InboxThreadEmail>>> InboxThreadEmailListAsync( Guid inboxId, Guid threadId, PaginatedQuery? query = null, CancellationToken cancellationToken = default )
    {
        var url = InboxQueryUrl( $"/inboxes/{inboxId}/threads/{threadId}/emails", query, null );
        var req = new HttpRequestMessage( HttpMethod.Get, url );

        return Execute<PaginatedResult<InboxThreadEmail>, PaginatedResult<InboxThreadEmail>>( req, ( x ) => x, cancellationToken );
    }


    /// <inheritdoc />
    public Task<ResendResponse<InboxThreadEmail>> InboxThreadEmailRetrieveAsync( Guid inboxId, Guid threadId, Guid emailId, CancellationToken cancellationToken = default )
    {
        var req = new HttpRequestMessage( HttpMethod.Get, $"/inboxes/{inboxId}/threads/{threadId}/emails/{emailId}" );

        return Execute<InboxThreadEmail, InboxThreadEmail>( req, ( x ) => x, cancellationToken );
    }


    /// <inheritdoc />
    public Task<ResendResponse<Guid>> InboxThreadEmailReplyAsync( Guid inboxId, Guid threadId, Guid emailId, InboxThreadEmailReplyData data, CancellationToken cancellationToken = default )
    {
        var req = new HttpRequestMessage( HttpMethod.Post, $"/inboxes/{inboxId}/threads/{threadId}/emails/{emailId}/reply" );
        req.Content = JsonContent.Create( data );

        return Execute<ObjectId, Guid>( req, ( x ) => x.Id, cancellationToken );
    }


    /// <inheritdoc />
    public Task<ResendResponse<Guid>> InboxThreadEmailReplyAsync( string idempotencyKey, Guid inboxId, Guid threadId, Guid emailId, InboxThreadEmailReplyData data, CancellationToken cancellationToken = default )
    {
        var req = new HttpRequestMessage( HttpMethod.Post, $"/inboxes/{inboxId}/threads/{threadId}/emails/{emailId}/reply" );
        req.Content = JsonContent.Create( data );
        req.Headers.Add( IdempotencyKey, idempotencyKey );

        return Execute<ObjectId, Guid>( req, ( x ) => x.Id, cancellationToken );
    }


    /// <inheritdoc />
    public Task<ResendResponse<Guid>> InboxThreadEmailForwardAsync( Guid inboxId, Guid threadId, Guid emailId, InboxThreadEmailForwardData data, CancellationToken cancellationToken = default )
    {
        var req = new HttpRequestMessage( HttpMethod.Post, $"/inboxes/{inboxId}/threads/{threadId}/emails/{emailId}/forward" );
        req.Content = JsonContent.Create( data );

        return Execute<ObjectId, Guid>( req, ( x ) => x.Id, cancellationToken );
    }


    /// <inheritdoc />
    public Task<ResendResponse<Guid>> InboxThreadEmailForwardAsync( string idempotencyKey, Guid inboxId, Guid threadId, Guid emailId, InboxThreadEmailForwardData data, CancellationToken cancellationToken = default )
    {
        var req = new HttpRequestMessage( HttpMethod.Post, $"/inboxes/{inboxId}/threads/{threadId}/emails/{emailId}/forward" );
        req.Content = JsonContent.Create( data );
        req.Headers.Add( IdempotencyKey, idempotencyKey );

        return Execute<ObjectId, Guid>( req, ( x ) => x.Id, cancellationToken );
    }


    /// <inheritdoc />
    public Task<ResendResponse<PaginatedResult<InboxDraftSummary>>> InboxDraftListAsync( Guid inboxId, PaginatedQuery? query = null, CancellationToken cancellationToken = default )
    {
        var url = InboxQueryUrl( $"/inboxes/{inboxId}/drafts", query, null );
        var req = new HttpRequestMessage( HttpMethod.Get, url );

        return Execute<PaginatedResult<InboxDraftSummary>, PaginatedResult<InboxDraftSummary>>( req, ( x ) => x, cancellationToken );
    }


    /// <inheritdoc />
    public Task<ResendResponse<Guid>> InboxDraftCreateAsync( Guid inboxId, InboxDraftCreateData data, CancellationToken cancellationToken = default )
    {
        var req = new HttpRequestMessage( HttpMethod.Post, $"/inboxes/{inboxId}/drafts" );
        req.Content = JsonContent.Create( data );

        return Execute<ObjectId, Guid>( req, ( x ) => x.Id, cancellationToken );
    }


    /// <inheritdoc />
    public Task<ResendResponse<Guid>> InboxDraftCreateAsync( string idempotencyKey, Guid inboxId, InboxDraftCreateData data, CancellationToken cancellationToken = default )
    {
        var req = new HttpRequestMessage( HttpMethod.Post, $"/inboxes/{inboxId}/drafts" );
        req.Content = JsonContent.Create( data );
        req.Headers.Add( IdempotencyKey, idempotencyKey );

        return Execute<ObjectId, Guid>( req, ( x ) => x.Id, cancellationToken );
    }


    /// <inheritdoc />
    public Task<ResendResponse<InboxDraft>> InboxDraftRetrieveAsync( Guid inboxId, Guid draftId, CancellationToken cancellationToken = default )
    {
        var req = new HttpRequestMessage( HttpMethod.Get, $"/inboxes/{inboxId}/drafts/{draftId}" );

        return Execute<InboxDraft, InboxDraft>( req, ( x ) => x, cancellationToken );
    }


    /// <inheritdoc />
    public Task<ResendResponse<InboxDraft>> InboxDraftUpdateAsync( Guid inboxId, Guid draftId, InboxDraftUpdateData data, CancellationToken cancellationToken = default )
    {
        var req = new HttpRequestMessage( HttpMethod.Patch, $"/inboxes/{inboxId}/drafts/{draftId}" );
        req.Content = JsonContent.Create( data );

        return Execute<InboxDraft, InboxDraft>( req, ( x ) => x, cancellationToken );
    }


    /// <inheritdoc />
    public Task<ResendResponse<InboxDraftDeleteResult>> InboxDraftDeleteAsync( Guid inboxId, Guid draftId, CancellationToken cancellationToken = default )
    {
        var req = new HttpRequestMessage( HttpMethod.Delete, $"/inboxes/{inboxId}/drafts/{draftId}" );

        return Execute<InboxDraftDeleteResult, InboxDraftDeleteResult>( req, ( x ) => x, cancellationToken );
    }


    /// <inheritdoc />
    public Task<ResendResponse<Guid>> InboxDraftSendAsync( Guid inboxId, Guid draftId, InboxDraftSendData? data = null, CancellationToken cancellationToken = default )
    {
        var req = new HttpRequestMessage( HttpMethod.Post, $"/inboxes/{inboxId}/drafts/{draftId}/send" );

        if ( data != null )
            req.Content = JsonContent.Create( data );

        return Execute<ObjectId, Guid>( req, ( x ) => x.Id, cancellationToken );
    }


    /// <inheritdoc />
    public Task<ResendResponse<Guid>> InboxDraftSendAsync( string idempotencyKey, Guid inboxId, Guid draftId, InboxDraftSendData? data = null, CancellationToken cancellationToken = default )
    {
        var req = new HttpRequestMessage( HttpMethod.Post, $"/inboxes/{inboxId}/drafts/{draftId}/send" );

        if ( data != null )
            req.Content = JsonContent.Create( data );

        req.Headers.Add( IdempotencyKey, idempotencyKey );

        return Execute<ObjectId, Guid>( req, ( x ) => x.Id, cancellationToken );
    }


    /// <summary />
    private static void AddThreadFilters( Dictionary<string, string?> qs, InboxThreadListQuery query )
    {
        if ( query.Folders != null )
            qs.Add( "folders", string.Join( ",", query.Folders.Select( x => JsonStringEnumValue<InboxFolder>.Of( x ) ) ) );

        if ( query.Labels != null )
            qs.Add( "labels", string.Join( ",", query.Labels ) );

        if ( query.IsRead.HasValue == true )
            qs.Add( "read", ToQueryValue( query.IsRead.Value ) );
    }


    /// <summary />
    private static string InboxQueryUrl( string baseUrl, PaginatedQuery? query, Dictionary<string, string?>? qs )
    {
        qs ??= new Dictionary<string, string?>();

        if ( query != null )
        {
            if ( query.Limit.HasValue == true )
                qs.Add( "limit", query.Limit.Value.ToString() );

            if ( query.Before != null )
                qs.Add( "before", query.Before );

            if ( query.After != null )
                qs.Add( "after", query.After );
        }

        if ( qs.Count == 0 )
            return baseUrl;

        return QueryHelpers.AddQueryString( baseUrl, qs );
    }


    /// <summary>
    /// The API accepts only the literal values <c>true</c> and <c>false</c>, while
    /// <see cref="bool.ToString()"/> yields <c>True</c> and <c>False</c>.
    /// </summary>
    private static string ToQueryValue( bool value )
    {
        return value == true ? "true" : "false";
    }
}
