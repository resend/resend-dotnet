# Webhook Workflow Example

This example demonstrates a complete webhook lifecycle using the Resend .NET SDK:

1. Create a webhook
2. Retrieve the webhook by ID
3. Update the webhook (endpoint, events, and status)
4. List all webhooks
5. Delete the webhook
6. Verify deletion

## Prerequisites

- .NET 8.0 SDK
- Resend API key

## Running the Example

1. Set your Resend API key as an environment variable:

```bash
export RESEND_API_KEY=re_your_api_key_here
```

2. Run the example:

```bash
cd examples/WebhookWorkflow
dotnet run
```

## Output

The script will output JSON-formatted responses from each API call, showing:
- The created webhook with its ID and signing secret
- The retrieved webhook details
- The list of all webhooks
- Confirmation of deletion

## Note

This example uses a test endpoint URL (`https://example.com/webhook`). In production, you would use your actual webhook endpoint that can receive and process webhook events.
