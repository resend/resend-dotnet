using System;
using System.Threading.Tasks;
using Resend;

public class App
{
    private readonly IResend _resend;

    public App(IResend resend)
    {
        _resend = resend;
    }

    public async Task Run()
    {
        var message = new EmailMessage
        {
            From = "you@example.com",
            To = { "delivered@resend.dev" },
            Subject = "Hello from Console with DI",
            HtmlBody = "<p>Email using <strong>Resend .NET SDK</strong> with dependency injection</p>"
        };

        var response = await _resend.EmailSendAsync(message);

        if (response.Success)
        {
            Console.WriteLine($"Email sent! ID: {response.Content}");
        }
        else
        {
            Console.WriteLine($"Failed: {response.Exception?.Message}");
        }
    }
}
