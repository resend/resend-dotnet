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
         * Send email
         */
        var resp = await _resend.EmailSendAsync( message, cancellationToken );

        _logger.LogInformation( "Sent with Id {EmailId}", resp.Content );
    }
}