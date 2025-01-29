using McMaster.Extensions.CommandLineUtils;
using System.ComponentModel.DataAnnotations;

namespace Resend.Cli.ApiKey;

/// <summary />
[Command( "delete", Description = "Delete an API key" )]
public class ApiKeyDeleteCommand
{
    private readonly IResend _resend;


    /// <summary />
    [Argument( 0, Description = "API key identifier" )]
    [Required]
    public Guid? KeyId { get; set; }


    /// <summary />
    public ApiKeyDeleteCommand( IResend resend )
    {
        _resend = resend;
    }


    /// <summary />
    public async Task<int> OnExecuteAsync()
    {
        await _resend.ApiKeyDeleteAsync( this.KeyId!.Value );

        return 0;
    }
}
