using Resend;
using Temporalio.Workflows;

namespace AsyncTemporal.Temporal;

/// <summary />
[Workflow]
public class EmailSendWorkflow
{
    /// <summary />
    public EmailSendWorkflow()
    {
    }


    /// <summary />
    [WorkflowRun]
    public async Task<string> RunAsync( EmailMessage email )
    {
        /*
         * Use workflow logger, which doesn't log during replay: only when
         * the code segment is being executed for the first time.
         */
        Workflow.Logger.LogInformation( "Email {To}: {Subject}", email.To, email.Subject );


        /*
         * Email Send
         */
        var emailId = await Workflow.ExecuteActivityAsync(
            ( EmailSendActivity act ) => act.EmailSendAsync( email ),
            new() { ScheduleToCloseTimeout = TimeSpan.FromMinutes( 5 ) } );


        /*
         * 
         */
        Workflow.Logger.LogInformation( "Sent {EmailId}", emailId );

        return emailId;
    }
}