using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

namespace Resend;

/// <summary>
/// Email address.
/// </summary>
[JsonConverter( typeof( EmailAddressConverter ) )]
public class EmailAddress : IEquatable<EmailAddress>
{
    /// <summary>
    /// Email address.
    /// </summary>
    public string Email { get; set; } = default!;

    /// <summary>
    /// Display name.
    /// </summary>
    public string? DisplayName { get; set; }


    /// <inheritdoc />
    public override string ToString()
    {
        return ToJson();
    }


    /// <summary>
    /// Converts to single string JSON representation.
    /// </summary>
    /// <returns></returns>
    public string ToJson()
    {
        string addr;

        if ( this.DisplayName == null )
            addr = this.Email;
        else
            addr = $"{this.DisplayName} <{this.Email}>";

        return addr;
    }


    private static readonly Regex _fn = new Regex( "^(?<displayName>.*) <(?<email>.*)>$" );


    /// <summary>
    /// Parses value from single string JSON representation.
    /// </summary>
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


    /// <inheritdoc />
    public bool Equals( EmailAddress? other )
    {
        return this.Email.Equals( other?.Email );
    }


    /// <inheritdoc />
    public override int GetHashCode()
    {
        return this.Email.GetHashCode();
    }


    /// <summary>
    /// Implicitly convert a string to <see cref="EmailAddress" /> value.
    /// </summary>
    public static implicit operator EmailAddress( string email ) => Parse( email );
}