using Microsoft.Extensions.Options;
using Resend;


/*
 * 
 */
var apiToken = Environment.GetEnvironmentVariable( "RESEND_APITOKEN" );

if ( apiToken == null )
{
    Console.Error.WriteLine( "err: Environnment variable RESEND_APITOKEN is not defined" );
    return;
}


/*
 * 
 */
var options = new ResendClientOptions()
{
    ApiToken = apiToken,
};

var resend = ResendClient.Create( options );


/*
 * 
 */
try
{
    var resp = await resend.EmailSendAsync( new EmailMessage()
    {
        From = "onboarding@resend.dev",
        To = "delivered@resend.dev",
        Subject = "Hello from Console",
        HtmlBody = "<p>Email using <strong>Resend .NET SDK</strong></p>",
    } );

    Console.WriteLine( "Id={0}", resp.Content );
}
catch ( ResendException ex )
{
    Console.Error.WriteLine( "err: {0} - {1}", ex.ErrorType, ex.Message );
}
