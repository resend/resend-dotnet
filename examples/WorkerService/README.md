.NET SDK: Worker Service
=====================================================================

This example shows how to send emails from a .NET Worker Service,
using a `BackgroundService` that runs alongside the host, sends an
email at a fixed interval and stops cleanly on shutdown.

No web server, no job framework: just the generic host and `IResend`.


How to run
---------------------------------------------------------------------

1. Set the `RESEND_APITOKEN` environment variable to your Resend API.
2. Edit the `From`, `To` and `Interval` in `appsettings.json` as necessary.
3. Run the worker with `dotnet run`.
4. Press `Ctrl+C` to stop it.

```bash
> export RESEND_APITOKEN=re_xxxxxxxxxxxxxxxxxxxxxxxxxxxxxxxx
> dotnet run
```


Why a scope?
---------------------------------------------------------------------

A `BackgroundService` is a singleton, while `IResend` is registered
as a typed `HttpClient` that depends on scoped options. Resolving it
straight into the worker's constructor fails in `Development` (scope
validation), and in other environments keeps the same `HttpClient`
alive for the lifetime of the process.

Instead, the worker creates a scope for each send:

```csharp
await using var scope = _scopeFactory.CreateAsyncScope();
var resend = scope.ServiceProvider.GetRequiredService<IResend>();
```
