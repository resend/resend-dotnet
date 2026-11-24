//namespace Resend.Webhooks;

///// <summary />
//public class WebhookSigner
//{
//    private readonly string _secret;

//    /// <summary />
//    public WebhookSigner( string secret )
//    {
//        _secret = secret;
//    }


//    /// <summary />
//    public string Sign( string messageId, DateTimeOffset dateTimeOffset, string payload )
//    {
//        var wh = new Svix.Webhook( _secret );

//        return wh.Sign( messageId, dateTimeOffset, payload );
//    }
//}