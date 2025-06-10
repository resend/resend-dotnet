Resend .NET SDK: Examples
==========================================================================

| Example  | Port | Description
|----------|------|-------------------------------------------------------
| [AsyncHangfire](https://github.com/resend/resend-dotnet/tree/master/examples/AsyncHangfire) | 5003 | Send email as a background job using [Hangfire](https://www.hangfire.io/)
| [AsyncTemporal](https://github.com/resend/resend-dotnet/tree/master/examples/AsyncTemporal) | 5004 | Send email in durable workflow using [Temporal](https://temporal.io/)
| [ConsoleCalendar](https://github.com/resend/resend-dotnet/tree/master/examples/ConsoleCalendar) | n/a  | Send email iCalendar attachment from console app
| [ConsoleNoDi](https://github.com/resend/resend-dotnet/tree/master/examples/ConsoleNoDi)     | n/a  | Send email from console app (without dependency injection)
| [RenderLiquid](https://github.com/resend/resend-dotnet/tree/master/examples/RenderLiquid)   | 5006 | Render an HTML body using [Fluid](https://github.com/sebastienros/fluid), a [Liquid](https://shopify.github.io/liquid/) template language
| [RenderRazor](https://github.com/resend/resend-dotnet/tree/master/examples/RenderRazor)     | 5005 | Render an HTML body using Razor views
| [WebControllerApi](https://github.com/resend/resend-dotnet/tree/master/examples/WebControllerApi) | 5000 | Send email from a controller
| [WebMinimalApi](https://github.com/resend/resend-dotnet/tree/master/examples/WebMinimalApi) | 5001 | Send email from a (minimal) API
| [WebRazor](https://github.com/resend/resend-dotnet/tree/master/examples/WebRazor)           | 5002 | Send email from a Razor form


Async
--------------------------------------------------------------------------

Sending emails through an API can occasionally fail due to transient issues
such as network interruptions, temporary outages on the email provider's
servers, or rate-limiting constraints. These failures, while often
short-lived, can disrupt critical workflows if not handled properly.

To ensure reliability, libraries that support durable activity execution
are essential. These libraries provide built-in retry mechanisms, backoff
strategies, and error handling to manage transient failures effectively,
ensuring the email is eventually sent without requiring manual intervention.
By abstracting these complexities, they help maintain consistent and
reliable email delivery in applications.

Below is a (non-comprehensive) list of such libraries / products
compatible with a .NET environment.


* [Hangfire](https://www.hangfire.io/) (LGPL 3.0) - [📂 Example](https://github.com/resend/resend-dotnet/tree/master/examples/AsyncHangfire)

> A library for processing background jobs in .NET applications. It
> supports retries, scheduled tasks, and persistent queues, ensuring durability.


* [Temporal](https://temporal.io/) (MIT) - [📂 Example](https://github.com/resend/resend-dotnet/tree/master/examples/AsyncTemporal)

> Platform for building scalable, durable, and fault-tolerant workflows
> and microservices. It enables developers to manage the state and execution
> of long-running workflows with built-in support for retries, timeouts,
> and error handling. 


* [Quartz.NET](https://www.quartz-scheduler.net/) (Apache 2.0)

> A flexible job scheduling library for .NET applications, useful for
> retrying and running tasks at scheduled intervals.


Render: Template Rendering Engines
--------------------------------------------------------------------------

Writing HTML directly in C# can be cumbersome and error-prone because the
language is not designed for handling raw HTML effectively. Embedding
HTML as strings in C# code can make the codebase harder to read, maintain,
and debug, especially when the HTML structure becomes complex or dynamic.
It often leads to messy concatenation, increases the risk of syntax errors,
and creates challenges in managing reusable components or separating logic
from presentation.

Below is a (non-comprehensive) list of template rendering engines in
.NET.


* [Razor](https://learn.microsoft.com/en-us/aspnet/core/razor-pages/?view=aspnetcore-9.0&tabs=visual-studio) (Apache 2.0) - [📂 Example](https://github.com/resend/resend-dotnet/tree/master/examples/RenderRazor)

> Razor is a lightweight, syntax-efficient templating engine in ASP.NET.



* [Fluid](https://github.com/sebastienros/fluid) (MIT) - [📂 Example](https://github.com/resend/resend-dotnet/tree/master/examples/RenderLiquid)

> Fluid is an open-source .NET template engine based on the Liquid template
> language. It's a secure template language that is also very accessible
> for non-programmer audiences.


* [Markdig](https://github.com/xoofx/markdig) (BSD-2-Clause)

> Markdig is a fast, powerful, [CommonMark](http://commonmark.org/) compliant, extensible [Markdown](https://www.markdownguide.org/)
> processor for .NET.


* [Scriban](https://github.com/scriban/scriban) (BSD-2-Clause)

> Scriban is a fast, powerful, safe and lightweight scripting language and
> engine for .NET, which was primarily developed for text templating with
> a compatibility mode for parsing liquid templates.


* [Handlebars.Net](https://github.com/Handlebars-Net/Handlebars.Net) (MIT)

> [Handlebars](https://handlebarsjs.com/) templates for .NET. Handlebars.Net
> doesn't use a scripting engine to run a Javascript library - it compiles
> Handlebars templates directly to IL bytecode. It also mimics the JS
> library's API as closely as possible.
