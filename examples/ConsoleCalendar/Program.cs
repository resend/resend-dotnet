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
 * Warning: iCalendar content must be sent as text.
 */
var inviteText = File.ReadAllText( "invite.ics" );


/*
 * 
 */
try
{
    var email = new EmailMessage()
    {
        From = "you@example.com",
        To = "user@gmail.com",
        Subject = "Hello from Console",
        HtmlBody = "<p>Email using <strong>Resend .NET SDK</strong></p>",
        Attachments = [
            new EmailAttachment()
            {
                Filename = "invite.ics",
                ContentType = "text/calendar",
                Content = inviteText,
            },
        ],
    };

    var resp = await resend.EmailSendAsync( email );

    Console.WriteLine( "Id={0}", resp.Content );
}
catch ( ResendException ex )
{
    Console.Error.WriteLine( "err: {0} - {1}", ex.ErrorType, ex.Message );
}
