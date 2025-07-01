resend sender for FluentEmail
==========================================================================

[![CI](https://github.com/resend/resend-dotnet/workflows/CI/badge.svg)](https://github.com/resend/resend-dotnet/actions)
[![NuGet](https://img.shields.io/nuget/vpre/resend.fluentemail.svg?label=NuGet)](https://www.nuget.org/packages/Resend.FluentEmail/)
[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](https://opensource.org/licenses/MIT)

Send emails using [FluentEmail](https://github.com/lukencode/FluentEmail) .NET API,
using [Resend](https://resend.com) as the underlying sender.


Functionality
--------------------------------------------------------------------------

The Resend sender supports the following features:

* Display name in From, To, Cc, Bcc, and ReplyTo addresses
* HTML and non-HTML bodies
* Priority enumerate
* Headers
* Tags
* Attachments


Installing via NuGet
--------------------------------------------------------------------------

Package is published in the [NuGet](https://www.nuget.org/packages/Resend.FluentEmail/) gallery.

From the command-line:

```
> dotnet add package Resend.FluentEmail
```

From within Visual Studio using Package Manager Console:

```
PM> Install-Package Resend.FluentEmail
```


Getting started
--------------------------------------------------------------------------

In the startup of your application, configure the DI container as follows:

```
using FluentEmail.Core.Interfaces;
using Resend;
using Resend.FluentEmail;

builder.Services.AddOptions();
builder.Services.AddHttpClient<ResendClient>();
builder.Services.Configure<ResendClientOptions>( o =>
{
    o.ApiToken = Environment.GetEnvironmentVariable( "RESEND_APITOKEN" )!;
} );
builder.Services.AddTransient<IResend, ResendClient>();
builder.Services.AddTransient<ISender, ResendSender>();
```

Send the email in the FluentEmail way, using the injected `ISender`:

```
using FluentEmail.Core.Interfaces;

public class FeatureImplementation
{
    private readonly ISender _sender;


    public FeatureImplementation( ISender sender )
    {
        _sender_ = sender;
    }


    public async Task Execute()
    {
        var email = Email
            .From( "onboarding@resend.dev" )
            .To( "delivered@resend.dev" )
            .Subject( "Hello!" )
            .Body( "<div><strong>Greetings<strong> 👋🏻 from .NET</div>", true )
            .PlaintextAlternativeBody( "Greetigs from .NET" );

        email.Sender = _sender;
        var response = await email.SendAsync();
    }
}
```
