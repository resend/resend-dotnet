using McMaster.Extensions.CommandLineUtils;

namespace Resend.Cli.Receive;

/// <summary />
[Command( "attach", Description = "Attachment of received emails" )]
[Subcommand( typeof( ReceiveAttachmentListCommand ) )]
[Subcommand( typeof( ReceiveAttachmentRetrieveCommand ) )]
public class ReceiveAttachmentCommand
{
    /// <summary />
    public int OnExecute( CommandLineApplication app )
    {
        app.ShowHelp();
        return 1;
    }
}