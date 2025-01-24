.NET SDK: Async Hangfire
=====================================================================

This example shows how to send emails asynchronously as an background
job using [Hangfire](https://www.hangfire.io/).


How to run
---------------------------------------------------------------------

1. Set the `RESEND_APITOKEN` environment variable to your Resend API.
2. Edit the initial values of `From` and `To` in `Controllers/TestController.cs` as necessary.
3. Run the web app with `dotnet run`.
4. Open browser at `http://localhost:5003/jobs/` to view jobs
5. Open browser at `http://localhost:5003/test/` to create job

```bash
> set RESEND_APITOKEN=re_8m9gwsVG_6n94KaJkJ42Yj6qSeVvLq9xF
> dotnet run
```


In order to simulate errors, you can change the implementation of
`IResend` to `ResendMockClient`. This implementation will always fail
to send the email the first two times the job runs.

```
// From
builder.Services.AddTransient<IResend, ResendClient>();

// To
builder.Services.AddTransient<IResend, ResendMockClient>();
```


Explained
---------------------------------------------------------------------

| Step | Description
|------|-------------------------------------------------------------
| 1    | When hitting `/test`, the `Get` method of `Controllers/TestController.cs` runs.
| 2    | `BackgroundJob.Enqueue` will create an instance of the email send job.
| 3    | When viewing `/jobs`, one job will be enqueued for execution.
| 4    | Refresh `/jobs`, and the job will change to scheduled.
| 5    | Refresh `/jobs`, and the job will change to processing.



Considerations
---------------------------------------------------------------------

As per the [best practices](https://docs.hangfire.io/en/latest/best-practices.html) documentation,
it is advised to keep the 
