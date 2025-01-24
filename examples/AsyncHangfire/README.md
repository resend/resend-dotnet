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
| 3    | Hangfire server will pick up from queue and invoke `EmailSendJob.BackgroundSendAsync` method.
| 4    | If an exception is thrown, Hangfire server will retry the job.
| 5    | Otherwise, job will be marked as Succeeded.


Considerations
---------------------------------------------------------------------

As per the [best practices](https://docs.hangfire.io/en/latest/best-practices.html) documentation,
it is advised to keep the payload size small.

> Method invocation (i.e. a job) is serialized during the background job
> creation process. Arguments are converted into JSON strings using the
> TypeConverter class. If you have complex entities and/or large objects;
> including arrays, it is better to place them into a database, and then pass
> only their identities to the background job.

As such, it is desireable to avoid inline attachments, since the
entire `byte[]` would be stored in the Hangfire job database.

One alternative of setting the value of `Content` property, would be to
re-use the `Path` property with special value such that the `EmailSendJob`
can then load the content prior to sending email. See `Jobs/EmailSendJob.cs`
for a skeleton of such code.
