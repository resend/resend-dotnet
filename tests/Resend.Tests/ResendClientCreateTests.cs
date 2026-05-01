namespace Resend.Tests;

public class ResendClientCreateTests
{
    [Fact]
    public void Create_WithApiToken_Succeeds()
    {
        var client = ResendClient.Create( "re_test_123" );

        Assert.NotNull( client );
    }


    [Fact]
    public void Create_NoArg_WithEnvVar_Succeeds()
    {
        Environment.SetEnvironmentVariable( "RESEND_API_KEY", "re_env_123" );

        try
        {
            var client = ResendClient.Create();

            Assert.NotNull( client );
        }
        finally
        {
            Environment.SetEnvironmentVariable( "RESEND_API_KEY", null );
        }
    }


    [Fact]
    public void Create_WithOptions_NoApiToken_EnvVarFallback_Succeeds()
    {
        Environment.SetEnvironmentVariable( "RESEND_API_KEY", "re_env_123" );

        try
        {
            var opt = new ResendClientOptions();
            var client = ResendClient.Create( opt );

            Assert.NotNull( client );
        }
        finally
        {
            Environment.SetEnvironmentVariable( "RESEND_API_KEY", null );
        }
    }


    [Fact]
    public void Create_NoArg_NoEnvVar_ThrowsInvalidOperationException()
    {
        Environment.SetEnvironmentVariable( "RESEND_API_KEY", null );

        var ex = Assert.Throws<InvalidOperationException>( () => ResendClient.Create() );

        Assert.Contains( "RESEND_API_KEY", ex.Message );
    }
}
