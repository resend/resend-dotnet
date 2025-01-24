using Microsoft.AspNetCore.Mvc;
using Resend;


/*
 * 
 */
var builder = WebApplication.CreateBuilder( args );

builder.Services.AddOptions();
builder.Services.Configure<ResendClientOptions>( o =>
{
    o.ApiToken = Environment.GetEnvironmentVariable( "RESEND_APITOKEN" )!;
} );
builder.Services.AddHttpClient<ResendClient>();
builder.Services.AddTransient<IResend, ResendClient>();


/*
 * 
 */
var app = builder.Build();

app.MapPost( "/email/send", async ( [FromServices] IResend resend, [FromServices] ILogger<EmailSend> logger ) =>
{
    var message = new EmailMessage();
    message.From = "you@domain.com";
    message.To.Add( "user@gmail.com" );
    message.Subject = "Hello from Minimal API";
    message.TextBody = "Email using Resend .NET SDK";

    var resp = await resend.EmailSendAsync( message );

    logger.LogInformation( "Sent email, with Id = {EmailId}", resp.Content );

    return Results.Ok();
} );


/*
 * 
 */
app.Run();


/*
 * 
 */
public class EmailSend { };
