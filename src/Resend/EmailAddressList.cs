using System.Text.Json.Serialization;

namespace Resend;

/// <summary>
/// List of email addresses.
/// </summary>
[JsonConverter( typeof( EmailAddressListConverter ) )]
public class EmailAddressList : List<EmailAddress>
{
    /// <summary />
    public EmailAddressList()
    {
    }


    /// <summary>
    /// Implicitly create an email address list from a single email string.
    /// </summary>
    public static implicit operator EmailAddressList( string email )
    {
        var list = new EmailAddressList();
        list.Add( email );

        return list;
    }


    /// <summary>
    /// Implicitly create an email address list from an array of email strings.
    /// </summary>
    public static implicit operator EmailAddressList( string[] emails )
    {
        var list = new EmailAddressList();

        foreach ( var email in emails )
            list.Add( email );

        return list;
    }


    /// <summary />
    public static EmailAddressList From( IEnumerable<string> emails )
    {
        var list = new EmailAddressList();

        foreach ( var em in emails )
            list.Add( em );

        return list;
    }


    /// <summary />
    public static EmailAddressList From( params string[] emails )
    {
        var list = new EmailAddressList();

        foreach ( var em in emails )
            list.Add( em );

        return list;
    }
}