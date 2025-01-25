.NET SDK: Render Razor
=====================================================================

This example shows how to send emails, using [Fluid](https://github.com/sebastienros/fluid)
as rendering engine -- which implements [Liquid](https://shopify.github.io/liquid/) template language.


How to run
---------------------------------------------------------------------

1. Set the `RESEND_APITOKEN` environment variable to your Resend API.
2. Edit the initial values of `From` and `To` in `Controllers/EmailController.cs` as necessary.
3. Run the web app with `dotnet run`.
4. Open browser at `http://localhost:5006/email/render` to render an email
5. Open browser at `http://localhost:5006/email/send` to send rendered email

```bash
> set RESEND_APITOKEN=re_8m9gwsVG_6n94KaJkJ42Yj6qSeVvLq9xF
> dotnet run
```


References
---------------------------------------------------------------------

* VsCode plugin for `.liquid` files: [Liquid](https://marketplace.visualstudio.com/items?itemName=sissel.shopify-liquid)
* Overview: [An Overview of Liquid: Shopify's Templating Language](https://www.shopify.com/partners/blog/115244038-an-overview-of-liquid-shopifys-templating-language#)
