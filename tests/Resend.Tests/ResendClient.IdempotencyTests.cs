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


    /// <summary />
    [Fact]
    public async Task InboxDraftCreate_Key()
    {
        var resp = await _resend.InboxDraftCreateAsync( IdempotencyKey.New(), Guid.NewGuid(), new InboxDraftCreateData()
        {
            TextBody = "Refund issued for order 1041.",
        } );

        Assert.NotNull( resp );
        Assert.True( resp.Success );
        Assert.NotEqual( Guid.Empty, resp.Content );
    }


    /// <summary />
    [Fact]
    public async Task InboxDraftSend_Key()
    {
        var draftId = Guid.NewGuid();

        var resp = await _resend.InboxDraftSendAsync( IdempotencyKey.New<Guid>( "draft", draftId ), Guid.NewGuid(), draftId );

        Assert.NotNull( resp );
        Assert.True( resp.Success );
        Assert.NotEqual( Guid.Empty, resp.Content );
    }
}