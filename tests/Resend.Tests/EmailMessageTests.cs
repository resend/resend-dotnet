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
    public void SerializesBatchFields()
    {
        var message = new EmailMessage()
        {
            From = "from@example.com",
            To = "to@example.com",
            Subject = "Hello",
            HtmlBody = "<p>Hi</p>",
            Tags = [ new EmailTag { Name = "category", Value = "confirm_email" } ],
            MomentSchedule = "in 1 hour",
            Attachments = [
                new EmailAttachment()
                {
                    Filename = "file.txt",
                    Content = "hello"u8.ToArray(),
                },
            ],
        };

        var json = JsonSerializer.Serialize( message );

        Assert.Contains( "\"tags\":[{\"name\":\"category\",\"value\":\"confirm_email\"}]", json );
        Assert.Contains( "\"scheduled_at\":\"in 1 hour\"", json );
        Assert.Contains( "\"attachments\":[{\"filename\":\"file.txt\",\"content\":\"aGVsbG8=\"}]", json );
    }


    /// <summary />
    [Fact]
    public void DeserializesBatchFields()
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
              ],
              "scheduled_at": "in 1 hour",
              "attachments": [
                {
                  "filename": "file.txt",
                  "content": "aGVsbG8="
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
        Assert.NotNull( message.MomentSchedule );
        Assert.False( message.MomentSchedule.Value.IsMoment );
        Assert.Equal( "in 1 hour", message.MomentSchedule.Value.Human );
        Assert.NotNull( message.Attachments );
        Assert.Single( message.Attachments );
        Assert.Equal( "file.txt", message.Attachments[ 0 ].Filename );
        Assert.True( message.Attachments[ 0 ].Content?.IsByteArray );
        Assert.Equal( "hello"u8.ToArray(), message.Attachments[ 0 ].Content?.ByteArray );
    }
}
