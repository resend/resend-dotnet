using McMaster.Extensions.CommandLineUtils;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace Resend.Cli.Domain.Claim;

/// <summary />
[Command( "create", Description = "Claim a domain that is already verified by another team" )]
public class DomainClaimCreateCommand
{
    private readonly IResend _resend;


    /// <summary />
    [Option( "-n|--name", CommandOptionType.SingleValue, Description = "Domain name" )]
    [Required]
    public string DomainName { get; set; } = default!;

    /// <summary />
    [Option( "-r|--region", CommandOptionType.SingleValue, Description = "Delivery region" )]
    public DeliveryRegion? Region { get; set; }

    /// <summary />
    [Option( "--return-path", CommandOptionType.SingleValue, Description = "Return path" )]
    public string? ReturnPath { get; set; }


    /// <summary />
    public DomainClaimCreateCommand( IResend resend )
    {
        _resend = resend;
    }


    /// <summary />
    public async Task<int> OnExecuteAsync()
    {
        var res = await _resend.DomainClaimAsync( new DomainClaimData()
        {
            DomainName = this.DomainName,
            Region = this.Region,
            CustomReturnPath = this.ReturnPath,
        } );
        var claim = res.Content;


        /*
         *
         */
        var jso = new JsonSerializerOptions()
        {
            WriteIndented = true,
        };

        var json = JsonSerializer.Serialize( claim, jso );
        Console.WriteLine( json );

        return 0;
    }
}
