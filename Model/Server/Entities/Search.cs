using System.Text.Json.Serialization;

namespace Model.Server.Entities;
/// <see href="https://docs.joinmastodon.org/methods/search/#response">Mastodon API Documentation</see>
public sealed class Search {
    [JsonPropertyName("accounts")]
    public List<Account> Accounts { get; set; } = [];

    [JsonPropertyName("statuses")]
    public List<Status> Statuses { get; set; } = [];

    [JsonPropertyName("hashtags")]
    public List<Tag> Hashtags { get; set; } = [];

    [JsonPropertyName("collections")]
    public List<Collection> Collections { get; set; } = [];
}
