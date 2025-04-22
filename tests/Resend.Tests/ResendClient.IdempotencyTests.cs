namespace Resend.Tests;

public partial class ResendClientTests
{
    /// <summary />
    [Fact]
    public async Task EmailSend_GuidKey()
    {
        var email = new EmailMessage();
        email.Subject = "Unit testing";
        email.From = "from@example.com";
        email.To = "to@example.com";
        email.HtmlBody = "From unit test!";

        var key = IdempotencyKey.New();

        var resp = await _resend.EmailSendAsync( key, email );

        Assert.NotNull( resp );
        Assert.True( resp.Success );
        Assert.NotEqual( Guid.Empty, resp.Content );
    }


    /// <summary />
    [Fact]
    public async Task EmailSend_EntityId()
    {
        var email = new EmailMessage();
        email.Subject = "Unit testing";
        email.From = "from@example.com";
        email.To = "to@example.com";
        email.HtmlBody = "From unit test!";

        var key = IdempotencyKey.New<Guid>( "test", Guid.NewGuid() );

        var resp = await _resend.EmailSendAsync( key, email );

        Assert.NotNull( resp );
        Assert.True( resp.Success );
        Assert.NotEqual( Guid.Empty, resp.Content );
    }
}