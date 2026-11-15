using System.Reflection;
using FluentEmail.Core;

namespace Resend.FluentEmail.Tests;

/// <summary />
public class AttachmentContentTypeTests
{
    /// <summary />
    [Theory]
    [InlineData( null )]
    [InlineData( "application/pdf" )]
    [InlineData( "text/plain" )]
    public async Task AttachFromFilename_SendsFileBytes( string? contentType )
    {
        var payload = new byte[] { 0x00, 0xFF, 0x10, 0x7F, 0x41 };
        var path = Path.Combine( Path.GetTempPath(), "resend-fluent-" + Guid.NewGuid().ToString( "N" ) + ".bin" );
        await File.WriteAllBytesAsync( path, payload );

        var client = RecordingResend.Create( out var recording );
        var email = Email.From( "from@example.com" ).To( "to@example.com" ).Subject( "Attachment" );
        email.Sender = new ResendSender( client );

        try
        {
            var response = await email.AttachFromFilename( path, contentType ).SendAsync();

            Assert.True( response.Successful );
            Assert.Equal( 1, recording.SendCalls );

            var attachment = Assert.Single( recording.Message!.Attachments! );
            Assert.Equal( contentType, attachment.ContentType );
            Assert.Equal( path, attachment.Filename );
            Assert.NotNull( attachment.Content );
            Assert.Equal( payload, attachment.Content.Value.ByteArray );
        }
        finally
        {
            foreach ( var item in email.Data.Attachments )
                item.Data?.Dispose();

            File.Delete( path );
        }
    }


    /// <summary />
    public class RecordingResend : DispatchProxy
    {
        /// <summary />
        public int SendCalls { get; private set; }

        /// <summary />
        public EmailMessage? Message { get; private set; }


        /// <summary />
        public static IResend Create( out RecordingResend recording )
        {
            var client = Create<IResend, RecordingResend>();
            recording = (RecordingResend) (object) client;

            return client;
        }


        /// <inheritdoc />
        protected override object? Invoke( MethodInfo? targetMethod, object?[]? args )
        {
            if ( targetMethod?.Name == nameof( IResend.EmailSendAsync ) && args != null && args.Length > 0 && args[ 0 ] is EmailMessage message )
            {
                SendCalls++;
                Message = message;

                return Task.FromResult( new ResendResponse<Guid>( Guid.NewGuid(), null ) );
            }

            throw new NotSupportedException( targetMethod?.Name );
        }
    }
}
