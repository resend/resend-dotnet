using System.Text.Json;

namespace Resend.Tests;

/// <summary />
public class EmailMessageTests
{
    /// <summary />
    [Fact]
    public void OmitsSubjectAndFromWhenNull()
    {
        var message = new EmailMessage()
        {
            To = "to@example.com",
        };

        var json = JsonSerializer.Serialize( message );

        Assert.DoesNotContain( "\"subject\"", json );
        Assert.DoesNotContain( "\"from\"", json );
    }


    /// <summary />
    [Fact]
    public void IncludesSubjectAndFromWhenSet()
    {
        var message = new EmailMessage()
        {
            From = "from@example.com",
            To = "to@example.com",
            Subject = "Hello",
        };

        var json = JsonSerializer.Serialize( message );

        Assert.Contains( "\"subject\":\"Hello\"", json );
        Assert.Contains( "\"from\"", json );
    }
}
