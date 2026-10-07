Resend .NET SDK
==========================================================================

[![CI](https://github.com/resend/resend-dotnet/workflows/CI/badge.svg)](https://github.com/resend/resend-dotnet/actions)
[![NuGet](https://img.shields.io/nuget/vpre/resend.svg?label=NuGet)](https://www.nuget.org/packages/Resend/)
[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](https://opensource.org/licenses/MIT)

.NET library for the Resend API.


Install
--------------------------------------------------------------------------

```
> dotnet add package Resend
```

```
PM> Install-Package Resend
```

The packages target .NET 8 and .NET 10. Apps on .NET 9 use the .NET 8 build.


Examples
--------------------------------------------------------------------------

Send email with:

* [ASP.NET - Minimal API](https://github.com/resend/resend-dotnet/tree/master/examples/WebMinimalApi) - Send email from an API
* [ASP.NET - Razor](https://github.com/resend/resend-dotnet/tree/master/examples/WebRazor) - Send email from a Razor web application
* [Console app](https://github.com/resend/resend-dotnet/tree/master/examples/ConsoleNoDi) - Send email from console app (without dependency injection)
* [Async - Hangfire](https://github.com/resend/resend-dotnet/tree/master/examples/AsyncHangfire) - Send email as a background job using [Hangfire](https://www.hangfire.io/)
* [Async - Temporal](https://github.com/resend/resend-dotnet/tree/master/examples/AsyncTemporal) - Send email in durable workflow using [Temporal](https://temporal.io/)
* [Render - Razor](https://github.com/resend/resend-dotnet/tree/master/examples/RenderRazor) - Render an HTML body using Razor views
* [Render - Liquid](https://github.com/resend/resend-dotnet/tree/master/examples/RenderLiquid) - Render an HTML body using [Fluid](https://github.com/sebastienros/fluid), a [Liquid](https://shopify.github.io/liquid/) template language


Setup
--------------------------------------------------------------------------

First, you need to get an API key, which is available in the
[Resend Dashboard](https://resend.com/).

In the startup of your application, configure the DI container as follows:

```csharp
using Resend;

builder.Services.AddResend( o => 
{
    o.ApiToken = Environment.GetEnvironmentVariable( "RESEND_APITOKEN" )!;
} );
```

You can then use the injected `IResend` instance to send emails.


Usage
--------------------------------------------------------------------------

Send your first email:

```csharp
using Resend;

public class FeatureImplementation
{
    private readonly IResend _resend;


    public FeatureImplementation( IResend resend )
    {
        _resend = resend;
    }


    public async Task Execute()
    {
        var message = new EmailMessage();
        message.From = "onboarding@resend.dev";
        message.To.Add( "delivered@resend.dev" );
        message.Subject = "hello world";
        message.TextBody = "it works!";

        await _resend.EmailSendAsync( message );
    }
}
```


Send email using HTML
--------------------------------------------------------------------------

Send an email custom HTML content:

```csharp
using Resend;

public class FeatureImplementation
{
    private readonly IResend _resend;


    public FeatureImplementation( IResend resend )
    {
        _resend = resend;
    }


    public async Task Execute()
    {
        var message = new EmailMessage();
        message.From = "onboarding@resend.dev";
        message.To.Add( "delivered@resend.dev" );
        message.Subject = "hello world";
        message.HtmlBody = "<strong>it works!</strong>";

        await _resend.EmailSendAsync( message );
    }
}
```


Native AOT
--------------------------------------------------------------------------

The `Resend`, `Resend.Webhooks` and `Resend.FluentEmail` packages are
compatible with trimming and Native AOT publishing.

When publishing with Native AOT, values assigned to `object` properties
-- such as `ContactPropertyData.DefaultValue`, `ContactPropertyUpdateData.DefaultValue`,
`ContactData.Properties` and `TemplateVariable.Default` -- may be any
primitive (string, bool, all integer types, floating point, decimal),
enums (written as their underlying number -- custom enum converters are
not applied), `byte[]`, `DateTime`, `DateOnly`, `TimeOnly`, `TimeSpan`,
`Guid`, `JsonElement`, `JsonNode`, arrays, lists/`IEnumerable`, and
dictionaries. Arbitrary POCOs and anonymous types are only supported in
JIT-compiled applications.


License
--------------------------------------------------------------------------

MIT License
