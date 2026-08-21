using McMaster.Extensions.CommandLineUtils;

namespace Resend.Cli;

/// <summary />
[Command( "apikey", Description = "API key management" )]
[Subcommand( typeof( ApiKey.ApiKeyCreateCommand ))]
[Subcommand( typeof( ApiKey.ApiKeyListCommand ))]
[Subcommand( typeof( ApiKey.ApiKeyUpdateCommand ) )]
[Subcommand( typeof( ApiKey.ApiKeyDeleteCommand ) )]
public class ApiKeyCommand
{
    /// <summary />
    public int OnExecute( CommandLineApplication app )
    {
        app.ShowHelp();
        return 1;
    }
}
