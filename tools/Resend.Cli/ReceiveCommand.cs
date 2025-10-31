using McMaster.Extensions.CommandLineUtils;

namespace Resend.Cli;

/// <summary />
[Command( "recv", Description = "Receive emails" )]
[Subcommand( typeof( Receive.ReceiveAttachmentCommand ) )]
[Subcommand( typeof( Receive.ReceiveListCommand ) )]
[Subcommand( typeof( Receive.ReceiveRetrieveCommand ) )]
public class ReceiveCommand
{
    /// <summary />
    public int OnExecute( CommandLineApplication app )
    {
        app.ShowHelp();
        return 1;
    }
}