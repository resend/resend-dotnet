using System.Text.Json;
using System.Text.Json.Serialization;

namespace Resend;

/// <summary>
/// An entry from the API request logs.
/// </summary>
public class Log
{
    /// <summary>
    /// Log identifier.
    /// </summary>
    [JsonPropertyName( "id" )]
    public Guid Id { get; set; }

    /// <summary>
    /// When the request was logged.
    /// </summary>
    [JsonPropertyName( "created_at" )]
    [JsonConverter( typeof( JsonUtcDateTimeConverter ) )]
    public DateTime MomentCreated { get; set; }

    /// <summary>
    /// API path that was invoked (for example <c>/emails</c>).
    /// </summary>
    [JsonPropertyName( "endpoint" )]
    public string Endpoint { get; set; } = default!;

    /// <summary>
    /// HTTP method (for example GET or POST).
    /// </summary>
    [JsonPropertyName( "method" )]
    public string HttpMethod { get; set; } = default!;

    /// <summary>
    /// HTTP status code returned for the request.
    /// </summary>
    [JsonPropertyName( "response_status" )]
    public int ResponseStatus { get; set; }

    /// <summary>
    /// User-Agent header sent with the request.
    /// </summary>
    [JsonPropertyName( "user_agent" )]
    public string UserAgent { get; set; } = default!;

    /// <summary>
    /// Request body as logged. Shape depends on the API operation. Only present when retrieving a single log.
    /// </summary>
    [JsonPropertyName( "request_body" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public JsonElement? RequestBody { get; set; }

    /// <summary>
    /// Response body as logged. Shape depends on the API operation. Only present when retrieving a single log.
    /// </summary>
    [JsonPropertyName( "response_body" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public JsonElement? ResponseBody { get; set; }
}
