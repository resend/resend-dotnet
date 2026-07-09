using McMaster.Extensions.CommandLineUtils;

namespace Resend.Cli;

/// <summary />
[Command( "oauth-grant", Description = "OAuth grant management" )]
[Subcommand( typeof( OAuthGrant.OAuthGrantListCommand ) )]
[Subcommand( typeof( OAuthGrant.OAuthGrantRevokeCommand ) )]
public class OAuthGrantCommand
{
    /// <summary />
    public int OnExecute( CommandLineApplication app )
    {
        app.ShowHelp();
        return 1;
    }
}
