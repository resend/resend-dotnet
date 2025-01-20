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

    /// <summary>
    /// Whether the client should retry the request when the rate limit is exceeded.
    /// </summary>
    public bool RetryOnRateLimit { get; set; } = true;
}
