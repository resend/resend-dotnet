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

    /// <summary />
    [JsonPropertyName( "from" )]
    public EmailAddress From { get; set; } = default!;

    /// <summary />
    [JsonPropertyName( "to" )]
    public EmailAddressList To { get; set; } = default!;

    /// <summary />
    [JsonPropertyName( "subject" )]
    public string Subject { get; set; } = default!;

    /// <summary />
    [JsonPropertyName( "tags" )]
    public List<EmailTag>? Tags { get; set; }

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
}


/// <summary />
public class EmailBounceData
{
    /// <summary />
    [JsonPropertyName( "message" )]
    public string Message { get; set; } = default!;

    /// <summary />
    [JsonPropertyName( "type" )]
    public EmailBounceType Type { get; set; }

    /// <summary />
    [JsonPropertyName( "subType" )]
    public EmailBounceSubType SubType { get; set; } = default!;
}


/// <summary />
[JsonConverter( typeof( JsonStringEnumConverter<EmailBounceType> ) )]
public enum EmailBounceType
{
    /// <summary />
    Permanent,
}


/// <summary />
[JsonConverter( typeof( JsonStringEnumConverter<EmailBounceSubType> ) )]
public enum EmailBounceSubType
{
    /// <summary />
    Suppressed,
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