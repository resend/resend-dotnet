using McMaster.Extensions.CommandLineUtils;

namespace Resend.Cli.Domain;

/// <summary />
[Command( "claim", Description = "Claim a domain that is already verified by another team" )]
[Subcommand( typeof( Claim.DomainClaimCreateCommand ) )]
[Subcommand( typeof( Claim.DomainClaimGetCommand ) )]
[Subcommand( typeof( Claim.DomainClaimVerifyCommand ) )]
public class DomainClaimCommand
{
    /// <summary />
    public int OnExecute( CommandLineApplication app )
    {
        app.ShowHelp();
        return 1;
    }
}
