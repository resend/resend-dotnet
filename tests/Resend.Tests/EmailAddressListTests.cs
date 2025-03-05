namespace Resend.Tests;

/// <summary />
public class EmailAddressListTests
{
    /// <summary />
    [Fact]
    public void ImplicitFromString()
    {
        var email = "a@example.com";

        var message = new EmailMessage();
        message.To = email;

        Assert.NotNull( message.To );
        Assert.Single( message.To );
        Assert.Equal( email, message.To.First().Email );
        Assert.Null( message.To.First().DisplayName );
    }


    /// <summary />
    [Fact]
    public void ImplicitFromStringWithDisplayName()
    {
        var value = "John Doe <a@example.com>";

        var message = new EmailMessage();
        message.To = value;

        Assert.NotNull( message.To );
        Assert.Single( message.To );
        Assert.Equal( "a@example.com", message.To.First().Email );
        Assert.Equal( "John Doe", message.To.First().DisplayName );
    }


    /// <summary />
    [Fact]
    public void ImplicitFromArray()
    {
        var value = new string[] {
            "John Doe <a@example.com>",
            "Jane Doe <b@example.com>",
            "c@example.com"
        };

        var message = new EmailMessage();
        message.To = value;

        Assert.NotNull( message.To );
        Assert.Equal( 3, message.To.Count );

        Assert.Equal( "a@example.com", message.To[ 0 ].Email );
        Assert.Equal( "John Doe", message.To[ 0 ].DisplayName );

        Assert.Equal( "b@example.com", message.To[ 1 ].Email );
        Assert.Equal( "Jane Doe", message.To[ 1 ].DisplayName );

        Assert.Equal( "c@example.com", message.To[ 2 ].Email );
        Assert.Null( message.To[ 2 ].DisplayName );
    }


    /// <summary />
    [Fact]
    public void FromParams()
    {
        var list = EmailAddressList.From( "a@example.com", "b@example.com" );

        Assert.NotNull( list );
        Assert.Equal( 2, list.Count );
    }


    /// <summary />
    [Fact]
    public void FromArray()
    {
        var array = new string[] { "a@example.com", "b@example.com" };
        var enumz = array.AsEnumerable();

        var list = EmailAddressList.From( enumz );

        Assert.NotNull( list );
        Assert.Equal( 2, list.Count );
        Assert.Equal( array[ 0 ], list[ 0 ].Email );
        Assert.Equal( array[ 1 ], list[ 1 ].Email );
    }
}