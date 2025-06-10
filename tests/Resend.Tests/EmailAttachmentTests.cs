namespace Resend.Tests;

/// <summary />
public class EmailAttachmentTests
{
    /// <summary />
    [Fact]
    public void FromOk()
    {
        var relPath = OperatingSystem.IsWindows() == true ? @"..\\..\\..\\" : "../../../";
        var path = Path.Combine( AppContext.BaseDirectory, relPath, "EmailAttachmentTests.cs" );
        var size = new FileInfo( path ).Length;

        var attach = EmailAttachment.From( path );


        /*
         * 
         */
        Assert.Equal( "EmailAttachmentTests.cs", attach.Filename );
        Assert.Null( attach.Path );
        Assert.NotNull( attach.Content );
        Assert.Equal( size, attach.Content?.LongLength );
    }


    /// <summary />
    [Fact]
    public void FromNotFound()
    {
        var relPath = OperatingSystem.IsWindows() == true ? @"..\\..\\..\\" : "../../../";
        var path = Path.Combine( AppContext.BaseDirectory, relPath, "NOTFOUND.cs" );

        Action act = () => EmailAttachment.From( path );


        /*
         * 
         */
        var ex = Assert.Throws<InvalidOperationException>( act );
        Assert.StartsWith( "EA001:", ex.Message );
    }


    /// <summary />
    [Fact]
    public async Task FromAsyncOk()
    {
        var relPath = OperatingSystem.IsWindows() == true ? @"..\\..\\..\\" : "../../../";
        var path = Path.Combine( AppContext.BaseDirectory, relPath, "EmailAttachmentTests.cs" );
        var size = new FileInfo( path ).Length;

        var attach = await EmailAttachment.FromAsync( path );


        /*
         * 
         */
        Assert.Equal( "EmailAttachmentTests.cs", attach.Filename );
        Assert.Null( attach.Path );
        Assert.NotNull( attach.Content );
        Assert.Equal( size, attach.Content?.LongLength );
    }


    /// <summary />
    [Fact]
    public void FromAsyncNotFound()
    {
        var relPath = OperatingSystem.IsWindows() == true ? @"..\\..\\..\\" : "../../../";
        var path = Path.Combine( AppContext.BaseDirectory, relPath, "NOTFOUND.cs" );

        Action act = () => EmailAttachment.FromAsync( path ).GetAwaiter().GetResult();


        /*
         * 
         */
        var ex = Assert.Throws<InvalidOperationException>( act );
        Assert.StartsWith( "EA002:", ex.Message );
    }
}