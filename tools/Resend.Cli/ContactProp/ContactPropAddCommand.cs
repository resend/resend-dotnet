using McMaster.Extensions.CommandLineUtils;
using System.ComponentModel.DataAnnotations;

namespace Resend.Cli.ContactProp;

/// <summary />
[Command( "add", Description = "Create a contact property" )]
public class ContactPropAddCommand
{
    private readonly IResend _resend;


    /// <summary />
    [Argument( 0, Description = "Key" )]
    [Required]
    public string? Key { get; set; }

    /// <summary />
    [Option( "-t|--type", CommandOptionType.SingleValue, Description = "Property type" )]
    public ContactPropertyType PropertyType { get; set; } = ContactPropertyType.String;

    /// <summary />
    [Option( "-v|--default", CommandOptionType.SingleValue, Description = "Default value" )]
    public string? Default { get; set; }


    /// <summary />
    public ContactPropAddCommand( IResend resend )
    {
        _resend = resend;
    }


    /// <summary />
    public async Task<int> OnExecuteAsync()
    {
        object? def = null;

        if ( this.Default != null )
        {
            if ( this.PropertyType == ContactPropertyType.String )
                def = this.Default;

            if ( this.PropertyType == ContactPropertyType.Number )
                def = long.Parse( this.Default );
        }

        var data = new ContactPropertyData()
        {
            Key = this.Key!,
            PropertyType = this.PropertyType,
            DefaultValue = def,
        };

        var res = await _resend.ContactPropCreateAsync( data );
        var propId = res.Content;


        /*
         * 
         */
        Console.WriteLine( propId );

        return 0;
    }
}