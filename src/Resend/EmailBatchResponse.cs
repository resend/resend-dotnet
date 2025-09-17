using System.Text.Json.Serialization;

namespace Resend;

/// <summary />
public class EmailBatchResponse
{
    /// <summary>
    /// List of message identifiers.
    /// </summary>
    [JsonPropertyName( "data" )]
    public List<EmailBatchReceipt> Data { get; set; } = default!;

    /// <summary>
    /// List of validation errors.
    /// </summary>
    /// <remarks>
    /// Only present in permissive validation mode, otherwise it is null.
    /// </remarks>
    [JsonPropertyName( "errors" )]
    public List<EmailBatchError>? Errors { get; set; }
}


/// <summary />
public class EmailBatchReceipt
{
    /// <summary>
    /// Message identifier.
    /// </summary>
    [JsonPropertyName( "id" )]
    public Guid Id { get; set; } = default!;
}


/// <summary />
public class EmailBatchError
{
    /// <summary>
    /// Index of the email in the batch request
    /// </summary>
    [JsonPropertyName( "index" )]
    public int Index { get; set; }

    /// <summary>
    /// Error message identifying the validation error
    /// </summary>
    [JsonPropertyName( "message" )]
    public string Message { get; set; } = default!;
}