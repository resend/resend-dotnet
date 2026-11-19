using System.Reflection;
using System.Text.Json;
using FluentEmail.Core;
using FluentEmail.Core.Models;

namespace Resend.FluentEmail.Tests;

/// <summary />
public class AttachmentTests
{
    /// <summary />
    [Theory]
    [InlineData( "image/png", "logo" )]
    [InlineData( "text/plain", "text" )]
    [InlineData( "image/png", null )]
    [InlineData( "text/plain", null )]
    public void ToMessage_ShouldPreserveContentId( string contentType, string? contentId )
    {
        using var data = new MemoryStream( new byte[] { 1, 2, 3 } );
        var email = Email.From( "from@example.com" ).To( "to@example.com" );
        email.Data.Attachments.Add( new Attachment()
        {
            Filename = "attachment",
            ContentType = contentType,
            ContentId = contentId,
            IsInline = contentId != null,
            Data = data,
        } );

        var method = typeof( ResendSender ).GetMethod(
            "ToMessage",
            BindingFlags.NonPublic | BindingFlags.Static );
        var message = (EmailMessage) method!.Invoke( null, new object[] { email } )!;
        var attachment = Assert.Single( message.Attachments! );

        Assert.Equal( contentId, attachment.ContentId );
        using var json = JsonDocument.Parse( JsonSerializer.Serialize( attachment ) );
        if ( contentId != null )
            Assert.Equal( contentId, json.RootElement.GetProperty( "content_id" ).GetString() );
        else
            Assert.False( json.RootElement.TryGetProperty( "content_id", out _ ) );
    }
}
