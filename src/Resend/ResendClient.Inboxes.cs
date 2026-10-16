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
    public Task<ResendResponse<Inbox>> InboxRetrieveAsync( InboxIdOrEmail inboxId, CancellationToken cancellationToken = default )
    {
        var req = new HttpRequestMessage( HttpMethod.Get, inboxId.Path );

        return Execute<Inbox, Inbox>( req, ( x ) => x, cancellationToken );
    }


    /// <inheritdoc />
    public Task<ResendResponse<Guid>> InboxUpdateAsync( InboxIdOrEmail inboxId, InboxUpdateData data, CancellationToken cancellationToken = default )
    {
        var req = new HttpRequestMessage( HttpMethod.Patch, inboxId.Path );
        req.Content = JsonContent.Create( data );

        return Execute<ObjectId, Guid>( req, ( x ) => x.Id, cancellationToken );
    }


    /// <inheritdoc />
    public Task<ResendResponse<InboxDeleteResult>> InboxDeleteAsync( InboxIdOrEmail inboxId, CancellationToken cancellationToken = default )
    {
        var req = new HttpRequestMessage( HttpMethod.Delete, inboxId.Path );

        return Execute<InboxDeleteResult, InboxDeleteResult>( req, ( x ) => x, cancellationToken );
    }


    /// <inheritdoc />
    public Task<ResendResponse<InboxAgent>> InboxAgentRetrieveAsync( InboxIdOrEmail inboxId, CancellationToken cancellationToken = default )
    {
        var req = new HttpRequestMessage( HttpMethod.Get, $"{inboxId.Path}/agent" );

        return Execute<InboxAgent, InboxAgent>( req, ( x ) => x, cancellationToken );
    }


    /// <inheritdoc />
    public Task<ResendResponse<Guid>> InboxAgentUpdateAsync( InboxIdOrEmail inboxId, InboxAgentUpdateData data, CancellationToken cancellationToken = default )
    {
        var req = new HttpRequestMessage( HttpMethod.Patch, $"{inboxId.Path}/agent" );
        req.Content = JsonContent.Create( data );

        return Execute<ObjectId, Guid>( req, ( x ) => x.Id, cancellationToken );
    }


    /// <inheritdoc />
    public Task<ResendResponse<List<InboxLabel>>> InboxLabelListAsync( InboxIdOrEmail inboxId, CancellationToken cancellationToken = default )
    {
        var req = new HttpRequestMessage( HttpMethod.Get, $"{inboxId.Path}/labels" );

        return Execute<ListOf<InboxLabel>, List<InboxLabel>>( req, ( x ) => x.Data, cancellationToken );
    }


    /// <inheritdoc />
    public Task<ResendResponse<Guid>> InboxLabelCreateAsync( InboxIdOrEmail inboxId, InboxLabelCreateData data, CancellationToken cancellationToken = default )
    {
        var req = new HttpRequestMessage( HttpMethod.Post, $"{inboxId.Path}/labels" );
        req.Content = JsonContent.Create( data );

        return Execute<ObjectId, Guid>( req, ( x ) => x.Id, cancellationToken );
    }


    /// <inheritdoc />
    public Task<ResendResponse<Guid>> InboxLabelUpdateAsync( InboxIdOrEmail inboxId, Guid labelId, InboxLabelUpdateData data, CancellationToken cancellationToken = default )
    {
        var req = new HttpRequestMessage( HttpMethod.Patch, $"{inboxId.Path}/labels/{labelId}" );
        req.Content = JsonContent.Create( data );

        return Execute<ObjectId, Guid>( req, ( x ) => x.Id, cancellationToken );
    }


    /// <inheritdoc />
    public Task<ResendResponse<InboxLabelDeleteResult>> InboxLabelDeleteAsync( InboxIdOrEmail inboxId, Guid labelId, CancellationToken cancellationToken = default )
    {
        var req = new HttpRequestMessage( HttpMethod.Delete, $"{inboxId.Path}/labels/{labelId}" );

        return Execute<InboxLabelDeleteResult, InboxLabelDeleteResult>( req, ( x ) => x, cancellationToken );
    }


    /// <inheritdoc />
    public Task<ResendResponse<PaginatedResult<InboxThread>>> InboxThreadListAsync( InboxIdOrEmail inboxId, InboxThreadListQuery? query = null, CancellationToken cancellationToken = default )
    {
        var qs = new Dictionary<string, string?>();

        if ( query != null )
            AddThreadFilters( qs, query );

        var url = InboxQueryUrl( $"{inboxId.Path}/threads", query, qs );
        var req = new HttpRequestMessage( HttpMethod.Get, url );

        return Execute<PaginatedResult<InboxThread>, PaginatedResult<InboxThread>>( req, ( x ) => x, cancellationToken );
    }


    /// <inheritdoc />
    public Task<ResendResponse<PaginatedResult<InboxThreadSearchResult>>> InboxThreadSearchAsync( InboxIdOrEmail inboxId, InboxThreadSearchQuery? query = null, CancellationToken cancellationToken = default )
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

        var url = InboxQueryUrl( $"{inboxId.Path}/threads/search", query, qs );
        var req = new HttpRequestMessage( HttpMethod.Get, url );

        return Execute<PaginatedResult<InboxThreadSearchResult>, PaginatedResult<InboxThreadSearchResult>>( req, ( x ) => x, cancellationToken );
    }


    /// <inheritdoc />
    public Task<ResendResponse<InboxThreadSummary>> InboxThreadRetrieveAsync( InboxIdOrEmail inboxId, Guid threadId, CancellationToken cancellationToken = default )
    {
        var req = new HttpRequestMessage( HttpMethod.Get, $"{inboxId.Path}/threads/{threadId}" );

        return Execute<InboxThreadSummary, InboxThreadSummary>( req, ( x ) => x, cancellationToken );
    }


    /// <inheritdoc />
    public Task<ResendResponse<InboxThreadSummary>> InboxThreadUpdateAsync( InboxIdOrEmail inboxId, Guid threadId, InboxThreadUpdateData data, CancellationToken cancellationToken = default )
    {
        var req = new HttpRequestMessage( HttpMethod.Patch, $"{inboxId.Path}/threads/{threadId}" );
        req.Content = JsonContent.Create( data );

        return Execute<InboxThreadSummary, InboxThreadSummary>( req, ( x ) => x, cancellationToken );
    }


    /// <inheritdoc />
    public Task<ResendResponse<InboxThreadDeleteResult>> InboxThreadDeleteAsync( InboxIdOrEmail inboxId, Guid threadId, CancellationToken cancellationToken = default )
    {
        var req = new HttpRequestMessage( HttpMethod.Delete, $"{inboxId.Path}/threads/{threadId}" );

        return Execute<InboxThreadDeleteResult, InboxThreadDeleteResult>( req, ( x ) => x, cancellationToken );
    }


    /// <inheritdoc />
    public Task<ResendResponse<List<InboxThreadLabel>>> InboxThreadLabelAddAsync( InboxIdOrEmail inboxId, Guid threadId, List<Guid> labelIds, CancellationToken cancellationToken = default )
    {
        var req = new HttpRequestMessage( HttpMethod.Post, $"{inboxId.Path}/threads/{threadId}/labels" );
        req.Content = JsonContent.Create( new InboxThreadLabelsRequest() { Labels = labelIds } );

        return Execute<ListOf<InboxThreadLabel>, List<InboxThreadLabel>>( req, ( x ) => x.Data, cancellationToken );
    }


    /// <inheritdoc />
    public Task<ResendResponse<List<InboxThreadLabel>>> InboxThreadLabelSetAsync( InboxIdOrEmail inboxId, Guid threadId, List<Guid> labelIds, CancellationToken cancellationToken = default )
    {
        var req = new HttpRequestMessage( HttpMethod.Put, $"{inboxId.Path}/threads/{threadId}/labels" );
        req.Content = JsonContent.Create( new InboxThreadLabelsRequest() { Labels = labelIds } );

        return Execute<ListOf<InboxThreadLabel>, List<InboxThreadLabel>>( req, ( x ) => x.Data, cancellationToken );
    }


    /// <inheritdoc />
    public Task<ResendResponse<List<InboxThreadLabel>>> InboxThreadLabelRemoveAsync( InboxIdOrEmail inboxId, Guid threadId, Guid labelId, CancellationToken cancellationToken = default )
    {
        var req = new HttpRequestMessage( HttpMethod.Delete, $"{inboxId.Path}/threads/{threadId}/labels/{labelId}" );

        return Execute<ListOf<InboxThreadLabel>, List<InboxThreadLabel>>( req, ( x ) => x.Data, cancellationToken );
    }


    /// <inheritdoc />
    public Task<ResendResponse<PaginatedResult<InboxThreadEmail>>> InboxThreadEmailListAsync( InboxIdOrEmail inboxId, Guid threadId, PaginatedQuery? query = null, CancellationToken cancellationToken = default )
    {
        var url = InboxQueryUrl( $"{inboxId.Path}/threads/{threadId}/emails", query, null );
        var req = new HttpRequestMessage( HttpMethod.Get, url );

        return Execute<PaginatedResult<InboxThreadEmail>, PaginatedResult<InboxThreadEmail>>( req, ( x ) => x, cancellationToken );
    }


    /// <inheritdoc />
    public Task<ResendResponse<InboxThreadEmail>> InboxThreadEmailRetrieveAsync( InboxIdOrEmail inboxId, Guid threadId, Guid emailId, CancellationToken cancellationToken = default )
    {
        var req = new HttpRequestMessage( HttpMethod.Get, $"{inboxId.Path}/threads/{threadId}/emails/{emailId}" );

        return Execute<InboxThreadEmail, InboxThreadEmail>( req, ( x ) => x, cancellationToken );
    }


    /// <inheritdoc />
    public Task<ResendResponse<Guid>> InboxThreadEmailReplyAsync( InboxIdOrEmail inboxId, Guid threadId, Guid emailId, InboxThreadEmailReplyData data, CancellationToken cancellationToken = default )
    {
        var req = new HttpRequestMessage( HttpMethod.Post, $"{inboxId.Path}/threads/{threadId}/emails/{emailId}/reply" );
        req.Content = JsonContent.Create( data );

        return Execute<ObjectId, Guid>( req, ( x ) => x.Id, cancellationToken );
    }


    /// <inheritdoc />
    public Task<ResendResponse<Guid>> InboxThreadEmailReplyAsync( string idempotencyKey, InboxIdOrEmail inboxId, Guid threadId, Guid emailId, InboxThreadEmailReplyData data, CancellationToken cancellationToken = default )
    {
        var req = new HttpRequestMessage( HttpMethod.Post, $"{inboxId.Path}/threads/{threadId}/emails/{emailId}/reply" );
        req.Content = JsonContent.Create( data );
        req.Headers.Add( IdempotencyKey, idempotencyKey );

        return Execute<ObjectId, Guid>( req, ( x ) => x.Id, cancellationToken );
    }


    /// <inheritdoc />
    public Task<ResendResponse<Guid>> InboxThreadEmailForwardAsync( InboxIdOrEmail inboxId, Guid threadId, Guid emailId, InboxThreadEmailForwardData data, CancellationToken cancellationToken = default )
    {
        var req = new HttpRequestMessage( HttpMethod.Post, $"{inboxId.Path}/threads/{threadId}/emails/{emailId}/forward" );
        req.Content = JsonContent.Create( data );

        return Execute<ObjectId, Guid>( req, ( x ) => x.Id, cancellationToken );
    }


    /// <inheritdoc />
    public Task<ResendResponse<Guid>> InboxThreadEmailForwardAsync( string idempotencyKey, InboxIdOrEmail inboxId, Guid threadId, Guid emailId, InboxThreadEmailForwardData data, CancellationToken cancellationToken = default )
    {
        var req = new HttpRequestMessage( HttpMethod.Post, $"{inboxId.Path}/threads/{threadId}/emails/{emailId}/forward" );
        req.Content = JsonContent.Create( data );
        req.Headers.Add( IdempotencyKey, idempotencyKey );

        return Execute<ObjectId, Guid>( req, ( x ) => x.Id, cancellationToken );
    }


    /// <inheritdoc />
    public Task<ResendResponse<PaginatedResult<InboxDraftSummary>>> InboxDraftListAsync( InboxIdOrEmail inboxId, PaginatedQuery? query = null, CancellationToken cancellationToken = default )
    {
        var url = InboxQueryUrl( $"{inboxId.Path}/drafts", query, null );
        var req = new HttpRequestMessage( HttpMethod.Get, url );

        return Execute<PaginatedResult<InboxDraftSummary>, PaginatedResult<InboxDraftSummary>>( req, ( x ) => x, cancellationToken );
    }


    /// <inheritdoc />
    public Task<ResendResponse<Guid>> InboxDraftCreateAsync( InboxIdOrEmail inboxId, InboxDraftCreateData data, CancellationToken cancellationToken = default )
    {
        var req = new HttpRequestMessage( HttpMethod.Post, $"{inboxId.Path}/drafts" );
        req.Content = JsonContent.Create( data );

        return Execute<ObjectId, Guid>( req, ( x ) => x.Id, cancellationToken );
    }


    /// <inheritdoc />
    public Task<ResendResponse<Guid>> InboxDraftCreateAsync( string idempotencyKey, InboxIdOrEmail inboxId, InboxDraftCreateData data, CancellationToken cancellationToken = default )
    {
        var req = new HttpRequestMessage( HttpMethod.Post, $"{inboxId.Path}/drafts" );
        req.Content = JsonContent.Create( data );
        req.Headers.Add( IdempotencyKey, idempotencyKey );

        return Execute<ObjectId, Guid>( req, ( x ) => x.Id, cancellationToken );
    }


    /// <inheritdoc />
    public Task<ResendResponse<InboxDraft>> InboxDraftRetrieveAsync( InboxIdOrEmail inboxId, Guid draftId, CancellationToken cancellationToken = default )
    {
        var req = new HttpRequestMessage( HttpMethod.Get, $"{inboxId.Path}/drafts/{draftId}" );

        return Execute<InboxDraft, InboxDraft>( req, ( x ) => x, cancellationToken );
    }


    /// <inheritdoc />
    public Task<ResendResponse<Guid>> InboxDraftUpdateAsync( InboxIdOrEmail inboxId, Guid draftId, InboxDraftUpdateData data, CancellationToken cancellationToken = default )
    {
        var req = new HttpRequestMessage( HttpMethod.Patch, $"{inboxId.Path}/drafts/{draftId}" );
        req.Content = JsonContent.Create( data );

        return Execute<ObjectId, Guid>( req, ( x ) => x.Id, cancellationToken );
    }


    /// <inheritdoc />
    public Task<ResendResponse<InboxDraftDeleteResult>> InboxDraftDeleteAsync( InboxIdOrEmail inboxId, Guid draftId, CancellationToken cancellationToken = default )
    {
        var req = new HttpRequestMessage( HttpMethod.Delete, $"{inboxId.Path}/drafts/{draftId}" );

        return Execute<InboxDraftDeleteResult, InboxDraftDeleteResult>( req, ( x ) => x, cancellationToken );
    }


    /// <inheritdoc />
    public Task<ResendResponse<Guid>> InboxDraftSendAsync( InboxIdOrEmail inboxId, Guid draftId, InboxDraftSendData? data = null, CancellationToken cancellationToken = default )
    {
        var req = new HttpRequestMessage( HttpMethod.Post, $"{inboxId.Path}/drafts/{draftId}/send" );

        if ( data != null )
            req.Content = JsonContent.Create( data );

        return Execute<ObjectId, Guid>( req, ( x ) => x.Id, cancellationToken );
    }


    /// <inheritdoc />
    public Task<ResendResponse<Guid>> InboxDraftSendAsync( string idempotencyKey, InboxIdOrEmail inboxId, Guid draftId, InboxDraftSendData? data = null, CancellationToken cancellationToken = default )
    {
        var req = new HttpRequestMessage( HttpMethod.Post, $"{inboxId.Path}/drafts/{draftId}/send" );

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
