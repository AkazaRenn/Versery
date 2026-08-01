using System.Text.Json.Serialization;

namespace Model.Server.Entities;
/// <summary>
/// Represents the results of a search.
/// Version: 4.6.0
/// </summary>
/// <see href="https://docs.joinmastodon.org/methods/search/#response">Mastodon API Documentation</see>
public class Search {
    /// <summary>
    /// Accounts which match the given query
    /// </summary>
    [JsonPropertyName("accounts")]
    public IEnumerable<Account> Accounts { get; set; } = [];

    /// <summary>
    /// Statuses which match the given query
    /// </summary>
    [JsonPropertyName("statuses")]
    public IEnumerable<Status> Statuses { get; set; } = [];

    /// <summary>
    /// Hashtags which match the given query
    /// </summary>
    [JsonPropertyName("hashtags")]
    public IEnumerable<Tag> Hashtags { get; set; } = [];

    /// <summary>
    /// Collections which match the given query
    /// </summary>
    [JsonPropertyName("collections")]
    public IEnumerable<Collection> Collections { get; set; } = [];
}
