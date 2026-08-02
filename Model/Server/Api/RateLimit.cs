namespace Model.Server;

public static class RateLimit {
    public static event EventHandler<RateLimitEventArgs>? RateLimitsUpdated;

    private static void UpdateRateLimits(HttpResponseMessage response, object source) {
        if (RateLimitsUpdated != null) {
            // Get ratelimit info
            // https://docs.joinmastodon.org/api/rate-limits/

            var category = ApiCallCategory.Global;

            var requestMethod = response.RequestMessage?.Method;
            var requestPath = response.RequestMessage?.RequestUri?.AbsolutePath ?? "";
            if (requestMethod == HttpMethod.Post && requestPath == "/api/v1/media") {
                category = ApiCallCategory.MediaUpload;
            }
            if ((requestMethod == HttpMethod.Delete && requestPath.StartsWith("/api/v1/statuses/")) ||
                (requestMethod == HttpMethod.Post && requestPath.EndsWith("unreblog"))) {
                category = ApiCallCategory.StatusDelete;
            }


            var headers = response.Headers;
            var limit = headers.GetValues("X-RateLimit-Limit").FirstOrDefault();
            var remaining = headers.GetValues("X-RateLimit-Remaining").FirstOrDefault();
            var reset = headers.GetValues("X-RateLimit-Reset").FirstOrDefault();

            if (!string.IsNullOrEmpty(limit) && int.TryParse(limit, out int intLimit) &&
                !string.IsNullOrEmpty(remaining) && int.TryParse(remaining, out int intRemaining) &&
                !string.IsNullOrEmpty(reset) && DateTime.TryParse(reset, out DateTime dateReset)) {
                var rateLimitEventArgs = new RateLimitEventArgs {
                    RateLimitCategory = category,
                    Limit = intLimit,
                    Remaining = intRemaining,
                    Reset = dateReset
                };

                RateLimitsUpdated?.Invoke(source, rateLimitEventArgs);
            }
        }
    }
}

public class RateLimitEventArgs: EventArgs {
    public ApiCallCategory RateLimitCategory { get; internal set; }
    public int Limit { get; internal set; }
    public int Remaining { get; internal set; }
    public DateTime Reset { get; internal set; }
}

public enum ApiCallCategory {
    Global,
    MediaUpload,
    StatusDelete
}
