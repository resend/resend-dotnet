namespace Resend
{
    public static class RateLimitExtensions
    {
        public static TimeSpan GetRetryAfter( this IHasRateLimit rateLimit )
        {
            return TimeSpan.FromSeconds( rateLimit.Limits?.RetryAfter ?? 0 );
        }

        public static async Task RetryAfterDelay( this IHasRateLimit rateLimit, Func<Task> action, CancellationToken cancellationToken = default )
        {
            await Task.Delay( rateLimit.GetRetryAfter(), cancellationToken );
            await action();
        }
    }
}