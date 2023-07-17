﻿using McMaster.Extensions.CommandLineUtils;

namespace Resend.Cli;

/// <summary />
[Command( "email" )]
[Subcommand( typeof( Email.EmailRetrieveCommand ) )]
[Subcommand( typeof( Email.EmailSendCommand ) )]
public class EmailCommand
{
    /// <summary />
    public int OnExecute( CommandLineApplication app )
    {
        app.ShowHelp();
        return 1;
    }
}
