using System.Text.Json;

namespace Resend.Tests;

/// <summary />
public class EmailMessageTemplateTests
{
    /// <summary />
    [Fact]
    public void SerializesAliasId()
    {
        var message = new EmailMessage()
        {
            From = "from@example.com",
            To = "to@example.com",
            Template = new EmailMessageTemplate() { TemplateId = "welcome-email" },
        };

        var json = JsonSerializer.Serialize( message );

        Assert.Contains( "\"id\":\"welcome-email\"", json );
    }


    /// <summary />
    [Fact]
    public void RoundtripsAliasId()
    {
        var template = new EmailMessageTemplate() { TemplateId = "welcome-email" };

        var json = JsonSerializer.Serialize( template );
        var actual = JsonSerializer.Deserialize<EmailMessageTemplate>( json );

        Assert.NotNull( actual );
        Assert.Equal( "welcome-email", actual.TemplateId );
    }
}
