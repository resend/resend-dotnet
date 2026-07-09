using System.Text.Json.Serialization;

namespace Resend.Webhooks;

/// <summary />
public class EmailEventData : IWebhookData
{
    /// <summary />
    [JsonPropertyName( "created_at" )]
    [JsonConverter( typeof( JsonUtcDateTimeConverter ) )]
    public DateTime MomentCreated { get; set; }

    /// <summary />
    [JsonPropertyName( "email_id" )]
    public Guid EmailId { get; set; }

    /// <summary>
    /// RFC Message-ID header value for the email.
    /// </summary>
    [JsonPropertyName( "message_id" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public string? MessageId { get; set; }

    /// <summary />
    [JsonPropertyName( "from" )]
    public EmailAddress From { get; set; } = default!;

    /// <summary />
    [JsonPropertyName( "to" )]
    public EmailAddressList To { get; set; } = default!;

    /// <summary />
    /// <remarks>
    /// Only set for <see cref="WebhookEventType.EmailReceived" />, otherwise is null.
    /// </remarks>
    [JsonPropertyName( "bcc" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public EmailAddressList? Bcc { get; set; } = default!;

    /// <summary />
    /// <remarks>
    /// Only set for <see cref="WebhookEventType.EmailReceived" />, otherwise is null.
    /// </remarks>
    [JsonPropertyName( "cc" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public EmailAddressList? Cc { get; set; } = default!;

    /// <summary />
    /// <remarks>
    /// Only set for <see cref="WebhookEventType.EmailReceived" />, otherwise is null.
    /// </remarks>
    [JsonPropertyName( "message_id" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public string? MessageId { get; set; }

    /// <summary />
    [JsonPropertyName( "subject" )]
    public string Subject { get; set; } = default!;

    /// <summary />
    [JsonPropertyName( "broadcast_id" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public string? BroadcastId { get; set; }

    /// <summary />
    [JsonPropertyName( "template_id" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public string? TemplateId { get; set; }

    /// <summary />
    [JsonPropertyName( "tags" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public Dictionary<string, string>? Tags { get; set; }

    /// <summary />
    /// <remarks>
    /// Only set for <see cref="WebhookEventType.EmailReceived" />, otherwise is null.
    /// </remarks>
    [JsonPropertyName( "attachments" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public List<EmailEventAttachment>? Attachments { get; set; }

    /// <summary />
    /// <remarks>
    /// Only set for <see cref="WebhookEventType.EmailClicked" />, otherwise is null.
    /// </remarks>
    [JsonPropertyName( "click" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public EmailClickData? Click { get; set; }

    /// <summary />
    /// <remarks>
    /// Only set for <see cref="WebhookEventType.EmailBounced" />, otherwise is null.
    /// </remarks>
    [JsonPropertyName( "bounce" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public EmailBounceData? Bounce { get; set; }

    /// <summary />
    /// <remarks>
    /// Only set for <see cref="WebhookEventType.EmailFailed"/>
    /// </remarks>
    [JsonPropertyName( "failed" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull  )]
    public EmailFailedData? Failed { get; set; }

    /// <summary />
    /// <remarks>
    /// Only set for <see cref="WebhookEventType.EmailSuppressed"/>
    /// </remarks>
    [JsonPropertyName( "suppressed" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public EmailSuppressedData? Suppressed { get; set; }
}


/// <summary />
public class EmailBounceData
{
    /// <summary />
    [JsonPropertyName( "message" )]
    public string Message { get; set; } = default!;

    /// <summary />
    /// <remarks>TODO: What are the possible values?</remarks>
    [JsonPropertyName( "subType" )]
    public string SubType { get; set; } = default!;

    /// <summary />
    /// <remarks>TODO: What are the possible values?</remarks>
    [JsonPropertyName( "type" )]
    public string Type { get; set; } = default!;

    /// <summary />
    [JsonPropertyName( "diagnosticCode" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public List<string>? DiagnosticCode { get; set; }
} 


/// <summary />
public class EmailClickData
{
    /// <summary />
    [JsonPropertyName( "ipAddress" )]
    public string IpAddress { get; set; } = default!;

    /// <summary />
    [JsonPropertyName( "link" )]
    public string Link { get; set; } = default!;

    /// <summary />
    [JsonPropertyName( "timestamp" )]
    [JsonConverter( typeof( JsonUtcDateTimeConverter ) )]
    public DateTime MomentClicked { get; set; }

    /// <summary />
    [JsonPropertyName( "userAgent" )]
    public string UserAgent { get; set; } = default!;
}


/// <summary />
public class EmailFailedData
{
    /// <summary />
    [JsonPropertyName( "reason" )]
    public string Reason { get; set; } = default!;
}


/// <summary />
public class EmailSuppressedData
{
    /// <summary />
    [JsonPropertyName( "message" )]
    public string Message { get; set; } = default!;

    /// <summary />
    /// <remarks>TODO: What are the possible values?</remarks>
    [JsonPropertyName( "type" )]
    public string Type { get; set; } = default!; 
}