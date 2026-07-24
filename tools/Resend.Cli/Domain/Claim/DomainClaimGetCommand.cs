using McMaster.Extensions.CommandLineUtils;
using Spectre.Console;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace Resend.Cli.Domain.Claim;

/// <summary />
[Command( "get", Description = "Retrieve the latest claim for a domain" )]
public class DomainClaimGetCommand
{
    private readonly IResend _resend;


    /// <summary />
    [Argument( 0, Description = "Placeholder domain identifier" )]
    [Required]
    public Guid? DomainId { get; set; }

    /// <summary />
    [Option( "-j|--json", CommandOptionType.NoValue, Description = "Emit output as JSON" )]
    public bool InJson { get; set; }


    /// <summary />
    public DomainClaimGetCommand( IResend resend )
    {
        _resend = resend;
    }


    /// <summary />
    public async Task<int> OnExecuteAsync()
    {
        var res = await _resend.DomainClaimRetrieveAsync( this.DomainId!.Value );

        DomainClaimRender.Write( res.Content, this.InJson );

        return 0;
    }
}
