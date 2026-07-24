using McMaster.Extensions.CommandLineUtils;
using System.ComponentModel.DataAnnotations;

namespace Resend.Cli.Domain.Claim;

/// <summary />
[Command( "verify", Description = "Trigger DNS verification for a domain claim" )]
public class DomainClaimVerifyCommand
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
    public DomainClaimVerifyCommand( IResend resend )
    {
        _resend = resend;
    }


    /// <summary />
    public async Task<int> OnExecuteAsync()
    {
        var res = await _resend.DomainClaimVerifyAsync( this.DomainId!.Value );

        DomainClaimRender.Write( res.Content, this.InJson );

        return 0;
    }
}
