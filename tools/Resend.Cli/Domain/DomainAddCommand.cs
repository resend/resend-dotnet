using McMaster.Extensions.CommandLineUtils;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace Resend.Cli.Domain;

/// <summary />
[Command( "add", Description = "Create an email sender domain" )]
public class DomainAddCommand
{
    private readonly IResend _resend;


    /// <summary />
    [Argument( 0, Description = "Domain name" )]
    [Required]
    public string DomainName { get; set; } = default!;

    /// <summary />
    [Option( "-r|--region", CommandOptionType.SingleValue, Description = "Delivery region" )]
    public DeliveryRegion? Region { get; set; }

    /// <summary />
    [Option( "--return-path", CommandOptionType.SingleValue, Description = "Return path" )]
    public string? ReturnPath { get; set; }


    /// <summary />
    public DomainAddCommand( IResend resend )
    {
        _resend = resend;
    }


    /// <summary />
    public async Task<int> OnExecuteAsync()
    {
        var res = await _resend.DomainAddAsync( new DomainAddData()
        {
            DomainName = this.DomainName,
            Region = this.Region,
            CustomReturnPath = this.ReturnPath,
        } );
        var domain = res.Content;


        /*
         * 
         */
        var jso = new JsonSerializerOptions()
        {
            WriteIndented = true,
        };

        var json = JsonSerializer.Serialize( domain, jso );
        Console.WriteLine( json );

        return 0;
    }
}
