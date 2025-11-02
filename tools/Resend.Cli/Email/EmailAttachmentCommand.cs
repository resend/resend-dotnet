using McMaster.Extensions.CommandLineUtils;

namespace Resend.Cli.Email;

/// <summary />
[Command( "attach", Description = "Attachment of sent emails" )]
[Subcommand( typeof( EmailAttachmentListCommand ) )]
[Subcommand( typeof( EmailAttachmentRetrieveCommand ) )]
public class EmailAttachmentCommand
{
    /// <summary />
    public int OnExecute( CommandLineApplication app )
    {
        app.ShowHelp();
        return 1;
    }
}