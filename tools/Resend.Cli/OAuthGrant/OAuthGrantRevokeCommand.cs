using McMaster.Extensions.CommandLineUtils;
using System.ComponentModel.DataAnnotations;

namespace Resend.Cli.OAuthGrant;

/// <summary />
[Command( "revoke", Description = "Revoke an OAuth grant" )]
public class OAuthGrantRevokeCommand
{
    private readonly IResend _resend;


    /// <summary />
    [Argument( 0, Description = "OAuth grant identifier" )]
    [Required]
    public Guid? GrantId { get; set; }


    /// <summary />
    public OAuthGrantRevokeCommand( IResend resend )
    {
        _resend = resend;
    }


    /// <summary />
    public async Task<int> OnExecuteAsync()
    {
        await _resend.OAuthGrantRevokeAsync( this.GrantId!.Value );

        return 0;
    }
}
