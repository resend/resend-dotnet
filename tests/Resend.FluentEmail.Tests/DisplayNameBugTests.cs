using System.Reflection;
using FluentEmail.Core.Models;

namespace Resend.FluentEmail.Tests;

/// <summary>
/// Tests for display name handling in FluentEmail to Resend conversion.
/// </summary>
public class DisplayNameBugTests
{
    /// <summary />
    private static EmailAddress InvokeToEmailAddress( Address fluentAddress )
    {
        var method = typeof( ResendSender ).GetMethod(
            "ToEmailAddress",
            BindingFlags.NonPublic | BindingFlags.Static );

        return (EmailAddress) method!.Invoke( null, new object[] { fluentAddress } )!;
    }


    /// <summary />
    [Fact]
    public void WithDisplayName_ShouldPreserveIt()
    {
        var fluent = new Address( "john@example.com", "John Doe" );

        var result = InvokeToEmailAddress( fluent );

        Assert.Equal( "john@example.com", result.Email );
        Assert.Equal( "John Doe", result.DisplayName );
    }


    /// <summary />
    [Fact]
    public void WithDisplayName_ShouldSerializeCorrectly()
    {
        var fluent = new Address( "john@example.com", "John Doe" );

        var result = InvokeToEmailAddress( fluent );

        Assert.Equal( "John Doe <john@example.com>", result.ToString() );
    }


    /// <summary />
    [Fact]
    public void WithoutDisplayName_ShouldNotSetDisplayName()
    {
        var fluent = new Address( "john@example.com" );

        var result = InvokeToEmailAddress( fluent );

        Assert.Equal( "john@example.com", result.Email );
        Assert.Null( result.DisplayName );
        Assert.Equal( "john@example.com", result.ToString() );
    }
}
