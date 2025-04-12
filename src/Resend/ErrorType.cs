﻿using System.Text.Json.Serialization;

namespace Resend;

/// <summary />
[JsonConverter( typeof( JsonStringEnumValueConverter<ErrorType> ) )]
public enum ErrorType
{
    /// <summary>
    /// An error with one or more fields.
    /// </summary>
    [JsonStringValue( "validation_error" )]
    ValidationError,

    /// <summary>
    /// Missing API key in the authorization header.
    /// </summary>
    [JsonStringValue( "missing_api_key" )]
    MissingApiKey,

    /// <summary>
    /// Missing API key in the authorization header.
    /// </summary>
    [JsonStringValue( "restricted_api_key" )]
    RestrictedApiKey,

    /// <summary>
    /// Requested endpoint does not exist.
    /// </summary>
    [JsonStringValue( "not_found" )]
    NotFound,

    /// <summary>
    /// Attachment must have either a `content` or `path` property.
    /// </summary>
    [JsonStringValue( "invalid_attachment" )]
    InvalidAttachment,

    /// <summary>
    /// Body is missing one or more required fields.
    /// </summary>
    [JsonStringValue( "missing_required_field" )]
    MissingRequiredField,

    /// <summary>
    /// You have reached your daily email sending quota.
    /// </summary>
    [JsonStringValue( "daily_quota_exceeded" )]
    DailyQuotaExceeded,

    /// <summary>
    /// Too many requests.
    /// </summary>
    [JsonStringValue( "rate_limit_exceeded" )]
    RateLimitExceeded,

    /// <summary>
    /// Resend found a security issue with the request.
    /// </summary>
    [JsonStringValue( "security_error" )]
    SecurityError,

    /// <summary>
    /// An unexpected error occurred.
    /// </summary>
    [JsonStringValue( "application_error" )]
    ApplicationError,

    /// <summary>
    /// Invalid value provided as Idempotency Key, must be between 1-256 chars.
    /// </summary>
    /// <remarks>
    /// Request must be retried using a valid idempotency key.
    /// </remarks>
    [JsonStringValue( "invalid_idempotency_key" )]
    InvalidIdempotencyKey,

    /// <summary>
    /// Mismatch between idempotency key and request payload.
    /// </summary>
    /// <remarks>
    /// Retry request must be identical: the idempotency key and payloads must be identical.
    /// </remarks>
    [JsonStringValue( "invalid_idempotent_request" )]
    InvalidIdempotentRequest,

    /// <summary>
    /// Idempotency key matches request which is still in progress.
    /// </summary>
    /// <remarks>
    /// Past request will continue executing, current request was ignored. Moreover, in case
    /// the original request fails to execute, it is still possible to retry in the future.
    /// </remarks>
    [JsonStringValue( "concurrent_idempotent_requests" )]
    ConcurrentIdempotentRequests,



    /// <summary>
    /// Failed to send the HTTP request.
    /// </summary>
    /// <remarks>
    /// Not returned by API, part of the SDK.
    /// </remarks>
    [JsonStringValue( "http_send_failed" )]
    HttpSendFailed,

    /// <summary>
    /// Expected JSON response was missing.
    /// </summary>
    /// <remarks>
    /// Not returned by API, part of the SDK.
    /// </remarks>
    [JsonStringValue( "missing_response" )]
    MissingResponse,

    /// <summary>
    /// Failed to deserialize JSON response.
    /// </summary>
    /// <remarks>
    /// Not returned by API, part of the SDK.
    /// </remarks>
    [JsonStringValue( "deserialization" )]
    Deserialization,
}