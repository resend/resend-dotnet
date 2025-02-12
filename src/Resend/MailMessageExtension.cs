using Resend;

namespace System.Net.Mail;

/// <summary />
public static class MailMessageExtension
{
    /// <summary>
    /// Creates an instance of <see cref="EmailMessage" /> for Resend API, based
    /// on an instance of the default/standard .NET <see cref="MailMessage" />.
    /// </summary>
    /// <param name="message">
    /// Mail message.
    /// </param>
    /// <returns>
    /// Email message for Resend.
    /// </returns>
    /// <exception cref="ArgumentOutOfRangeException">
    /// Exception is thrown whenever mandatory fields are not specified, such as
    /// sender or recipient email addresses.
    /// </exception>
    public static EmailMessage ToEmailMessage( this MailMessage message )
    {
        if ( message.From == null )
            throw new ArgumentOutOfRangeException( nameof( message ), "From is not set" );

        if ( message.To.Count == 0 )
            throw new ArgumentOutOfRangeException( nameof( message ), "No To email addresss are set" );


        /*
         * 
         */
        var email = new EmailMessage();
        email.Subject = message.Subject;
        email.From = ToEmailAddress( message.From );
        email.To.AddRange( message.To.Select( x => ToEmailAddress( x ) ) );

        email.Cc = ToAddressList( message.CC );
        email.Bcc = ToAddressList( message.Bcc );
        email.ReplyTo = ToAddressList( message.ReplyToList );

        if ( message.IsBodyHtml == true )
            email.HtmlBody = message.Body;
        else
            email.TextBody = message.Body;


        /*
         * Headers
         */
        email.Headers = new Dictionary<string, string>();

        if ( message.Headers.Count > 0 )
        {
            foreach ( string key in message.Headers )
            {
                string val = message.Headers[ key ] ?? "";

                email.Headers.Add( key, val );
            }
        }


        /*
         * Priority
         */
        switch ( message.Priority )
        {
            case MailPriority.High:
                // https://docs.microsoft.com/en-us/openspecs/exchange_server_protocols/ms-oxcmail/2bb19f1b-b35e-4966-b1cb-1afd044e83ab
                email.Headers.Add( "X-Priority", "1" );
                email.Headers.Add( "X-MSMail-Priority", "High" );
                break;

            case MailPriority.Normal:
                // Do not set anything.
                // Leave default values. It means Normal Priority.
                break;

            case MailPriority.Low:
                // https://docs.microsoft.com/en-us/openspecs/exchange_server_protocols/ms-oxcmail/2bb19f1b-b35e-4966-b1cb-1afd044e83ab
                email.Headers.Add( "X-Priority", "5" );
                email.Headers.Add( "X-MSMail-Priority", "Low" );
                break;
        }


        /*
         * Attachments
         */
        if ( message.Attachments.Count > 0 )
        {
            email.Attachments = new List<EmailAttachment>();

            foreach ( var att in message.Attachments )
            {
                var ms = new MemoryStream();
                att.ContentStream.CopyTo( ms );

                email.Attachments.Add( new EmailAttachment()
                {
                    Filename = att.Name ?? "file.oct",
                    Content = ms.ToArray(),
                } );
            }
        }

        return email;
    }


    /// <summary />
    private static EmailAddress ToEmailAddress( MailAddress from )
    {
        if ( from.DisplayName == null )
            return from.Address;

        return new EmailAddress()
        {
            Email = from.Address,
            DisplayName = from.DisplayName,
        };
    }


    /// <summary />
    private static EmailAddressList? ToAddressList( MailAddressCollection collection )
    {
        if ( collection.Count == 0 )
            return null;

        var list = new EmailAddressList();
        list.AddRange( collection.Select( x => ToEmailAddress( x ) ) );

        return list;
    }
}
