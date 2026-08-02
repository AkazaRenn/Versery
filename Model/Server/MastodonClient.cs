using Model.Server.Methods;
using Refit;

namespace Model.Server;

public class Client {
    #region Ctor

    private readonly RefitSettings settings = new() {
        ContentSerializer = new SystemTextJsonContentSerializer(JsonContext.Default.Options)
    };
    private readonly HttpClient apiHttpClient;

    public string InstanceUrl { get; }
    public string AccessToken { get; }

    #region API Endpoints
    public IAccounts Accounts { get; }
    public IInstance Instance { get; }
    public ITimelines Timelines { get; }
    #endregion

    public Client(string instance, string accessToken) {
        InstanceUrl = instance;
        AccessToken = accessToken;

        apiHttpClient = new() {
            BaseAddress = new Uri($"https://{InstanceUrl}")
        };
        apiHttpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", AccessToken);

        Accounts = RestService.For<IAccounts>(apiHttpClient, settings);
        Instance = RestService.For<IInstance>(apiHttpClient, settings);
        Timelines = RestService.For<ITimelines>(apiHttpClient, settings);
    }

    #endregion

    #region Rate limits

    public event EventHandler<RateLimitEventArgs>? RateLimitsUpdated;

    private void UpdateRateLimits(HttpResponseMessage response) {
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

                RateLimitsUpdated?.Invoke(this, rateLimitEventArgs);
            }
        }
    }

    protected void OnResponseReceived(HttpResponseMessage response) {
        UpdateRateLimits(response);
    }

    #endregion
}
