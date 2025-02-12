using System.Text.Json;

namespace Resend.Tests;

/// <summary />
public class EmailAddressTests
{
    /// <summary />
    [Fact]
    public void OperatorSimple()
    {
        EmailAddress src = "dev@example.com";

        Assert.Equal( "dev@example.com", src.Email );
        Assert.Null( src.DisplayName );
    }


    /// <summary />
    [Fact]
    public void OperatorWithDisplayName()
    {
        EmailAddress src = "John Doe <dev@example.com>";

        Assert.Equal( "dev@example.com", src.Email );
        Assert.Equal( "John Doe", src.DisplayName );
    }
}