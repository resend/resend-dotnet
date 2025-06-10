using FluentEmail.Core;
using FluentEmail.Core.Interfaces;
using FluentEmail.Core.Models;
using System.Text;

namespace Resend.FluentEmail;

/// <summary>
/// FluendEmail implementation for Resend.
/// </summary>
public class ResendSender : ISender
{
    private readonly IResend _resend;

    /// <summary />
    public ResendSender( IResend resend )
    {
        _resend = resend;
    }


    /// <inheritdoc />
    public SendResponse Send( IFluentEmail email, CancellationToken? token = null )
    {
        var task = SendAsync( email, token );

        return task.GetAwaiter().GetResult();
    }


    /// <inheritdoc />
    public async Task<SendResponse> SendAsync( IFluentEmail email, CancellationToken? token = null )
    {
        var message = ToMessage( email );

        var resp = await _resend.EmailSendAsync( message, token ?? CancellationToken.None ).ConfigureAwait( false );


        /*
         * 
         */
        if ( resp.Success == false )
        {
            return new SendResponse()
            {
                ErrorMessages = ToErrorMessages( resp.Exception! ).ToList(),
            };
        }


        /*
         * 
         */
        return new SendResponse()
        {
            MessageId = resp.Content.ToString(),
        };
    }


    /// <summary />
    private IEnumerable<string> ToErrorMessages( ResendException exception )
    {
        yield return $"{exception.StatusCode}: {exception.ErrorType}: {exception.Message}";
    }


    /// <summary />
    private static EmailMessage ToMessage( IFluentEmail email )
    {
        var message = new EmailMessage();
        message.Headers = new Dictionary<string, string>();
        message.Tags = new List<EmailTag>();


        /*
         * Basics
         */
        message.Subject = email.Data.Subject;
        message.From = ToEmailAddress( email.Data.FromAddress );
        message.To = ToEmailAddressList( email.Data.ToAddresses );

        if ( email.Data.CcAddresses.Count > 0 )
            message.Cc = ToEmailAddressList( email.Data.CcAddresses );

        if ( email.Data.BccAddresses.Count > 0 )
            message.Bcc = ToEmailAddressList( email.Data.BccAddresses );

        if ( email.Data.ReplyToAddresses.Count > 0 )
            message.ReplyTo = ToEmailAddressList( email.Data.ReplyToAddresses );


        /*
         * Body
         */
        if ( email.Data.IsHtml == true )
        {
            message.HtmlBody = email.Data.Body;

            if ( email.Data.PlaintextAlternativeBody != "" )
                message.TextBody = email.Data.PlaintextAlternativeBody;
        }
        else
        {
            message.TextBody = email.Data.Body;
        }


        /*
         * Priority
         */
        switch ( email.Data.Priority )
        {
            case Priority.High:
                // https://docs.microsoft.com/en-us/openspecs/exchange_server_protocols/ms-oxcmail/2bb19f1b-b35e-4966-b1cb-1afd044e83ab
                message.Headers.Add( "X-Priority", "1" );
                message.Headers.Add( "X-MSMail-Priority", "High" );
                break;

            case Priority.Normal:
                // Do not set anything.
                // Leave default values. It means Normal Priority.
                break;

            case Priority.Low:
                // https://docs.microsoft.com/en-us/openspecs/exchange_server_protocols/ms-oxcmail/2bb19f1b-b35e-4966-b1cb-1afd044e83ab
                message.Headers.Add( "X-Priority", "5" );
                message.Headers.Add( "X-MSMail-Priority", "Low" );
                break;
        }


        /*
         * Headers
         */
        if ( email.Data.Headers.Count > 0 )
        {
            foreach ( var kv in email.Data.Headers )
            {
                if ( message.Headers.ContainsKey( kv.Key ) == true )
                    message.Headers[ kv.Key ] = kv.Value;
                else
                    message.Headers.Add( kv.Key, kv.Value );
            }
        }


        /*
         * Tags
         */
        if ( email.Data.Tags.Count > 0 )
        {
            message.Tags = email.Data.Tags.Select( x => new EmailTag()
            {
                Name = x,
                Value = "1",
            } ).ToList();
        }


        /*
         * Attachments
         */
        if ( email.Data.Attachments.Count > 0 )
        {
            message.Attachments = email.Data.Attachments.Select( x =>
            {
                var ms = new MemoryStream();
                x.Data.CopyTo( ms );

                if ( AsText( x.ContentType ) == true )
                {
                    var text = Encoding.UTF8.GetString( ms.ToArray() );

                    return new EmailAttachment()
                    {
                        Filename = x.Filename,
                        ContentType = x.ContentType,
                        Content = ms.ToArray(),
                    };
                }
                else
                {
                    return new EmailAttachment()
                    {
                        Filename = x.Filename,
                        ContentType = x.ContentType,
                        Content = ms.ToArray(),
                    };
                }

            } ).ToList();
        }

        return message;
    }


    /// <summary />
    private static bool AsText( string contentType )
    {
        if ( contentType.StartsWith( "text/" ) == true )
            return true;

        if ( contentType == "application/json" )
            return true;

        if ( contentType.EndsWith( "+json" ) == true )
            return true;

        if ( contentType.EndsWith( "+xml" ) == true )
            return true;

        return false;
    }


    /// <summary />
    private static EmailAddressList ToEmailAddressList( IList<Address> fluentAddresses )
    {
        var list = new EmailAddressList();

        foreach ( var addr in fluentAddresses )
            list.Add( ToEmailAddress( addr ) );

        return list;
    }


    /// <summary />
    private static EmailAddress ToEmailAddress( Address fluentAddress )
    {
        EmailAddress addr = new EmailAddress();
        addr.Email = fluentAddress.EmailAddress;

        if ( string.IsNullOrEmpty( fluentAddress.Name ) == true )
            addr.DisplayName = fluentAddress.Name;

        return addr;
    }
}
