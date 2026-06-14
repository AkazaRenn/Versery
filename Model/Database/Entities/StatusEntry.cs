using LiteDB;

namespace Model.Database.Entities;
public record class StatusEntry {
    [BsonId]
    public string Id;
    public DateTime CreatedAt;
    public bool Favourited;
    public bool Reblogged;
    public bool Muted;
    public bool Bookmarked;
    public bool Pinned;

    internal StatusEntry(Mastonet.Entities.Status apiStatus) {
        Id = apiStatus.Id;
        CreatedAt = apiStatus.CreatedAt;
        Favourited = apiStatus.Favourited ?? false;
        Reblogged = apiStatus.Reblogged ?? false;
        Muted = apiStatus.Muted ?? false;
        Bookmarked = apiStatus.Bookmarked ?? false;
        Pinned = apiStatus.Pinned ?? false;
    }
}
