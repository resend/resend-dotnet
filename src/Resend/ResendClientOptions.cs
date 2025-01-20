using Microsoft.Extensions.Options;

namespace Resend;

/// <summary />
public class ResendClientOptions
{
    /// <summary>
    /// Base URL for Resend API.
    /// </summary>
    /// <remarks>
    /// Default is `https://api.resend.com`.
    /// </remarks>
    public string ApiUrl { get; set; } = "https://api.resend.com";

    /// <summary>
    /// API key token.
    /// </summary>
    public string ApiToken { get; set; } = default!;

    /// <summary>
    /// Whether exceptions should be thrown when the API invocation
    /// fails (for whatever reason).
    /// </summary>
    public bool ThrowExceptions { get; set; } = true;
}


/// <summary />
internal class OptionsSnapshot<T> : IOptionsSnapshot<T>
    where T : class
{
    private readonly T _value;


    /// <summary />
    internal OptionsSnapshot( T value )
    {
        _value = value;
    }


    /// <inheritdoc />
    public T Value => _value;


    /// <inheritdoc />
    public T Get( string? name )
    {
        return _value;
    }
}
