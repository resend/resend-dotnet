.NET SDK: ASP.NET - Webhook
=====================================================================

This example shows how to receive webhook events from Resend.


How to run
---------------------------------------------------------------------

1. Create a Webhook on the Resend dashboard
   - Go to the [Webhooks page](https://resend.com/webhooks)
   - The endpoint URL needs to be HTTPS (You can use [Ngrok](https://ngrok.com/) to point to the dotnet app)
   - Copy the Webhook signing secret after creating the Webhook

2. Set the `RESEND_WEBHOOK_SECRET` environment variable to your Webhook


1. Set the `RESEND_APITOKEN` environment variable to your Resend API.
2. Edit the `From` and `To` in the `Program.cs` as necessary.
3. Run the console app with `dotnet run`.
4. Make a `POST` request to `http://localhost:5007/email/send`

```bash
set RESEND_WEBHOOK_SECRET=whsec_alJairX6NuQxWqjdF8PIQd48gh2IQOHl
set RESEND_APITOKEN=re_8m9gwsVG_6n94KaJkJ42Yj6qSeVvLq9xF
dotnet run
```