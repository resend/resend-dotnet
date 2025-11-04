using McMaster.Extensions.CommandLineUtils;

namespace Resend.Cli;

/// <summary />
[Command( "email", Description = "Send emails" )]
[Subcommand( typeof( Email.EmailAttachmentCommand ) )]
[Subcommand( typeof( Email.EmailBatchCommand ) )]
[Subcommand( typeof( Email.EmailCancelCommand ) )]
[Subcommand( typeof( Email.EmailListCommand ) )]
[Subcommand( typeof( Email.EmailRescheduleCommand ) )]
[Subcommand( typeof( Email.EmailRetrieveCommand ) )]
[Subcommand( typeof( Email.EmailSendCommand ) )]
[Subcommand( typeof( Email.EmailSkeletonCommand ) )]
public class EmailCommand
{
    /// <summary />
    public int OnExecute( CommandLineApplication app )
    {
        app.ShowHelp();
        return 1;
    }
}