using Resend;
using System.Text.Json;

var apiKey = Environment.GetEnvironmentVariable( "RESEND_API_KEY" );

if ( apiKey == null )
{
    Console.Error.WriteLine( "Error: RESEND_API_KEY environment variable is not set" );
    return 1;
}

var options = new ResendClientOptions()
{
    ApiToken = apiKey,
};

var resend = ResendClient.Create( options );
var jsonOptions = new JsonSerializerOptions { WriteIndented = true };

try
{
    Console.WriteLine( "==> Creating webhook..." );
    var createResponse = await resend.WebhookAddAsync( new WebhookAddData()
    {
        EndpointUrl = "https://example.com/webhook",
        Events = new List<WebhookEventType>
        {
            WebhookEventType.EmailSent,
            WebhookEventType.EmailDelivered,
            WebhookEventType.EmailBounced
        }
    } );

    Console.WriteLine( JsonSerializer.Serialize( createResponse.Content, jsonOptions ) );
    var webhookId = createResponse.Content.Id;
    Console.WriteLine();

    await Task.Delay( 5000 );

    Console.WriteLine( "==> Getting webhook by ID..." );
    var getResponse = await resend.WebhookRetrieveAsync( webhookId );
    Console.WriteLine( JsonSerializer.Serialize( getResponse.Content, jsonOptions ) );
    Console.WriteLine();

    await Task.Delay( 5000 );

    Console.WriteLine( "==> Updating webhook..." );
    var updateResponse = await resend.WebhookUpdateAsync( webhookId, new WebhookUpdateData()
    {
        EndpointUrl = "https://example.com/webhook-updated",
        Events = new List<WebhookEventType>
        {
            WebhookEventType.EmailSent,
            WebhookEventType.EmailDelivered,
            WebhookEventType.EmailBounced,
            WebhookEventType.EmailOpened,
            WebhookEventType.EmailClicked
        },
        Status = WebhookStatus.Enabled
    } );

    Console.WriteLine( "Update successful" );
    Console.WriteLine();

    await Task.Delay( 5000 );

    Console.WriteLine( "==> Getting updated webhook..." );
    var getUpdatedResponse = await resend.WebhookRetrieveAsync( webhookId );
    Console.WriteLine( JsonSerializer.Serialize( getUpdatedResponse.Content, jsonOptions ) );
    Console.WriteLine();

    await Task.Delay( 5000 );

    Console.WriteLine( "==> Listing all webhooks..." );
    var listResponse = await resend.WebhookListAsync( new PaginatedQuery() { Limit = 10 } );
    Console.WriteLine( JsonSerializer.Serialize( listResponse.Content, jsonOptions ) );
    Console.WriteLine();

    await Task.Delay( 5000 );

    Console.WriteLine( "==> Deleting webhook..." );
    var deleteResponse = await resend.WebhookDeleteAsync( webhookId );
    Console.WriteLine( "Delete successful" );
    Console.WriteLine();

    await Task.Delay( 5000 );

    Console.WriteLine( "==> Verifying deletion by listing webhooks..." );
    var verifyResponse = await resend.WebhookListAsync();
    Console.WriteLine( JsonSerializer.Serialize( verifyResponse.Content, jsonOptions ) );
    Console.WriteLine();

    Console.WriteLine( "Workflow completed successfully!" );
    return 0;
}
catch ( ResendException ex )
{
    Console.Error.WriteLine( $"Error: {ex.ErrorType} - {ex.Message}" );
    if ( ex.InnerException != null )
    {
        Console.Error.WriteLine( $"Inner: {ex.InnerException.Message}" );
    }
    return 1;
}
catch ( Exception ex )
{
    Console.Error.WriteLine( $"Unexpected error: {ex.Message}" );
    return 1;
}
