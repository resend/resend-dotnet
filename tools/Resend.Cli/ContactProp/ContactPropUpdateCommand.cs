using McMaster.Extensions.CommandLineUtils;
using System.ComponentModel.DataAnnotations;

namespace Resend.Cli.ContactProp;

/// <summary />
[Command( "update", Description = "Update a contact property" )]
public class ContactPropUpdateCommand
{
    private readonly IResend _resend;

    /// <summary />
    [Argument( 0, Description = "Property identifier" )]
    [Required]
    public Guid? PropId { get; set; }

    /// <summary />
    [Option( "-v|--default", CommandOptionType.SingleValue, Description = "Default value" )]
    public string? Default { get; set; }


    /// <summary />
    public ContactPropUpdateCommand( IResend resend )
    {
        _resend = resend;
    }


    /// <summary />
    public async Task<int> OnExecuteAsync()
    {
        /*
         * 
         */
        var res = await _resend.ContactPropRetrieveAsync( this.PropId!.Value );
        var prop = res.Content;


        /*
         * 
         */
        object? def = null;

        if ( this.Default != null )
        {
            if ( prop.PropertyType == ContactPropertyType.String )
                def = this.Default;

            if ( prop.PropertyType == ContactPropertyType.Number )
                def = long.Parse( this.Default );
        }


        /*
         * 
         */
        await _resend.ContactPropUpdateAsync( this.PropId!.Value, new ContactPropertyUpdateData()
        {
            DefaultValue = def,
        } );

        return 0;
    }
}