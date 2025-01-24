.NET SDK: Async Temporal
=====================================================================

This example shows how to send emails asynchronously as an background
job using [Temporal](https://temporal.io/).


Pre-requisites
---------------------------------------------------------------------

1. Download the [Temporal CLI](https://docs.temporal.io/cli) to `temporal-cli` folder
2. Run `temporal-cli/go` command, which will start Temporal server with UI

```bash
> go
CLI 1.2.0 (Server 1.26.2, UI 2.34.0)

Server:  localhost:7233
UI:      http://localhost:8233
Metrics: http://localhost:63482/metrics
```


How to run
---------------------------------------------------------------------

1. Set the `RESEND_APITOKEN` environment variable to your Resend API.
2. Edit the initial values of `From` and `To` in `Controllers/TestController.cs` as necessary.
3. Run the web app with `dotnet run`.
4. Open browser at `http://localhost:5004/test/` to create job
5. Open browser at `http://localhost:8233` to view Temporal UI and view the workflow

```bash
> set RESEND_APITOKEN=re_8m9gwsVG_6n94KaJkJ42Yj6qSeVvLq9xF
> dotnet run
```


In order to simulate errors, you can change the implementation of
`IResend` to `ResendMockClient`. This implementation will always fail
to send the email the first two times the job runs.

```csharp
// From
builder.Services.AddTransient<IResend, ResendClient>();

// To
builder.Services.AddTransient<IResend, ResendMockClient>();
```


Considerations
---------------------------------------------------------------------

As per [documentation pertaining to activities](https://docs.temporal.io/develop/dotnet/core-application#develop-activity):

> A single argument is limited to a maximum size of 2 MB. And the total
> size of a gRPC message, which includes all the arguments, is limited
> to a maximum of 4 MB.

As such, if the email message has attachments this example may not work
if the 2 MB limit is reached.

One alternative of setting the value of `Content` property, would be to
re-use the `Path` property with special value such that the `EmailSendActivity`
can then load the content prior to sending email. See `Temporal/EmailSendActivity.cs`
for a skeleton of such code.
