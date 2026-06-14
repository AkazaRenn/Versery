using Mastonet.Entities;

namespace Model.Entities;
public record struct Status {
    public string Content;
    public DateTime CreatedAt;
    public bool? Favourited;
    public bool? Reblogged;
    public bool? Bookmarked;

    internal Status(Mastonet.Entities.Status apiStatus) {
        Content = apiStatus.Content;
        CreatedAt = apiStatus.CreatedAt;
        Favourited = apiStatus.Favourited ?? false;
        Reblogged = apiStatus.Reblogged ?? false;
        Bookmarked = apiStatus.Bookmarked ?? false;
    }

    internal static IEnumerable<Status> From(MastodonList<Mastonet.Entities.Status> apiStatuses) {
        foreach (var apiStatus in apiStatuses) {
            yield return new Status(apiStatus);
        }
    }
}
