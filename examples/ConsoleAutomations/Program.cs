using System.Text.Json;
using Resend;


/*
 * Setup
 */
var apiToken = Environment.GetEnvironmentVariable( "RESEND_API_KEY" );

if ( apiToken == null )
{
    Console.Error.WriteLine( "err: Environment variable RESEND_API_KEY is not defined" );
    return;
}

var options = new ResendClientOptions()
{
    ApiToken = apiToken,
};

var resend = ResendClient.Create( options );


/*
 * Helper to build a JsonElement from an anonymous object
 */
static JsonElement ToJson( object value )
{
    var bytes = JsonSerializer.SerializeToUtf8Bytes( value );
    return JsonDocument.Parse( bytes ).RootElement.Clone();
}


/*
 * -------------------------------------------------------------------------
 * EVENTS
 * -------------------------------------------------------------------------
 */

Console.WriteLine( "=== Events ===" );

// Create an event
Console.WriteLine( "\n--- EventCreateAsync ---" );

var eventCreateResp = await resend.EventCreateAsync( new EventCreateData()
{
    Name = "user.signed_up",
    Schema = ToJson( new { plan = "string", trial_days = "number" } ),
} );

var eventId = eventCreateResp.Content;
Console.WriteLine( "Created event id={0}", eventId );


// Retrieve event by id
Console.WriteLine( "\n--- EventRetrieveAsync (by id) ---" );

var eventByIdResp = await resend.EventRetrieveAsync( eventId );
var ev = eventByIdResp.Content;
Console.WriteLine( "Event id={0} name={1} created={2}", ev.Id, ev.Name, ev.MomentCreated );


// Retrieve event by name
Console.WriteLine( "\n--- EventRetrieveAsync (by name) ---" );

var eventByNameResp = await resend.EventRetrieveAsync( "user.signed_up" );
Console.WriteLine( "Event name={0}", eventByNameResp.Content.Name );


// List events
Console.WriteLine( "\n--- EventListAsync ---" );

var eventListResp = await resend.EventListAsync();
Console.WriteLine( "Total events: {0}", eventListResp.Content.Data.Count );
foreach ( var e in eventListResp.Content.Data )
    Console.WriteLine( "  id={0} name={1}", e.Id, e.Name );


// Update event schema
Console.WriteLine( "\n--- EventUpdateAsync (by id) ---" );

await resend.EventUpdateAsync( eventId, new EventUpdateData()
{
    Schema = ToJson( new { plan = "string", trial_days = "number", referral_code = "string" } ),
} );
Console.WriteLine( "Updated event schema" );


// Send event by email
Console.WriteLine( "\n--- EventSendAsync ---" );

var sendResp = await resend.EventSendAsync( new EventSendData()
{
    Event = "user.signed_up",
    Email = "delivered@resend.dev",
    Payload = ToJson( new { plan = "pro", trial_days = 14, referral_code = "LAUNCH" } ),
} );
Console.WriteLine( "Sent event={0}", sendResp.Content.Event );


// Delete event by name
Console.WriteLine( "\n--- EventDeleteAsync (by name) ---" );

await resend.EventDeleteAsync( "user.signed_up" );
Console.WriteLine( "Deleted event by name" );


/*
 * -------------------------------------------------------------------------
 * AUTOMATIONS
 * -------------------------------------------------------------------------
 */

Console.WriteLine( "\n=== Automations ===" );

// Create a template to use in the automation send_email step
Console.WriteLine( "\n--- TemplateCreateAsync ---" );

var templateId = ( await resend.TemplateCreateAsync( new TemplateData()
{
    Name = "Welcome email",
    Subject = "Welcome!",
    HtmlBody = "<p>Welcome to our product!</p>",
} ) ).Content;

Console.WriteLine( "Created template id={0}", templateId );

await resend.TemplatePublishAsync( templateId );
Console.WriteLine( "Published template" );


// Create an automation
Console.WriteLine( "\n--- AutomationCreateAsync ---" );

var automationId = ( await resend.AutomationCreateAsync( new AutomationCreateData()
{
    Name = "Welcome flow",
    Status = "disabled",
    Steps =
    [
        new AutomationStepData()
        {
            Ref = "trigger",
            Type = "trigger",
            Config = ToJson( new { event_name = "user.signed_up" } ),
        },
        new AutomationStepData()
        {
            Ref = "send-welcome-email",
            Type = "send_email",
            Config = ToJson( new { template_id = templateId.ToString() } ),
        },
    ],
    Edges =
    [
        new AutomationEdge() { From = "trigger", To = "send-welcome-email" },
    ],
} ) ).Content;

Console.WriteLine( "Created automation id={0}", automationId );


// Retrieve automation
Console.WriteLine( "\n--- AutomationRetrieveAsync ---" );

var automation = ( await resend.AutomationRetrieveAsync( automationId ) ).Content;
Console.WriteLine( "Automation id={0} name={1} status={2} steps={3}",
    automation.Id, automation.Name, automation.Status, automation.Steps.Count );


// List all automations
Console.WriteLine( "\n--- AutomationListAsync (all) ---" );

var allAutomations = ( await resend.AutomationListAsync() ).Content;
Console.WriteLine( "Total automations: {0}", allAutomations.Data.Count );
foreach ( var a in allAutomations.Data )
    Console.WriteLine( "  id={0} name={1} status={2}", a.Id, a.Name, a.Status );


// List automations filtered by status
Console.WriteLine( "\n--- AutomationListAsync (status=disabled) ---" );

var disabledAutomations = ( await resend.AutomationListAsync( new AutomationListQuery()
{
    Status = "disabled",
} ) ).Content;
Console.WriteLine( "Disabled automations: {0}", disabledAutomations.Data.Count );


// Update automation name
Console.WriteLine( "\n--- AutomationUpdateAsync ---" );

await resend.AutomationUpdateAsync( automationId, new AutomationUpdateData()
{
    Name = "Welcome flow (updated)",
} );
Console.WriteLine( "Updated automation name" );


// List runs (empty at this point, but exercises the endpoint)
Console.WriteLine( "\n--- AutomationRunListAsync (all) ---" );

var runs = ( await resend.AutomationRunListAsync( automationId ) ).Content;
Console.WriteLine( "Total runs: {0}", runs.Data.Count );


// List runs filtered by status
Console.WriteLine( "\n--- AutomationRunListAsync (status=completed) ---" );

var completedRuns = ( await resend.AutomationRunListAsync( automationId, new AutomationRunListQuery()
{
    Status = "completed",
} ) ).Content;
Console.WriteLine( "Completed runs: {0}", completedRuns.Data.Count );


// Stop automation
Console.WriteLine( "\n--- AutomationStopAsync ---" );

var stopResult = ( await resend.AutomationStopAsync( automationId ) ).Content;
Console.WriteLine( "Stopped automation id={0}", stopResult.Id );


// Delete automation
Console.WriteLine( "\n--- AutomationDeleteAsync ---" );

var deleteResult = ( await resend.AutomationDeleteAsync( automationId ) ).Content;
Console.WriteLine( "Deleted automation id={0}", deleteResult.Id );


// Clean up template
Console.WriteLine( "\n--- TemplateDeleteAsync ---" );

await resend.TemplateDeleteAsync( templateId );
Console.WriteLine( "Deleted template id={0}", templateId );

Console.WriteLine( "\nDone." );
