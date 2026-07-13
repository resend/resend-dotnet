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


    /// <summary />
    [Fact]
    public void SerializesTags()
    {
        var message = new EmailMessage()
        {
            From = "from@example.com",
            To = "to@example.com",
            Subject = "Hello",
            HtmlBody = "<p>Hi</p>",
            Tags = [ new EmailTag { Name = "category", Value = "confirm_email" } ],
        };

        var json = JsonSerializer.Serialize( message );

        Assert.Contains( "\"tags\":[{\"name\":\"category\",\"value\":\"confirm_email\"}]", json );
    }


    /// <summary />
    [Fact]
    public void DeserializesTags()
    {
        const string json = """
            {
              "from": "from@example.com",
              "to": ["to@example.com"],
              "subject": "Hello",
              "html": "<p>Hi</p>",
              "tags": [
                {
                  "name": "category",
                  "value": "confirm_email"
                }
              ]
            }
            """;

        var message = JsonSerializer.Deserialize<EmailMessage>( json );

        Assert.NotNull( message );
        Assert.NotNull( message.Tags );
        Assert.Single( message.Tags );
        Assert.Equal( "category", message.Tags[ 0 ].Name );
        Assert.Equal( "confirm_email", message.Tags[ 0 ].Value );
    }
}
