using Resend;
using Temporalio.Activities;
using Temporalio.Exceptions;

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
    public async Task<string> EmailSendAsync( EmailMessage message )
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
                ActivityExecutionContext.Current.Heartbeat();

                att.Content = await ContentLoadAsync( att.Path!, ActivityExecutionContext.Current.CancellationToken );
                att.Path = null;
            }
        }


        /*
         * When throwing Temporal's ApplicationFailureException, it is possible to
         * set nextRetryDelay to inform when to retry the activity.
         */
        ActivityExecutionContext.Current.Heartbeat();
        ResendResponse<Guid> resp;

        try
        {
            resp = await _resend.EmailSendAsync( message, ActivityExecutionContext.Current.CancellationToken );
        }
        catch ( ResendException ex )
        {
            TimeSpan nextRetryDelay = new TimeSpan( 0, 0, 10 );

            throw new ApplicationFailureException(
                ex.Message,
                errorType: ex.ErrorType.ToString(),
                nextRetryDelay: nextRetryDelay );
        }

        return resp.Content.ToString();
    }


    /// <summary />
    private async Task<byte[]> ContentLoadAsync( string attachmentPath, CancellationToken cancellationToken )
    {
        await Task.Delay( 1_000, cancellationToken );

        // TODO: Load based on attachmentPath

        throw new NotImplementedException();
    }
}
