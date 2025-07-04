Resend .NET SDK: Documentation
==========================================================================

While the SDK should be fairly self-explanatory, please find some notes
below on some features / functionality which may not be.


Email display names
--------------------------------------------------------------------------

When sending email, you can send from/to an email address only, or you can
also add a display name which may be used by the mail agent/program.

The idiomatic way of setting the display name is to leverage the `EmailAddress`
class as follows:

```csharp
var from = new EmailAddress()
{
    Email = "onboarding@resend.dev",
    DisplayName = "Onboarding",
};

var message = new EmailMessage()
{
    From = from,
};
```


Attachments: Binary vs Text
--------------------------------------------------------------------------

Due to Resend API design, you will need to construct the `EmailAttachment`
differently if the attachment is text. Please note how the `Content`
property is of type `ByteArrayOrString`.

```csharp
// Binary attachment
var bin = new EmailAttachment()
{
    Filename = "image.png",
    Content = File.ReadAllBytes( "image.png" ),
    ContentType = "image/png",
};

// Text attachment
var txt = new EmailAttachment()
{
    Filename = "invite.ics",
    Content = File.ReadAllText( "invite.ics" ),
    ContentType = "text/calendar",
};
```


Exceptions vs Errors
--------------------------------------------------------------------------

By default, the SDK will throw exceptions in case of connection issues,
invalid payloads, rate-limiting, etc. If you'd rather perform error
handling, configure the `ResendClient` as follows:

```csharp
using Resend;

builder.Services.AddOptions();
builder.Services.AddHttpClient<ResendClient>();
builder.Services.Configure<ResendClientOptions>( o =>
{
    o.ApiToken = Environment.GetEnvironmentVariable( "RESEND_APITOKEN" )!;
    o.ThrowExceptions = false;
} );
builder.Services.AddTransient<IResend, ResendClient>();
```

When using the SDK with exceptions off, you are responsible for checking
the `Success` flag on the `ResendResponse` object.


Transient Errors
--------------------------------------------------------------------------

The `ResendException` class has an `IsTransient` property, which you can
use to determine whether a retry (using the same payload) is possible,
or not. Consider the following conceptual code-block when implementing
a function which returns whether or not a retry should be attempted.

```csharp
// With exception
try
{
    await _resend.EmailAsync( ... )
}
catch ( ResendException ex ) : when ( ex.IsTransient = true )
{
    _logger.LogWarning( ex, "Failed with {ErrorType}, will retry", ex.ErrorType );
    return true;
}
catch ( ResendException ex )
{
    _logger.LogError( ex, "Failed with {ErrorType}, giving up", ex.ErrorType );
    return false;
}
```

Please note that an invalid API key is considered a transient error,
since the error is a result of configuration (which can be corrected)
rather than with the payload (which probably cannot be changed).


Limits
--------------------------------------------------------------------------

If the limits are exceeded, the SDK will return one of the following
error types:

* `RateLimitExceeded`, if the rate-limit was exceeded and you should
  throttle consequent requests;
* `DailyQuotaExceeded`, if the daily quota has been exceeded and no
  further attempts for the (delivery region) day should be done.

At the present moment, the Resend API does not support throttling email
sending on your behalf: this needs to be implemented in your codebase.

If your application is expected to deliver a high volume of emails, it
would be best to implement an asynchronous mechanism (e.g. queue) and
react not only to the two error codes (when limits are exceeded) but also
to check the value of the `.Limits` property (of the `ResendRateLimit` class).
