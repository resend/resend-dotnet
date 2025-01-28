.NET SDK: ASP.NET - Controller API
=====================================================================

This example shows how to send emails from an ASP.NET application
using controllers.


How to run
---------------------------------------------------------------------

1. Set the `RESEND_APITOKEN` environment variable to your Resend API.
2. Edit the `From` and `To` in `Controllers/EmailController.cs` as necessary.
3. Run the console app with `dotnet run`.
4. Make a `GET` request to `http://localhost:5000/email/send`

```bash
> set RESEND_APITOKEN=re_8m9gwsVG_6n94KaJkJ42Yj6qSeVvLq9xF
> dotnet run
```
