using System.Text.Json.Serialization;

namespace Resend.Payloads;

/// <summary>
/// Request object to update an API key.
/// </summary>
public class ApiKeyUpdateRequest
{
    /// <summary>
    /// Name of API key.
    /// </summary>
    [JsonPropertyName( "name" )]
    public string Name { get; set; } = default!;
}
