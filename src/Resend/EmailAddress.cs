using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace Resend;

/// <summary>
/// Email address.
/// </summary>
[JsonConverter( typeof( EmailAddressConverter ) )]
public class EmailAddress
{
    /// <summary>
    /// Email address.
    /// </summary>
    public string Email { get; set; } = default!;

    /// <summary>
    /// Display name.
    /// </summary>
    public string? DisplayName { get; set; }


    /// <summary />
    public override string ToString()
    {
        string addr;

        if ( this.DisplayName == null )
            addr = this.Email;
        else
            addr = $"{this.DisplayName} <{this.Email}>";

        return addr;
    }


    private static readonly Regex _fn = new Regex( "^(?<displayName>.*) <(?<email>.*)>$" );


    /// <summary />
    public static EmailAddress Parse( string addr )
    {
        /*
         * 
         */
        string email;
        string? displayName = null;

        var m = _fn.Match( addr );

        if ( m.Success == true )
        {
            email = m.Groups[ "email" ].Value;
            displayName = m.Groups[ "displayName" ].Value;
        }
        else
        {
            email = addr;
        }


        /*
         * 
         */
        return new EmailAddress()
        {
            Email = email,
            DisplayName = displayName,
        };
    }


    /// <summary />
    public static implicit operator EmailAddress( string email ) => Parse( email );
}
