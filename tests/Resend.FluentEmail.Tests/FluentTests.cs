using FluentEmail.Core;
using Microsoft.AspNetCore.Mvc.Testing;
using Resend.ApiServer;

namespace Resend.FluentEmail.Tests;

/// <summary />
public partial class FluentTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly IResend _resend;


    /// <summary />
    public FluentTests( WebApplicationFactory<Program> factory )
    {
        _factory = factory;

        var http = _factory.CreateClient();

        var opt = new ResendClientOptions()
        {
            ApiUrl = http.BaseAddress!.ToString(),
        };

        _resend = ResendClient.Create( opt, http );

        Email.DefaultSender = new ResendSender( _resend );
    }


    /// <summary />
    [Fact]
    public async Task EmailSend()
    {
        var resp = await Email
            .From( "from@example.com" )
            .To( "to@example.com" )
            .Subject( "Unit testing from Fluent" )
            .Body( "Html body", true )
            .PlaintextAlternativeBody( "Text body" )
            .SendAsync();

        Assert.NotNull( resp );
        Assert.True( resp.Successful );
        Assert.NotEqual( "", resp.MessageId );
    }
}
