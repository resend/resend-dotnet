using System.Text.Json.Serialization;

namespace Resend.Webhooks;

/// <summary />
public class ContactEventData : IWebhookData
{
    /// <summary />
    [JsonPropertyName( "id" )]
    public Guid ContactId { get; set; }

    /// <summary />
    [JsonPropertyName( "segment_ids" )]
    [JsonIgnore( Condition = JsonIgnoreCondition.WhenWritingNull )]
    public List<Guid>? SegmentIds { get; set; }

    /// <summary />
    [JsonPropertyName( "created_at" )]
    [JsonConverter( typeof( JsonUtcDateTimeConverter ) )]
    public DateTime MomentCreated { get; set; }

    /// <summary />
    [JsonPropertyName( "updated_at" )]
    [JsonConverter( typeof( JsonUtcDateTimeConverter ) )]
    public DateTime MomentUpdated { get; set; }

    /// <summary />
    [JsonPropertyName( "email" )]
    public string Email { get; set; } = default!;

    /// <summary />
    [JsonPropertyName( "first_name" )]
    public string FirstName { get; set; } = default!;

    /// <summary />
    [JsonPropertyName( "last_name" )]
    public string LastName { get; set; } = default!;

    /// <summary />
    [JsonPropertyName( "unsubscribed" )]
    public bool IsUnsubscribed { get; set; }
}