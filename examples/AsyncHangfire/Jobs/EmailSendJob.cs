using Hangfire;
using Resend;

namespace AsyncHangfire.Jobs;

/// <summary />
public class EmailSendJob
{
    private readonly IResend _resend;
    private readonly ILogger<EmailSendJob> _logger;


    /// <summary />
    public EmailSendJob( IResend resend, ILogger<EmailSendJob> logger )
    {
        _resend = resend;
        _logger = logger;
    }


    /// <summary />
    [JobDisplayName( "Email Send" )]
    public async Task BackgroundSendAsync( EmailMessage message, CancellationToken cancellationToken )
    {
        _logger.LogDebug( "Email to {To}: {Subject}", message.To, message.Subject );


        /*
         * 
         */
        if ( message.Attachments?.Count > 0 )
        {
            foreach ( var att in message.Attachments )
            {
                if ( att.Path?.StartsWith( "load:" ) == false )
                    continue;

                _logger.LogDebug( "Attachment: Load {Filename} based on {LoadPath}", att.Filename, att.Path );

                att.Content = await ContentLoadAsync( att.Path!, cancellationToken );
                att.Path = null;
            }
        }


        /*
         * Send email
         */
        var resp = await _resend.EmailSendAsync( message, cancellationToken );

        _logger.LogInformation( "Sent with Id {EmailId}", resp.Content );
    }


    /// <summary />
    private async Task<byte[]> ContentLoadAsync( string attachmentPath, CancellationToken cancellationToken )
    {
        await Task.Delay( 1_000, cancellationToken );

        // TODO: Load based on attachmentPath

        throw new NotImplementedException();
    }
}