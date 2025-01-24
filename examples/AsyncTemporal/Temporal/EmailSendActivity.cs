using Resend;
using Temporalio.Activities;

namespace AsyncTemporal.Temporal;

/// <summary />
public class EmailSendActivity
{
    private readonly IResend _resend;
    private readonly ILogger<EmailSendActivity> _logger;


    /// <summary />
    public EmailSendActivity( IResend resend, ILogger<EmailSendActivity> logger )
    {
        _resend = resend;
        _logger = logger;
    }


    /// <summary />
    [Activity]
    public async Task<string> EmailSendAsync( EmailMessage email )
    {
        /*
         * 
         */
        if ( email.Attachments?.Count > 0 )
        {
            foreach ( var att in email.Attachments )
            {
                if ( att.Path?.StartsWith( "load:" ) == false )
                    continue;

                _logger.LogDebug( "Attachment: Load {Filename} based on {LoadPath}", att.Filename, att.Path );

                att.Content = ContentLoad( att.Path! );
                att.Path = null;
            }
        }


        /*
         * 
         */
        var resp = await _resend.EmailSendAsync( email );

        return resp.Content.ToString();
    }


    /// <summary />
    private byte[]? ContentLoad( string attachmentPath )
    {
        // TODO: Load based on attachmentPath

        throw new NotImplementedException();
    }
}
