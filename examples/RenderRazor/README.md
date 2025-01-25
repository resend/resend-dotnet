.NET SDK: Render Razor
=====================================================================

This example shows how to send emails, by using [Razor](https://learn.microsoft.com/en-us/aspnet/core/mvc/views/razor?view=aspnetcore-9.0)
as the rendering engine.


How to run
---------------------------------------------------------------------

1. Set the `RESEND_APITOKEN` environment variable to your Resend API.
2. Edit the initial values of `From` and `To` in `Controllers/EmailController.cs` as necessary.
3. Run the web app with `dotnet run`.
4. Open browser at `http://localhost:5005/email/render` to render an email
5. Open browser at `http://localhost:5005/email/send` to send rendered email

```bash
> set RESEND_APITOKEN=re_8m9gwsVG_6n94KaJkJ42Yj6qSeVvLq9xF
> dotnet run
```


References
---------------------------------------------------------------------

* [Generate HTML Email from Razor View Page with a Strongly Typed Model](https://stackoverflow.com/questions/78085196/generate-html-email-from-razor-view-page-with-a-strongly-typed-model)
* [Render Razor components outside of ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/blazor/components/render-components-outside-of-aspnetcore?view=aspnetcore-8.0)
