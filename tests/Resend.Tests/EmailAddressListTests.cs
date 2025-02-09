namespace Resend.Tests;

/// <summary />
public class EmailAddressListTests
{
    /// <summary />
    [Fact]
    public void FromEmailString()
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
    public void FromEmailStringWithDisplayName()
    {
        var value = "Filipe Toscano <a@example.com>";

        var message = new EmailMessage();
        message.To = value;

        Assert.NotNull( message.To );
        Assert.Single( message.To );
        Assert.Equal( "a@example.com", message.To.First().Email );
        Assert.Equal( "Filipe Toscano", message.To.First().DisplayName );
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