using McMaster.Extensions.CommandLineUtils;
using System.ComponentModel.DataAnnotations;

namespace Resend.Cli.ApiKey;

/// <summary />
[Command( "update", Description = "Rename an API key" )]
public class ApiKeyUpdateCommand
{
    private readonly IResend _resend;


    /// <summary />
    [Argument( 0, Description = "API key identifier" )]
    [Required]
    public Guid? KeyId { get; set; }

    /// <summary />
    [Argument( 1, Description = "New API key name" )]
    [Required]
    public string KeyName { get; set; } = default!;


    /// <summary />
    public ApiKeyUpdateCommand( IResend resend )
    {
        _resend = resend;
    }


    /// <summary />
    public async Task<int> OnExecuteAsync()
    {
        await _resend.ApiKeyUpdateAsync( this.KeyId!.Value, this.KeyName );

        return 0;
    }
}
