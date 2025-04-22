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


    /// <summary />
    [Fact]
    public void Distinct()
    {
        EmailAddress src1 = "John Doe <dev@example.com>";
        EmailAddress src2 = "dev@example.com";
        EmailAddress src3 = "Johnny Doe <dev@example.com>";
        EmailAddress src4 = "DEV@example.com";

        var list = new List<EmailAddress>() { src1, src2, src3, src4 };
        var uq = list.Distinct().ToList();

        Assert.Equal( 2, uq.Count() );
    }
}