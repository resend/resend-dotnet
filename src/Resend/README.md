resend
==========================================================================

[![CI](https://github.com/resend/resend-dotnet/workflows/CI/badge.svg)](https://github.com/resend/resend-dotnet/actions)
[![NuGet](https://img.shields.io/nuget/vpre/resend.svg?label=NuGet)](https://www.nuget.org/packages/Resend/)
[![License: MIT](https://img.shields.io/badge/License-MIT-blue.svg)](https://opensource.org/licenses/MIT)

.NET client for [Resend](https://resend.com), an email API, written in C#.


Functionality
--------------------------------------------------------------------------

The `ResendClient` supports the following objects (and methods):

* Email (Send, Batch, Retrieve, Reschedule, Cancel)
* Domain (List, Add, Retrieve, Update, Verify, Delete)
* API key (List, Create, Delete)
* Audience (List, Add, Retrieve, Delete)
* Contact (List, Add, Retrieve, Update, Delete)
* Broadcast (List, Add, Retrieve, Update, Send, Schedule, Delete)


Installing via NuGet
--------------------------------------------------------------------------

Package is published in the [NuGet](https://www.nuget.org/packages/Resend/) gallery.

From the command-line:

```
> dotnet add package Resend
```

From within Visual Studio using Package Manager Console:

```
PM> Install-Package Resend
```


Getting started
--------------------------------------------------------------------------

In the startup of your application, configure the DI container as follows:

```
using Resend;

builder.Services.AddOptions();
builder.Services.AddHttpClient<ResendClient>();
builder.Services.Configure<ResendClientOptions>( o =>
{
    o.ApiToken = Environment.GetEnvironmentVariable( "RESEND_APITOKEN" )!;
} );
builder.Services.AddTransient<IResend, ResendClient>();
```


Send an email using the injected `IResend` instance:

```
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
        message.Subject = "Hello!";
        message.HtmlBody = "<div><strong>Greetings<strong> 👋🏻 from .NET</div>";

        await _resend.EmailSendAsync( message );
    }
}
```
