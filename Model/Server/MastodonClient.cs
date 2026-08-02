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
    public IProofs Proofs { get; }
    public IFollowRequests FollowRequests { get; }
    public ISuggestions Suggestions { get; }
    public IFavourites Favourites { get; }
    public IBookmarks Bookmarks { get; }
    public IFeaturedTags FeaturedTags { get; }
    public IInstance Instance { get; }
    public ITrends Trends { get; }
    public IDirectory Directory { get; }
    public IAnnouncements Announcements { get; }
    public ILists Lists { get; }
    public IMedia Media { get; }
    public ICustomEmojis CustomEmojis { get; }
    public INotifications Notifications { get; }
    public IReports Reports { get; }
    public ISearch SearchApi { get; }
    public IFilters Filters { get; }
    public IPolls Polls { get; }
    public IStatuses Statuses { get; }
    public IScheduledStatuses ScheduledStatuses { get; }
    public ITimelines Timelines { get; }
    public IConversations Conversations { get; }
    public IFollows Follows { get; }
    public IBlocks Blocks { get; }
    public IMutes Mutes { get; }
    public IEndorsements Endorsements { get; }
    public IMarkers Markers { get; }
    public IDomainBlocks DomainBlocks { get; }
    public ITags Tags { get; }
    public IFollowedTags FollowedTags { get; }
    public IAdminAccounts AdminAccounts { get; }
    #endregion

    public Client(string instance, string accessToken) {
        InstanceUrl = instance;
        AccessToken = accessToken;

        apiHttpClient = new() {
            BaseAddress = new Uri($"https://{InstanceUrl}")
        };
        apiHttpClient.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", AccessToken);

        Accounts = RestService.For<IAccounts>(apiHttpClient, settings);
        Proofs = RestService.For<IProofs>(apiHttpClient, settings);
        FollowRequests = RestService.For<IFollowRequests>(apiHttpClient, settings);
        Suggestions = RestService.For<ISuggestions>(apiHttpClient, settings);
        Favourites = RestService.For<IFavourites>(apiHttpClient, settings);
        Bookmarks = RestService.For<IBookmarks>(apiHttpClient, settings);
        FeaturedTags = RestService.For<IFeaturedTags>(apiHttpClient, settings);
        Instance = RestService.For<IInstance>(apiHttpClient, settings);
        Trends = RestService.For<ITrends>(apiHttpClient, settings);
        Directory = RestService.For<IDirectory>(apiHttpClient, settings);
        Announcements = RestService.For<IAnnouncements>(apiHttpClient, settings);
        Lists = RestService.For<ILists>(apiHttpClient, settings);
        Media = RestService.For<IMedia>(apiHttpClient, settings);
        CustomEmojis = RestService.For<ICustomEmojis>(apiHttpClient, settings);
        Notifications = RestService.For<INotifications>(apiHttpClient, settings);
        Reports = RestService.For<IReports>(apiHttpClient, settings);
        SearchApi = RestService.For<ISearch>(apiHttpClient, settings);
        Filters = RestService.For<IFilters>(apiHttpClient, settings);
        Polls = RestService.For<IPolls>(apiHttpClient, settings);
        Statuses = RestService.For<IStatuses>(apiHttpClient, settings);
        ScheduledStatuses = RestService.For<IScheduledStatuses>(apiHttpClient, settings);
        Timelines = RestService.For<ITimelines>(apiHttpClient, settings);
        Conversations = RestService.For<IConversations>(apiHttpClient, settings);
        Follows = RestService.For<IFollows>(apiHttpClient, settings);
        Blocks = RestService.For<IBlocks>(apiHttpClient, settings);
        Mutes = RestService.For<IMutes>(apiHttpClient, settings);
        Endorsements = RestService.For<IEndorsements>(apiHttpClient, settings);
        Markers = RestService.For<IMarkers>(apiHttpClient, settings);
        DomainBlocks = RestService.For<IDomainBlocks>(apiHttpClient, settings);
        Tags = RestService.For<ITags>(apiHttpClient, settings);
        FollowedTags = RestService.For<IFollowedTags>(apiHttpClient, settings);
        AdminAccounts = RestService.For<IAdminAccounts>(apiHttpClient, settings);
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
